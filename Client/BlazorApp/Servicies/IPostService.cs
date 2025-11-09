using System;
using DTOs;

namespace BlazorApp.Servicies;

public interface IPostService
{
    public Task<PostDto> AddPostAsync(CreatePostDto request);

    public Task UpdatePostAsync(int id, UpdatePostDto request);
    public Task<PostDto> GetPostByIdAsync(int id);
    public Task<List<PostDto>> GetPostsAsync();
    public Task DeletePostAsync(int id);
}
