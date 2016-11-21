using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WAndCAssignment.Models
{
    public class CustomerLogin
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Invalid Username")]
        public string Username { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The password address is required")]
        [StringLength(10, ErrorMessage = "Password must contain 5 to 10 characters", MinimumLength = 5)]
        public string Password { get; set; }
    }
}