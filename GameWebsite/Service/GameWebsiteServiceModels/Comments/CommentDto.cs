using GameWebsite.Service.Models;
using GameWebsite.Service.Models.Attachments;
using GameWebsite.Service.Models.Posts;

namespace GameWebsite.Service.Models.Comments
{
    public class CommentDto : BaseDto
    {
        public string Content { get; set; }
        public PostDto Post { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
    }
}
