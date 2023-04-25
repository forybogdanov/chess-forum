using GameWebsite.Service.Models.Comments;

namespace GameWebsite.Services.Comments
{
    public interface ICommentService
    {
        Task<CommentDto> CreateComment(CommentDto commentDto);
        Task<CommentDto> DeleteComment(long id);
        List<CommentDto> GetAllComments();
        Task<CommentDto> GetCommentById(long id);
        Task<CommentDto> UpdateComment(long id, CommentDto commentDto);
    }
}
