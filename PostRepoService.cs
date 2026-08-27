public class PostRepositoryService : IPostRepositoryService
{
    private readonly List<Post> _posts = new List<Post>();

    public void LogCreation(string message)
    {
        Console.WriteLine($"Post created: {message}");
    }

    public Task<List<Post>> GetAllAsync()
    {
        return Task.FromResult(_posts);
    }

    public Task<Post> Get(int id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post != null)
        {
            Console.WriteLine($"Post found: {post.Title}");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
        return Task.FromResult(post);
    }

    public Task Add(Post post)
    {
        _posts.Add(post);
        LogCreation(post.Title);
        return Task.CompletedTask;
    }

    public Task Remove(int id)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post != null)
        {
            _posts.Remove(post);
            Console.WriteLine($"Post removed: {post.Title}");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
        return Task.CompletedTask;
    }

    public Task Edit(int id, Post post)
    {
        var existingPost = _posts.FirstOrDefault(p => p.Id == id);
        if (existingPost != null)
        {
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            Console.WriteLine($"Post updated: {existingPost.Title}");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
        return Task.CompletedTask;
    }
}