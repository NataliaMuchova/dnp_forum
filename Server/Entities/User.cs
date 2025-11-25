using System;

namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public User() { } //Json
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    // for database navigation property
    public List<Post> Posts { get; set; }
}
