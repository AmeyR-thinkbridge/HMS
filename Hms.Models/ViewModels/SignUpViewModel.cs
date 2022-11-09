#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsCustomer { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

    }
}
