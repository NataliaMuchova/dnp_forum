using System;
using Entities;


namespace Contracts;

public interface IUserRepository
{
Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetSingleAsync(int id);
    Task<IQueryable<User>> GetManyAsync();
    Task<User> GetSingleAsync(string username, string password);
    Task<User?> GetSingleAsync(object username, string password);
}