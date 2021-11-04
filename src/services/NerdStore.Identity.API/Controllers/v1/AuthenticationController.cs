using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NerdStore.Core.Messages;
using NerdStore.Core.SharedEvents;
using NerdStore.Identity.API.Authentication;
using NerdStore.Identity.API.Extensions;
using NerdStore.Identity.API.ViewModels;
using NerdStore.MessageBus;
using NerdStore.WebAPI.Core.Controllers;
using NerdStore.WebAPI.Core.Jwt;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Identity.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]    
    public class AuthenticationController : MainController
    {
        private readonly IMessageBus _bus;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenSettings _tokenSettings;
        private readonly ILogger _logger;

        public AuthenticationController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<TokenSettings> tokenSettings,
            IMessageBus bus,
            ILogger<AuthenticationController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenSettings = tokenSettings.Value;
            _bus = bus;
            _logger = logger;
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateAccount(RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUserViewModel.Name,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUserViewModel.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Novo usuário criado ({registerUserViewModel.Email})");

                var createCustomerResult = await CreateCustomer(registerUserViewModel);

                _logger.LogInformation($"Usuário enviado para a fila de criação de clientes");

                if (!createCustomerResult.ValidationResult.IsValid)
                {
                    await _userManager.DeleteAsync(user);

                    return CustomResponse(createCustomerResult.ValidationResult);
                }

                return CustomResponse(await GenerateJwt(registerUserViewModel.Email));
            }

            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }

            return CustomResponse(registerUserViewModel);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, loginUserViewModel.Password, false, true);

            if (result.Succeeded)
            {
                _logger.LogInformation($"Usuário logado na aplicação ({loginUserViewModel.Email})");

                return CustomResponse(await GenerateJwt(loginUserViewModel.Email));
            }

            if (result.IsLockedOut)
            {
                AddError("O usuário foi temporariamente bloqueado por tentativas inválidas de login");

                return CustomResponse(loginUserViewModel);
            }

            AddError("Usuário ou senha inválidos");

            return CustomResponse(loginUserViewModel);
        }

        private async Task<LoginResponseViewModel> GenerateJwt(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var claims = await _userManager.GetClaimsAsync(user);
            var identityClaims = await GetUserClaimsAndRoles(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_tokenSettings.HoursToExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return new LoginResponseViewModel
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = TimeSpan.FromHours(_tokenSettings.HoursToExpire).TotalSeconds,
                User = new UserTokenViewModel
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private async Task<ClaimsIdentity> GetUserClaimsAndRoles(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64));

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private async Task<ResponseMessage> CreateCustomer(RegisterUserViewModel registerUserViewModel)
        {
            var createdUser = await _userManager.FindByEmailAsync(registerUserViewModel.Email);

            try
            {
                var createdUserIntegrationEvent = new AddedUserIntegrationEvent(
                    Guid.Parse(createdUser.Id), 
                    registerUserViewModel.Name, 
                    registerUserViewModel.Email, 
                    registerUserViewModel.Cpf);

                return await _bus.RequestAsync<AddedUserIntegrationEvent, ResponseMessage>(createdUserIntegrationEvent);
            }
            catch
            {
                await _userManager.DeleteAsync(createdUser);
                
                throw;
            }
        }
    }
}
