using System.Text.Json;
using ApiContracts;

namespace BlazorApp1.Services;

public class HttpUserService : IUserService
{
    private readonly HttpClient client;

    public HttpUserService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ReturnUserDto> AddUserAsync(CreateUserDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("users", request); 
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        } 
        return JsonSerializer.Deserialize<ReturnUserDto>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public IResult GetUsers()
    {
        throw new NotImplementedException();
    }

    public Task<IResult> UpdateUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IResult> DeleteUserAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto> GetUserAsync(int userId)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"users/{userId}"); 
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        } 
        return JsonSerializer.Deserialize<UserDto>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}