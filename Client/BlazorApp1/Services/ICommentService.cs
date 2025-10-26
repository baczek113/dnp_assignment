using ApiContracts.Comment;

namespace BlazorApp1.Services;

public interface ICommentService
{
    public Task<GetCommentsDto> GetCommentsAsync(int postId);
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request);
}