using System;
using Contracts;
using Entities;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task AddCommentAsync(string body, int userId, int postId)
    {
        var comment = new Comment { Body = body, UserId = userId, PostId = postId };
        var created = await commentRepository.AddAsync(comment);
        Console.WriteLine($"Comment '{created.Body}' created with ID: {created.Id}");
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Enter comment body: ");
        string? body = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter user ID: ");
        if (!int.TryParse(Console.ReadLine(), out var userId))
        {
            Console.WriteLine("Invalid user id.");
            return;
        }

        Console.WriteLine("Enter post ID: ");
        if (!int.TryParse(Console.ReadLine(), out var postId))
        {
            Console.WriteLine("Invalid post id.");
            return;
        }

        await AddCommentAsync(body, userId, postId);
    }
}
