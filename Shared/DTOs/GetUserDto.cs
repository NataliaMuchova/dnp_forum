using System;

namespace DTOs;

public class GetUserDto
{
public string Username { get; set; }
    public int Id { get; set; }
    public GetUserDto(string username, int id)
    {
        Username = username;
        Id = id;
    }
}
