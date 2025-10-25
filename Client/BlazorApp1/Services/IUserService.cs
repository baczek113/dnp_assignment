using ApiContracts;

namespace BlazorApp1.Services;

public interface IUserService
{
    public Task<ReturnUserDto> AddUserAsync(CreateUserDto request);
    public IResult GetUsers();
    public Task<IResult> UpdateUserAsync();
    public Task<IResult> DeleteUserAsync();
    public Task<UserDto> GetUserAsync(int userId);
}