using System;

namespace DTOs;

public class CreateUserDto
{
    public CreateUserDto(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public required string UserName { get; set; }
    public required string Password { get; set; }
}
