using ApiContracts;
using ApiContracts.Post;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostRepository postRepository;

    public PostController(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    [HttpPost] 
    public async Task<ActionResult<PostDto>> AddPost([FromBody] CreatePostDto request) {
        try
        {
            var post = new Post
            {
                Title = request.Title,
                Body = request.Body,
                AuthorId = request.AuthorId
            };
            Post created = await postRepository.AddAsync(post);
            PostDto dto = new()
            {
                Id = created.Id,
                Title = created.Title,
                Body = created.Body,
                AuthorId = created.AuthorId
            };
            return Created($"/posts/{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet] 
    public IResult GetPosts() {
        try
        {
            List<PostDto> posts = new();
            foreach(Post post in postRepository.GetAll())
            {
                posts.Add(new PostDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    AuthorId = post.AuthorId
                });
            }
            var dto = new GetPostsDto
            {
                Posts = posts
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
    public async Task<IResult> UpdatePost([FromBody] PostDto request) {
        try
        {
            var post = new Post
            {
                Title = request.Title,
                Body = request.Body,
                AuthorId = request.AuthorId,
                Id = request.Id
            };
            await postRepository.UpdateAsync(post);
            return Results.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem(e.Message, statusCode: 500);
        }
    }
    
    [HttpDelete] 
    public async Task<IResult> DeletePost([FromBody] UserIdDto request) {
        try
        {
            await postRepository.DeleteAsync(request.Id);
            return Results.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem(e.Message, statusCode: 500);
        }
    }
    
    [HttpGet("{id}")] 
    public async Task<IResult> GetPost(int id) {
        try
        {
            Post post = await postRepository.GetSingleAsync(id);
            PostDto postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                AuthorId = post.AuthorId
            };
            return Results.Ok(postDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem();
        }
    }
}