using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KursovaRabota.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(4, ErrorMessage = "First Name is at least 4 symbols")]
        [MaxLength(10, ErrorMessage = "First Name is max 10 symbols")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "The field {0} should be between 5 and 10 characters")]
        [RegularExpression("([A-Za-z]+)", ErrorMessage = "last name should contain only letters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Range(12, 120, ErrorMessage = "You should be above 12 years to use this site!")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}