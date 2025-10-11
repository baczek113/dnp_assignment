using ApiContracts;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public UsersController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    [HttpPost] 
    public async Task<ActionResult<ReturnUserDto>> AddUser([FromBody] CreateUserDto request) {
        try
        {
            User user = new(request.Username, request.Password);
            User created = await userRepository.AddAsync(user);
            ReturnUserDto dto = new()
            {
                Id = created.Id,
                Username = created.Username
            };
            return Created($"/users/{dto.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet] 
    public IResult GetUsers() {
        try
        {
            GetUsersDto dto = new();
            List<ReturnUserDto> users = new();
            foreach(User user in userRepository.GetAll())
            {
                users.Add(new ReturnUserDto
                {
                    Id = user.Id,
                    Username = user.Username
                });
            }
            dto.Users = users;
            return Results.Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.InternalServerError(e);
        }
    }
    
    [HttpPut] 
    public async Task<IResult> UpdateUser([FromBody] UserDto request) {
        try
        {
            User user = new(request.Username, request.Password);
            user.Id = request.Id;
            await userRepository.UpdateAsync(user);
            return Results.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem(e.Message, statusCode: 500);
        }
    }
    
    [HttpDelete] 
    public async Task<IResult> DeleteUser([FromBody] UserIdDto request) {
        try
        {
            await userRepository.DeleteAsync(request.Id);
            return Results.Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem(e.Message, statusCode: 500);
        }
    }
    
    [HttpGet("{id}")] 
    public async Task<IResult> GetUser(int id) {
        try
        {
            User user = await userRepository.GetSingleAsync(id);
            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password
            };
            return Results.Ok(userDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
            return Results.Problem();
        }
    }
}