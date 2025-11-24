using ApiContracts;
using ApiContracts.Auth;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost] 
    public async Task<IResult> AuthenticateLogin([FromBody] LoginRequest request) {
        try
        {
            foreach(User user in userRepository.GetAll())
            {
                if (user.Username == request.Username && user.Password == request.Password)
                {
                    ReturnUserDto dto = new()
                    {
                        Id = user.Id,
                        Username = user.Username
                    };
                    return Results.Ok(dto);
                }
            }
            return Results.Unauthorized();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.BadRequest();
        }
    }
}