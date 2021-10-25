using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;

namespace NerdStore.WebApp.MVC.Extensions
{
    public static class RazorExtensions
    {
        public static string HashEmailForGravatar(this RazorPage page, string email)
        {
            var hash = MD5.Create();
            var emailEncrypted = hash.ComputeHash(Encoding.Default.GetBytes(email));
            var builder = new StringBuilder();
            
            foreach (var data in emailEncrypted)
            {
                builder.Append(data.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
