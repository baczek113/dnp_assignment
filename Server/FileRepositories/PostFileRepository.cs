using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string filePath = "posts.json";
    
    public PostFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Post> AddAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        int maxId = posts.Count > 0 ? posts.Max(c => c.Id) : 1;

        post.Id = maxId + 1;

        posts.Add(post);

        postsAsJson = JsonSerializer.Serialize(post);

        await File.WriteAllTextAsync(filePath, postsAsJson);

        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        int postIndex = 0;
        
        foreach (Post postToChange in posts)
        {
            if (postToChange.Id != post.Id)
            {
                break;
            }
            postIndex++;
        }
        
        posts[postIndex] = post;

        postsAsJson = JsonSerializer.Serialize(posts);

        await File.WriteAllTextAsync(filePath, postsAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        
        int postIndex = 0;
        
        foreach (Post postToChange in posts)
        {
            if (postToChange.Id != id)
            {
                break;
            }
            postIndex++;
        }
        
        posts.RemoveAt(postIndex);

        postsAsJson = JsonSerializer.Serialize(posts);

        await File.WriteAllTextAsync(filePath, postsAsJson); 
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        string postsAsJson = await File.ReadAllTextAsync(filePath);

        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;

        Post post = posts.FirstOrDefault(post => post.Id == id);
        
        return post;
    }

    public IQueryable<Post> GetAll()
    {
        string postsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        return posts.AsQueryable();
    }
}