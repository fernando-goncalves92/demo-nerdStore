using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NerdStore.WebApp.MVC.Models.User;
using Auth = NerdStore.WebApp.MVC.Services.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class IdentityController : MainController
    {
        private readonly Auth.IAuthenticationService _authenticationService;
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(Auth.IAuthenticationService authenticationService, ILogger<IdentityController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpGet("create-account")]        
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount(UserRegister user)
        {
            if (!ModelState.IsValid) 
                return View(user);

            var response = await _authenticationService.CreateAccount(user);

            if (ResponseHasErros(response.ResponseResult))
                return View(user);

            await LoginApp(response);

            return RedirectToAction("Index", "Catalog");
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) 
                return View(user);

            var response = await _authenticationService.Login(user);

            if (ResponseHasErros(response.ResponseResult))
                return View(user);

            await LoginApp(response);

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Catalog");
            }

            return LocalRedirect(returnUrl);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Catalog");
        }

        private async Task LoginApp(UserLoginResponse user)
        {
            var token = new JwtSecurityTokenHandler().ReadToken(user.AccessToken) as JwtSecurityToken;
            var claims = new List<Claim>();
            
            claims.Add(new Claim("JWT", user.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }
    }
}
