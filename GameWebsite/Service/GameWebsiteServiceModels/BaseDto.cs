using GameWebsite.Service.Models.Users;

namespace GameWebsite.Service.Models
{
    public class BaseDto
    {
        public long Id { get; set; }
        public GameWebsiteUserDto CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
