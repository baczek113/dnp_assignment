using System.Text.Json;
using ApiContracts;
using ApiContracts.Post;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Services;

public class HttpPostService : IPostService
{
    private readonly HttpClient client;

    public HttpPostService(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<PostDto> AddPostAsync(CreatePostDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("post", request); 
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        } 
        return JsonSerializer.Deserialize<PostDto>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<GetPostsDto> GetPostsAsync()
    {
        HttpResponseMessage httpResponse = client.GetAsync("post").Result;
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<GetPostsDto>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}