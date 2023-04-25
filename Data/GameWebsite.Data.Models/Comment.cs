namespace GameWebsite.Data.Models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public Post Post { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
