using GameWebsite.Service.Models;
using GameWebsite.Service.Models.Attachments;
using GameWebsite.Service.Models.Categories;
using GameWebsite.Service.Models.Comments;

namespace GameWebsite.Service.Models.Posts
{
    public class PostDto : BaseDto
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public CategoryDto Category { get; set; }
        public List<CommentDto> Comments { get; set; }
        /*public List<AttachmentDto> Attachments { get; set; }*/
    }
}
