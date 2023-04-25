
namespace GameWebsite.Data.Models
{
    public class Post : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public Category Category { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
