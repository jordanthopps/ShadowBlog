using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Models
{
    public class Blog
    {
        //Non-descriptive administative property
        public int Id { get; set; }

        //Security in MVC
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        //We could add a property for an image.
        //We could add a property for an image type.

        //Add a navigational property to reference all of my children
        public ICollection<BlogPost> BlogPosts = new HashSet<BlogPost>();
        //ICollection means it's a generic collection that could be satisfied with any data type.
        //A hashset is a datastructure used to satisfy an ICollection.
    }




}
