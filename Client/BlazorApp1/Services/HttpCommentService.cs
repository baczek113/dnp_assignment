using System.Text.Json;
using ApiContracts.Comment;

namespace BlazorApp1.Services;

public class HttpCommentService : ICommentService
{
    
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<GetCommentsDto> GetCommentsAsync(int postId)
    {
        HttpResponseMessage httpResponse = client.GetAsync("comment/forPost/"+postId).Result;
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<GetCommentsDto>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public async Task<CommentDto> AddCommentAsync(CreateCommentDto request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync("comment", request); 
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        } 
        return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }
}