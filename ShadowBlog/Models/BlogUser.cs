using Microsoft.AspNetCore.Identity; //white means a class is using it.
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks; //greyed out means it's not being used by c#.

namespace ShadowBlog.Models
{
    public class BlogUser : IdentityUser //BlogUser now has all of the properties IdentityUser has.
    {
        //What's needed at this point are other properties I want like...

        [Required]
        [Display(Name = "First Name")]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long,", MinimumLength = 2)] 
        /*StringLength takes three args: 
         * (1) max number of chars. 
         * (2) the error message which should appear written in string.format {Annotation} {MinimumLength} {40 Chars}
         * (3) the minimum length*/
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(40, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long,", MinimumLength = 2)]
        public string LastName { get; set; }

        [Display(Name = "Display Name")]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long,", MinimumLength = 2)]
        public string DisplayName { get; set; }

        [NotMapped]
        public string FullName //a derived property (from FirstName and LastName).
        {
            get
            {
                return $"{FirstName} {LastName}"; //string interpolation.
            }
        }

    }
}
