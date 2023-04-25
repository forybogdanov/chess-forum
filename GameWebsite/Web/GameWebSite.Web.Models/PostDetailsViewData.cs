using GameWebsite.Service.Models.Comments;
using GameWebsite.Service.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebSite.Web.Models
{
    public class PostDetailsViewData
    {
        public PostDetailsViewData(PostDto post, List<CommentDto> comments)
        {
            Post = post;
            Comments = comments;
        }

        public PostDto Post { get; set; }
        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
