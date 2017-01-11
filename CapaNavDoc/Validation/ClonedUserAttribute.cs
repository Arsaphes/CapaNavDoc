using System.ComponentModel.DataAnnotations;
using CapaNavDoc.Classes;
using CapaNavDoc.DataAccessLayer;
using CapaNavDoc.Models;
using CapaNavDoc.Models.BusinessLayers;

namespace CapaNavDoc.Validation
{
    public class ClonedUserAttribute : ValidationAttribute
    {
        public string FirstNameProperty { get; set; }
        public string LastNameProperty { get; set; }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            BusinessLayer<User> bl = new BusinessLayer<User>(new CapaNavDocDal());

            if(validationContext.ObjectInstance.GetType().GetProperty("EditionMode").GetValue(validationContext.ObjectInstance)?.ToString() == EditionMode.Update) return ValidationResult.Success;

            string firstName = validationContext.ObjectInstance.GetType().GetProperty(FirstNameProperty).GetValue(validationContext.ObjectInstance)?.ToString();
            string lastname = validationContext.ObjectInstance.GetType().GetProperty(LastNameProperty).GetValue(validationContext.ObjectInstance)?.ToString();

            return bl.GetList().Exists(u => u.FirstName == firstName && u.LastName == lastname) ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }
}