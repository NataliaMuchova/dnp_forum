using System;
using Contracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;
    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

}
