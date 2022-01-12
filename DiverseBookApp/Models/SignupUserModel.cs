using System;
using System.ComponentModel.DataAnnotations;

namespace DiverseBookApp.Models
{
    public class SignupUserModel
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="Please enter Fisrt Name!")]
        public string FirstName { get; set; }


        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage ="Please enter email!")]
        [EmailAddress(ErrorMessage ="Please enter valid Email!")]
        public string Email { get; set; }


        [Required(ErrorMessage ="Please enter Password!")]
        [Compare("ConfirmPassword", ErrorMessage ="Password does not match!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        [Required(ErrorMessage ="Please enter Confirm Password!")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }        
    }
}
