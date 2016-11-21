using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAndCAssignment.Models
{
    public class Members
    {


        //properties
        [Required(ErrorMessage = "Must Include First Name Please ")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First Name must contain alphabets")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Must Include Last Name Please ")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last Name must contain alphabets")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username can't be empty")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The password address is required")]
        [StringLength(10, ErrorMessage = "Password must contain 5 to 10 characters", MinimumLength = 5)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


    }
}