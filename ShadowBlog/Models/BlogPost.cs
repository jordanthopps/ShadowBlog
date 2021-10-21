using System;
using System.Collections.Generic;
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
        public string Title { get; set; }

        //A property to get the user interested without forcing them to read the entire post...
        public string Abstract { get; set; }

        public string Content { get; set; }

        //Records the date, the time, and the number of hours offset from Greenwich Time Zone
        public DateTimeOffset Created { get; set; }

        public DateTime? Updated { get; set; }


        //Navigational properties
        public virtual Blog Blog { get; set; }
    }
}
