using System.ComponentModel.DataAnnotations;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Validation
{
    public class ClonedUserNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || validationContext.ObjectInstance.GetType().GetProperty("EditionMode").GetValue(validationContext.ObjectInstance)?.ToString() == EditionMode.Update) return ValidationResult.Success;

            BusinessLayer<User> bl = new BusinessLayer<User>(new CapaNavDocDal());
            return bl.GetList().Exists(u => u.UserName == value.ToString()) ? new ValidationResult(ErrorMessage) : ValidationResult.Success;

        }
    }
}