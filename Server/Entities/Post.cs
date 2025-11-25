using System;

namespace Entities;

public class Post
{
   public int Id { get; set; }        
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public int UserId { get; set; }
    //foreign key


    public Post() { }
    public Post(string title, string body, int userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
    }

    //database naviagation property
    public User User { get; set;}
    public List<Comment> Comments { get; set; }
}
