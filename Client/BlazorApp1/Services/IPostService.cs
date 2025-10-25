using ApiContracts.Post;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Services;

public interface IPostService
{
    public Task<PostDto> AddPostAsync(CreatePostDto request);
    public Task<GetPostsDto> GetPostsAsync();
}