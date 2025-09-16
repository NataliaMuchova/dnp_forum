using System;
using Entities;


namespace Contracts;

public interface ICommentRepository
{
  Task<Comment> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
    Task<Comment> GetSingleAsync(int id);
    IQueryable<Comment> GetManyAsync();
    Task<IEnumerable<Comment>> GetManyAsync(int postId);
}