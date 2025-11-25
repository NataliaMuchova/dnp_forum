using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
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
        await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await ctx.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new KeyNotFoundException("Post with id {post.Id} not found");
        }
        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Post with id {id} not found");
        }
        ctx.Posts.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? existing = await ctx.Posts.Include(p => p.Comments).SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Post with id {id} not found");
        }
        return existing;
    }

    public IQueryable<Post> GetMany()
    {
        return ctx.Posts.AsQueryable();
    }
    public async Task<IQueryable<Post>> GetManyAsync()
    {
        return await Task.FromResult(GetMany());
    }
}
