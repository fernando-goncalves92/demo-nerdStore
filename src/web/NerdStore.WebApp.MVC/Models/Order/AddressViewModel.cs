using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.WebApp.MVC.Models.Order
{
    public class AddressViewModel
    {
        [Required]
        [DisplayName("Rua")]
        public string Street { get; set; }
        
        [Required]
        [DisplayName("Número")]
        public string Number { get; set; }
        
        [DisplayName("Complemento")]
        public string Complement { get; set; }
        
        [Required]
        [DisplayName("Bairro")]
        public string District { get; set; }
        
        [Required]
        [DisplayName("CEP")]
        public string ZipCode { get; set; }
        
        [Required]
        [DisplayName("Cidade")]
        public string City { get; set; }
        
        [Required]
        [DisplayName("Estado")]
        public string State { get; set; }

        public override string ToString()
        {
            return $"{Street}, {Number} {Complement} - {District} - {City} - {State}";
        }
    }
}
