using GameWebsite.Data;
using GameWebsite.Data.Models;
using GameWebsite.Service.Mapping.Comments;
using GameWebsite.Service.Models.Comments;
using Microsoft.EntityFrameworkCore;

namespace GameWebsite.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly GameWebsiteDbContext gameWebsiteDbContext;

        public CommentService(GameWebsiteDbContext gameWebsiteDbContext)
        {
            this.gameWebsiteDbContext = gameWebsiteDbContext;
        }

        public async Task<CommentDto> CreateComment(CommentDto commentDto)
        {
            Comment comment = commentDto.ToEntity();
            comment.CreatedBy = await this.gameWebsiteDbContext.Users.SingleOrDefaultAsync(user => user.Id == commentDto.CreatedBy.Id);
            comment.Post = await this.gameWebsiteDbContext.Posts
                .Include(post => post.CreatedBy)
                .Include(post => post.Category)
                .Include(post => post.Category.CreatedBy)
                .SingleOrDefaultAsync(post => post.Id == commentDto.Post.Id);
            comment.Post.Comments = new List<Comment>();
            comment.CreatedAt = DateTime.Now;
            await this.gameWebsiteDbContext.AddAsync(comment);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            return comment.ToDto();
        }

        public async Task<CommentDto> DeleteComment(long id)
        {
            Comment comment = this.gameWebsiteDbContext.Comments
                .Include(comment => comment.CreatedBy)
                .Include(comment => comment.Post)
                .SingleOrDefault(comment => comment.Id == id);
            if (comment == null)
            {
                // TODO: throw exception
                return null;
            }
            comment.Post = new Post
            {
                Id = comment.Post.Id,
                Comments = new List<Comment>(),
                CreatedBy = new GameWebsiteUser(),
                Category = new Category
                {
                    CreatedBy = new GameWebsiteUser()
                }
            };
            CommentDto commentDto = comment.ToDto();
            this.gameWebsiteDbContext.Remove(comment);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            return commentDto;
        }

        public List<CommentDto> GetAllComments()
        {
            List<Comment?> comments = this.gameWebsiteDbContext.Comments
                .Include(comment => comment.CreatedBy)
                .Include(comment => comment.Post)
                .Select(comment => comment).ToList();
            comments = comments.Select((Comment comment) =>
            {
                comment.Post = new Post
                {
                    Id = comment.Post.Id,
                    Comments = new List<Comment>(),
                    CreatedBy = new GameWebsiteUser(),
                    Category = new Category
                    {
                        CreatedBy = new GameWebsiteUser()
                    }
                };
                return comment;
            }).ToList();
            return comments.Select(comment => comment.ToDto()).ToList();
        }

        public async Task<CommentDto> GetCommentById(long id)
        {
            Comment comment = this.gameWebsiteDbContext.Comments
                .Include(comment => comment.CreatedBy)
                .Include(comment => comment.Post)
                .Include(comment => comment.Post.CreatedBy)
                .SingleOrDefault(post => post.Id == id);
            if (comment == null)
            {
                // TODO: throw exception
                return null;
            }
            return comment.ToDto();
        }

        public async Task<CommentDto> UpdateComment(long id, CommentDto commentDto)
        {
            Comment comment = this.gameWebsiteDbContext.Comments
                .Include(comment => comment.Post)
                .Include(comment => comment.Post.CreatedBy)
                .SingleOrDefault(comment => comment.Id == id);
            if (comment == null)
            {
                // TODO: throw exception
                return null;
            }
            comment.Content = commentDto.Content;
            this.gameWebsiteDbContext.Update(comment);
            await this.gameWebsiteDbContext.SaveChangesAsync();
            // TODO: set comments
            return comment.ToDto();
        }
    }
}
