using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Models
{
    public class Blog
    {
        //Non-descriptive administative property
        public int Id { get; set; }

        //Security in MVC
        [Required]
        [StringLength(100, ErrorMessage = "You messed up", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} property must be at least {2} characters and at most {1}", MinimumLength = 5)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        //This represents the byte data not the physical file
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        //This property represents a physical file chose by the user
        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }

        //Add a navigational property to reference all of my children
        public ICollection<BlogPost> BlogPosts = new HashSet<BlogPost>();
        //ICollection means it's a generic collection that could be satisfied with any data type.
        //A hashset is a datastructure used to satisfy an ICollection.
        //new is used to create an instance of a class. 
    }




}
