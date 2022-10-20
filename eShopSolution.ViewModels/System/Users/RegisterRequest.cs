using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eShopSolution.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(32, ErrorMessage = "First name must be between 1 and 32 characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(32, ErrorMessage = "Last name must be between 1 and 32 characters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "E-Mail Address does not appear to be valid!")]
        public string Email { get; set; }

        [StringLength(11, ErrorMessage = "Max 10 to 11 digits")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password confirmation does not match password!")]
        public string ConfirmPassword { get; set; }
    }
}
