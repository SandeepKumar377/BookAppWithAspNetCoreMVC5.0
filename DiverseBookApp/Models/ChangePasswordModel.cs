using System.ComponentModel.DataAnnotations;

namespace DiverseBookApp.Models
{
    public class ChangePasswordModel
    {
        [Required, DataType(DataType.Password), Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }


        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [Compare("NewPassword", ErrorMessage ="Confirm password does not match!")]
        [Required, DataType(DataType.Password), Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
