using System;
using DTOs;

namespace BlazorApp.Servicies;

public interface ICommentService
{
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request);
    public Task UpdateCommentAsync(int id, UpdateCommentDto request);
    public Task<CommentDto> GetCommentByIdAsync(int id);
    public Task<List<CommentDto>> GetCommentsAsync();
    public Task DeleteCommentAsync(int id);
}
