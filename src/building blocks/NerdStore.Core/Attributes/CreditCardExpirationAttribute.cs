using System;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class CreditCardExpirationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var month = value.ToString().Split('/')[0];
            var year = $"20{value.ToString().Split('/')[1]}";

            if (int.TryParse(month, out var monthAux) &&
                int.TryParse(year, out var yearAux))
            {
                var date = new DateTime(yearAux, monthAux, 1);
                
                return date > DateTime.UtcNow;
            }

            return false;
        }
    }
}
