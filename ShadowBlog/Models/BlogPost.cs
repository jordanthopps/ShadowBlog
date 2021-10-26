using Microsoft.AspNetCore.Http;
using ShadowBlog.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; } //primary key (PK) for every individual record in the table.


        public int BlogId { get; set; } //foreign key (FK) to associate which blog it belongs to.
                                        //We are programmatically telling the db that the BlogPost is the kid and the BlogId is the parent.

        //Descriptive Properties of a blogpost
        [Required]
        [StringLength(50, ErrorMessage = "You messed up", MinimumLength = 5)]
        public string Title { get; set; }

        //A property to get the user interested without forcing them to read the entire post...
        [Required]
        [StringLength(200, ErrorMessage = "You messed up", MinimumLength = 50)]
        public string Abstract { get; set; }

        [Required]
        public string Content { get; set; }

        //Records the date, the time, and the number of hours offset from Greenwich Time Zone
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTimeOffset Created { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Updated Date")]
        public DateTime? Updated { get; set; }

        //Add a property of type bool named ProductionReady
        [Required]
        [Display(Name = "Ready Status")]
        public ReadyState ReadyStatus { get; set; }

        //This will basically be the Title run through a formatter
        //Role Based Security - role-based-security
        public string Slug { get; set; }

        //This represents the byte data not the physical file
        public byte[] ImageData {get; set; }
        public string ContentType { get; set; }

        //This property represents a physical file chosen by the user
        [NotMapped]
        public IFormFile Image { get; set; }

        //Navigational properties
        public virtual Blog Blog { get; set; }
    }
}
