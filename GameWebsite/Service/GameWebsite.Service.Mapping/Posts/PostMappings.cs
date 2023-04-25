using GameWebsite.Data.Models;
using GameWebsite.Service.Mapping.Categories;
using GameWebsite.Service.Mapping.Comments;
using GameWebsite.Service.Mapping.Users;
using GameWebsite.Service.Models.Posts;

namespace GameWebsite.Service.Mapping.Posts
{
    public static class PostMappings
    {
        public static Post ToEntity(this PostDto postDto)
        {
            return new Post
            {
                Id = postDto.Id,
                Name = postDto.Name,
                Content = postDto.Content,
                CreatedAt = postDto.CreatedAt,
                Category = postDto.Category.ToEntity(),
                CreatedBy = postDto.CreatedBy.ToEntity(),
                Comments = postDto.Comments.Select(commentDto => commentDto.ToEntity()).ToList(),
                /*Attachments = postDto.Attachments.Select(attachmentDto => attachmentDto.ToEntity()).ToList(),*/

            }; 
        }

        public static PostDto ToDto(this Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Name = post.Name,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                Category = post.Category.ToDto(),
                CreatedBy = post.CreatedBy.ToDto(),
                Comments = post.Comments.Select(comment => comment.ToDto()).ToList(),
                /*Attachments = post.Attachments.Select(attachment => attachment.ToEntity()).ToList()*/
            };
        }
    }
}
