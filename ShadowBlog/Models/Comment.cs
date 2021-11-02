using ShadowBlog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }

        /*We need to record the Id of the registered and logged in 
         * User who is creating these comments*/
        public string BlogUserId { get; set; }
        public string ModeratorId { get; set; }

        //Descriptive props
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; } //Nullable because the commentor might never update their post
        public string CommentBody { get; set; }

        //The Moderator related properties
        public DateTime? Deleted { get; set; } //soft delete
        public DateTime? Moderated { get; set; }
        public string ModeratedBody { get; set; } //modify body of comments
        public ModType Moderationtype { get; set; }

        //Navigational properties
        public virtual BlogPost BlogPost { get; set; }
        public virtual BlogUser BlogUser { get; set; }
        public virtual BlogUser Moderator { get; set; }

    }
}
