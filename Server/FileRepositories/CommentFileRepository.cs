using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository :  ICommentRepository
{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);

        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 1;

        comment.Id = maxId + 1;

        comments.Add(comment);

        commentsAsJson = JsonSerializer.Serialize(comments);

        await File.WriteAllTextAsync(filePath, commentsAsJson);

        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);

        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        int commentIndex = 0;
        
        foreach (Comment commentToChange in comments)
        {
            if (commentToChange.Id == comment.Id)
            {
                break;
            }
            commentIndex++;
        }
        
        comments[commentIndex] = comment;

        commentsAsJson = JsonSerializer.Serialize(comments);

        await File.WriteAllTextAsync(filePath, commentsAsJson); 
    }

    public async Task DeleteAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);

        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        int commentIndex = 0;
        
        foreach (Comment commentToChange in comments)
        {
            if (commentToChange.Id == id)
            {
                break;
            }
            commentIndex++;
        }
        
        comments.RemoveAt(commentIndex);

        commentsAsJson = JsonSerializer.Serialize(comments);

        await File.WriteAllTextAsync(filePath, commentsAsJson); 
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);

        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;

        Comment comment = comments.FirstOrDefault(comment => comment.Id == id);
        
        return comment;
    }

    public IQueryable<Comment> GetAll()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
    
    public IQueryable<Comment> GetAllForPost(int postId)
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        List<Comment> filteredComments = comments.Where(comment => comment.PostId == postId).ToList();
        return filteredComments.AsQueryable();
    }
}