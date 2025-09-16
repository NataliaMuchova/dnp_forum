using System;
using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using Contracts;

namespace CLI.UI;

public class CliApp
{
    private IUserRepository userRepository { get; }
    private IPostRepository postRepository { get; }
    private ICommentRepository commentRepository { get; }   

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    internal async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("1. Create Users");
            Console.WriteLine("2. Create Posts");
            Console.WriteLine("3. Create Comments");
            Console.WriteLine("4. View Post");
            Console.WriteLine("5. View Posts");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var createUsersView = new CreateUsersView(userRepository);
                    await createUsersView.StartAsync();
                    break;
                case "2":
                    var createPostView = new CreatePostView(postRepository);
                    await createPostView.StartAsync();
                    break;

                case "3":
                    var createComments = new CreateCommentView(commentRepository);
                    await createComments.StartAsync();
                    break;

                case "4":
                    var viewpost = new SinglePostView(postRepository, commentRepository);
                    await viewpost.StartAsync();
                    break;

                case "5":
                    var viewposts = new ManagePostsView(postRepository);
                    await viewposts.StartAsync();
                    break;
            }
        }
    }
}
