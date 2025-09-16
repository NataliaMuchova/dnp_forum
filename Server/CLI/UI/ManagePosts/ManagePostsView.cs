using System;
using System.Runtime.CompilerServices;
using Contracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository postRepository;
    public ManagePostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    private async Task displayPostAsync()
    {
        var values = await postRepository.GetManyAsync();
        foreach (Post post in values)
        {
            Console.WriteLine($"Post ID: {post.Id}, Body: {post.Body}, UserID: {post.UserId}");
        }
    }

    public async Task StartAsync()
    {
        Console.WriteLine("=== Manage Posts ===");
        await displayPostAsync();
    }
}
