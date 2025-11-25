using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;


namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
private readonly AppContext ctx;
    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddAsync(User user)
    {
        await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new KeyNotFoundException("User with id {user.Id} not found");
        }
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found");
        }
        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    //GetSingle
    public async Task<User> GetSingleAsync(string username, string password)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        if (existing == null)
        {
            throw new KeyNotFoundException($"User with username {username} not found");
        }
        return existing;
    }
    public async Task<User?> GetSingleAsync(object username, string password)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Username == (string)username && u.Password == password);
        return existing;
    }
    public async Task<User> GetSingleAsync(int id)
    {
        User? existing = await ctx.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"User with id {id} not found");
        }
        return existing;
    }
    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }
    public async Task<IQueryable<User>> GetManyAsync()
    {
        return await Task.FromResult(GetMany());
    }
}

