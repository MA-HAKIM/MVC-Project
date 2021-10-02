using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidence_07_Mid_Monthly.CustomVal
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidLocationAttribute : ValidationAttribute, IClientValidatable
    {

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "location"
            };
            yield return rule;
        }

        public override bool IsValid(object value)
        {
            if (value.ToString() == "Dhaka" || value.ToString() == "Chattagram" || value.ToString() == "Rajshahi")
                return true;
            else return false;
        }
    }
}