using System;

namespace Entities;

public class Post
{
    private string content;

    public Post(string title, string body, int userId)
    {
        Title = title;
        Body = body;
        UserId = userId;
        Id = ID++;
    }


    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }

    private static int ID = 0;
}
