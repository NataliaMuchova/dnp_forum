using System;
using Contracts;
using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    private async Task AddPostAsync(string title, string body, int userId)
    {
        User Created = await postRepository.AddAsync(new User(title, body, userId));
        Console.WriteLine($"Post '{Created.Title}' created with ID: {Created.Id}");
    }

    public async Task StartAsync()
    {
        Console.WriteLine("=== Create New Post ===");

        Console.WriteLine("Enter post title: ");
        string title = Console.ReadLine();

        Console.WriteLine("Enter post content: ");
        string body = Console.ReadLine();

        Console.WriteLine("Enter user ID: ");
        int userId = int.Parse(Console.ReadLine());

        await AddPostAsync(title, body, userId);
    }
    
}
