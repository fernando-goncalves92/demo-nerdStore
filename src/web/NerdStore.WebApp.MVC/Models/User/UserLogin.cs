using System.ComponentModel.DataAnnotations;

namespace NerdStore.WebApp.MVC.Models.User
{
    public class UserLogin
    {
        [Required(ErrorMessage = "O campo {0} � obrigat�rio")]
        [EmailAddress(ErrorMessage = "O campo {0} est� em formato inv�lido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
    }
}