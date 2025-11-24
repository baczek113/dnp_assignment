using Entities;
using RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppContext ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<Post> AddAsync(Post post)
    {
        Console.WriteLine("Db connection: " + ctx.Database.GetDbConnection().ConnectionString);
        await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new Exception($"Post with id {post.Id} not found");
        }
        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        } 
        ctx.Posts.Remove(existing); 
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? post = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        return post;
    }

    public IQueryable<Post> GetAll()
    {
        return ctx.Posts.AsQueryable();
    }
}