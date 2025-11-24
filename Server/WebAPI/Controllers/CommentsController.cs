using ApiContracts;
using ApiContracts.Comment;
using ApiContracts.Post;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
    [HttpPost] 
    public async Task<ActionResult<CommentDto>> AddComment([FromBody] CreateCommentDto request) {
        try
        {
            var comment = new Comment
            {
                PostId = request.PostId,
                Body = request.Body,
                UserId = request.AuthorId
            };
            Comment created = await commentRepository.AddAsync(comment);
            CommentDto dto = new()
            {
                Id = created.Id,
                Body = created.Body,
                PostId = created.PostId,
                AuthorId = created.UserId
            };
            return Created($"/comments/{dto.Id}", dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet] 
    public IResult GetComments() {
        try
        {
            List<CommentDto> comments = new();
            foreach(Comment comment in commentRepository.GetAll())
            {
                comments.Add(new CommentDto
                {
                    Id = comment.Id,
                    PostId = comment.PostId,
                    Body = comment.Body,
                    AuthorId = comment.UserId
                });
            }
            var dto = new GetCommentsDto
            {
                Comments = comments
            };
            return Results.Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.InternalServerError(e);
        }
    }
    
    [HttpPut] 
    public async Task<IResult> UpdateComment([FromBody] CommentDto request) {
        try
        {
            var post = new Comment
            {
                PostId = request.PostId,
                Body = request.Body,
                UserId = request.AuthorId,
                Id = request.Id
            };
            await commentRepository.UpdateAsync(post);
            return Results.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem(e.Message, statusCode: 500);
        }
    }
    
    [HttpDelete] 
    public async Task<IResult> DeleteComment([FromBody] CommentIdDto request) {
        try
        {
            await commentRepository.DeleteAsync(request.Id);
            return Results.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem(e.Message, statusCode: 500);
        }
    }
    
    [HttpGet("{id}")] 
    public async Task<IResult> GetComment(int id) {
        try
        {
            Comment comment = await commentRepository.GetSingleAsync(id);
            CommentDto commentDto = new CommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                Body = comment.Body,
                AuthorId = comment.UserId
            };
            return Results.Ok(commentDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem();
        }
    }
    
    [HttpGet("forPost/{postId}")] 
    public async Task<IResult> GetComments(int postId) {
        try
        {
            List<CommentDto> comments = new();
            List<Comment> commentsQuery = await commentRepository.GetAllForPostAsync(postId);
            foreach(Comment comment in commentsQuery)
            {
                comments.Add(new CommentDto
                {
                    Id = comment.Id,
                    PostId = comment.PostId,
                    Body = comment.Body,
                    AuthorId = comment.UserId
                });
            }
            var dto = new GetCommentsDto
            {
                Comments = comments
            };
            return Results.Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Results.Problem(
                detail: e.Message, 
                statusCode: 500,
                title: "An unexpected error occurred"
            );
        }
    }
}