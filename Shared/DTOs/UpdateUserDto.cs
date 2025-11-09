using System;

namespace DTOs;

public class UpdateUserDto
{
public string Username { get; set; }
    public string Password { get; set; }

    public UpdateUserDto(string username, string password)
    {
        Username = username;
        Password = password;
      
    }
}
