namespace GameWebsite.Data.Models
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public GameWebsiteUser CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
