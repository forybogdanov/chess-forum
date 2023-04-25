using GameWebsite.Data.Models;
using GameWebsite.Service.Mapping.Posts;
using GameWebsite.Service.Mapping.Users;
using GameWebsite.Service.Models.Comments;

namespace GameWebsite.Service.Mapping.Comments
{
    public static class CommentMappings
    {
        public static Comment ToEntity(this CommentDto commentDto)
        {
            return new Comment
            {
                Id = commentDto.Id,
                CreatedAt = commentDto.CreatedAt,
                Content = commentDto.Content,
                CreatedBy = commentDto.CreatedBy.ToEntity(),
                Post = commentDto.Post.ToEntity()
            };
        }
        public static CommentDto ToDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                CreatedAt = comment.CreatedAt,
                Content = comment.Content,
                CreatedBy = comment.CreatedBy.ToDto(),
                Post = comment.Post.ToDto()
            };
        }
    }
}
