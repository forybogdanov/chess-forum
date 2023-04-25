using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Service.Mapping.Categories;
using GameWebsite.Service.Mapping.Posts;
using GameWebsite.Service.Mapping.Users;
using GameWebsite.Service.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace GameWebsite.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly GameWebsiteDbContext gameWebsiteDbContext;

        public PostService(GameWebsiteDbContext gameWebsiteDbContext)
        {
            this.gameWebsiteDbContext = gameWebsiteDbContext;
        }
        public async Task<PostDto> CreatePost(PostDto postDto)
        {
            Post post = postDto.ToEntity();
            post.CreatedBy = await this.gameWebsiteDbContext.Users.SingleOrDefaultAsync(user => user.Id == postDto.CreatedBy.Id);
            post.Category = await this.gameWebsiteDbContext.Categories
                .Include(category => category.CreatedBy)
                .SingleOrDefaultAsync(category => category.Id == postDto.Category.Id);
            post.CreatedAt = DateTime.Now;
            await this.gameWebsiteDbContext.AddAsync(post);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            return post.ToDto();
        }

        public async Task<PostDto> DeletePost(long id)
        {
            Post post = this.gameWebsiteDbContext.Posts
                .Include(post => post.Category)
                .Include(post => post.Category.CreatedBy)
                .Include(post => post.CreatedBy)
                .Include(post => post.Comments)
                .SingleOrDefault(post => post.Id == id);
            if (post == null)
            {
                // TODO: throw exception
                return null;
            }

            PostDto postDto = post.ToDto();
            this.gameWebsiteDbContext.Remove(post);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            return postDto;
        }

        public List<PostDto> GetAllPosts()
        {
            /*IQueryable<PostDto> posts = this.gameWebsiteDbContext.Posts
                .Include(post => post.CreatedBy)
                .Include(post => post.Category)
                .Select((post) => 
                {
                    post.Comments = new List<Comment>();
                    return post;
                })
                .Select(post => post.toDto());*/
            List<Post> posts = this.gameWebsiteDbContext.Posts
                .Include(post => post.CreatedBy)
                .Include(post => post.Category).ToList();

            List<PostDto> postDtos = posts.Select((post) =>
            {
                post.Comments = new List<Comment>();
                return post.ToDto();
            }).ToList();
            return postDtos;
        }

        public async Task<PostDto> GetPostById(long id)
        {
            Post post = this.gameWebsiteDbContext.Posts
                .Include(post => post.CreatedBy)
                .Include(post => post.Category)
                .Include(post => post.Category.CreatedBy)
                .SingleOrDefault(post => post.Id == id);
            post.Comments = new List<Comment>();
            if (post == null)
            {
                // TODO: throw exception
                return null;
            }
            return post.ToDto();
        }

        public async Task<PostDto> UpdatePost(long id, PostDto postDto)
        {
            Post post = this.gameWebsiteDbContext.Posts
                .Include(post => post.Category)
                .Include(post => post.Category.CreatedBy)
                .Include(post => post.Comments)
                .SingleOrDefault(post => post.Id == id);
            if (post == null)
            {
                // TODO: throw exception
                return null;
            }
            post.Name = postDto.Name;
            post.Content = postDto.Content;
            this.gameWebsiteDbContext.Update(post);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            // TODO: set posts
            return post.ToDto();
        }
    }
}
