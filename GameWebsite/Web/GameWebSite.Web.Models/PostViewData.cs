using GameWebsite.Service.Models.Posts;

namespace GameWebSite.Web.Models
{
    public class PostViewData
    {
        public PostViewData(long categoryId, List<PostDto> posts)
        {
            CategoryId = categoryId;
            Posts = posts;
        }

        public long CategoryId { get; set; }
        public List<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}
