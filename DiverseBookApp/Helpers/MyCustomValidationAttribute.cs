using System.ComponentModel.DataAnnotations;

namespace DiverseBookApp.Helpers
{
    public class MyCustomValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string bookName=value.ToString();
                if(bookName.Contains("mvc"))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage??"Book name does not contain desired Value");
        }
    }
}
