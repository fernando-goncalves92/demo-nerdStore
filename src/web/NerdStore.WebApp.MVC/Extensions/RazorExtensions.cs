using Microsoft.AspNetCore.Mvc.Razor;
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

        public static string CurrencyFormat(this RazorPage page, decimal price)
        {
            return string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", price);
        }

        private static string CurrencyFormat(decimal price)
        {
            return string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", price);
        }

        public static string StockMessage(this RazorPage page, int amount)
        {
            return amount > 0 ? $"Apenas {amount} em estoque!" : "Produto esgotado!";
        }

        public static string UnitPerProduct(this RazorPage page, int unit)
        {
            return unit > 1 ? $"{unit} unidades" : $"{unit} unidade";
        }

        public static string SelectOptionsPerAmount(this RazorPage page, int amount, int selectedValue = 0)
        {
            var stringBuilder = new StringBuilder();

            for (var count = 1; count <= amount; count++)
            {
                string selected = string.Empty;

                if (count == selectedValue) 
                    selected = "selected";
                
                stringBuilder.Append($"<option {selected} value='{count}'>{count}</option>");
            }

            return stringBuilder.ToString();
        }

        public static string UnitPerProductTotalPurchase(this RazorPage page, int amount, decimal price)
        {
            return $"{amount}x {CurrencyFormat(price)} = Total: {CurrencyFormat(price * amount)}";
        }

        public static string ShowStatus(this RazorPage page, int status)
        {
            var statusMessage = "";
            var statusClass = "";

            switch (status)
            {
                case 1:
                    statusClass = "info";
                    statusMessage = "Em aprovação";
                    break;
                case 2:
                    statusClass = "primary";
                    statusMessage = "Aprovado";
                    break;
                case 3:
                    statusClass = "danger";
                    statusMessage = "Recusado";
                    break;
                case 4:
                    statusClass = "success";
                    statusMessage = "Entregue";
                    break;
                case 5:
                    statusClass = "warning";
                    statusMessage = "Cancelado";
                    break;
            }

            return $"<span class='badge badge-{statusClass}'>{statusMessage}</span>";
        }
    }
}
