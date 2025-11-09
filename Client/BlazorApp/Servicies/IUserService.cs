using System;
using DTOs;

namespace BlazorApp.Servicies;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto request);
    public Task UpdateUserAsync(int id, UpdateUserDto request);
    public Task<GetUserDto> GetUserByIdAsync(int id);
    public Task<List<GetUserDto>> GetUsersAsync();
    public Task DeleteUserAsync(int id);
}
