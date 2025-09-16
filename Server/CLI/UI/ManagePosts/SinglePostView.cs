using System;
using Contracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    private async Task DisplayPostAsync(int postId)
    {
        Post post = await postRepository.GetSingleAsync(postId);
        Console.WriteLine($"Post ID: {post.Id}, Title: {post.Title}, Body: {post.Body}, UserID: {post.UserId}");
        var comments = await commentRepository.GetManyAsync(postId);
        foreach (Comment comment in comments)
        {
            Console.WriteLine($"Comment ID: {comment.Id}, Body: {comment.Body}, UserID: {comment.UserId}");
        }
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Enter Post Id: ");
        int postId = int.Parse(Console.ReadLine());

        await DisplayPostAsync(postId);
    }

}
