using NerdStore.Core.Attributes;
using NerdStore.WebApp.MVC.Models.ShoppingCart;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.WebApp.MVC.Models.Order
{
    public class OrderTransactionViewModel
    {
        public decimal TotalPurchase { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }
        public List<ShoppingCartItemViewModel> ShoppingCartItems { get; set; } = new List<ShoppingCartItemViewModel>();
        public AddressViewModel Address { get; set; }
        
        [Required(ErrorMessage = "Informe o número do cartão")]
        [DisplayName("Número do Cartão")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Informe o nome do portador do cartão")]
        [DisplayName("Nome do Portador")]
        public string CreditCardName { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "O vencimento deve estar no padrão MM/AA")]
        [CreditCardExpiration(ErrorMessage = "Cartão Expirado")]
        [Required(ErrorMessage = "Informe o vencimento")]
        [DisplayName("Data de Vencimento MM/AA")]
        public string CreditCardExpirationDate { get; set; }

        [Required(ErrorMessage = "Informe o código de segurança")]
        [DisplayName("Código de Segurança")]
        public string CreditCardNumberCvv { get; set; }
    }
}
