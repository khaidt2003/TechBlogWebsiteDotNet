using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechBlogWebsite.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Username can not be blank !!!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can not be blank !!!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password can not be blank !!!")]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [NotMapped]

        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email can not be blank !!!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [Required(ErrorMessage = "First Name can not be blank !!!")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last Name can not be blank !!!")]
        public string lastName { get; set; }
    }
}