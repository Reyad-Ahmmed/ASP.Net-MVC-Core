using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Brand.Custom_Attribute
{
    public class ValidBrandAttribute:ValidationAttribute,IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-brand", "Invalid Data. Brand must be Nokia, Samsung, Xaomi, One Plus or iPhone");
        }

        public override bool IsValid(object value)
        {
            return value.ToString() == "Nokia" || value.ToString() == "Samsung" ||
                value.ToString() == "Xaomi" || value.ToString() == "One Plus" ||
                value.ToString() == "iPhone";
        }
    }
}
