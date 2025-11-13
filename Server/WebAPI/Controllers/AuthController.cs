using System;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities;
using DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IResult> Login(
        [FromBody] CreateUserDto request
    )
    {
        User? user = await userRepository.GetSingleAsync(request.UserName, request.Password);

        if (user is null)
        {
            return Results.Unauthorized();
        }

        GetUserDto userDTO = new GetUserDto(user.Username, user.Id);

        return Results.Ok(userDTO);
    }
}