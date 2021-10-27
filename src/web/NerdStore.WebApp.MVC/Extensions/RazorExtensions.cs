﻿using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

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

        public static string CurrencyFormat(this RazorPage page, decimal value)
        {
            return string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value);
        }

        public static string StockMessage(this RazorPage page, int amount)
        {
            return amount > 0 ? $"Apenas {amount} em estoque!" : "Produto esgotado!";
        }
    }
}
