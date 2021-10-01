using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Brand.ViewModel
{
    public class LoginVModel
    {
        [Required]
        public string Username { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class RegisterVModel
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare("Password"), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
