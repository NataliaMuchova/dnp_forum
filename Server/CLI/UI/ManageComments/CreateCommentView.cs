using System;
using Contracts;
using Entities;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commnetRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task AddCommentAsync(string body, int userId, int postId)
    {
        Comment created = await commentRepository.AddAsync(new Comment(body, userId, postId));
        Console.WriteLine($"Post '{created.Body}' created with ID: {created.UserId}");
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Enter comment body: ");
        string body = Console.ReadLine();

        Console.WriteLine("Enter user ID: ");
        int userId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter post ID: ");
        int postId = int.Parse(Console.ReadLine());

        await AddCommentAsync(body, userId, postId);
    }
}
