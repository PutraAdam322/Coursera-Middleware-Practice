public class PostRepositoryService : IPostRepositoryService
{
    private readonly List<Post> _posts = new List<Post>();
    private int _idCounter = 0;
    private readonly int _serviceId;
    private readonly IUserRepositoryService _userRepositoryService;

    public PostRepositoryService(IUserRepositoryService userRepositoryService)
    {
        _serviceId = new Random().Next(100000, 999999);
        _userRepositoryService = userRepositoryService;
        LogCreation($"Message: PostRepositoryService {_serviceId} created.");
    }

    public async Task<List<Post>> GetAll()
    {
        return await Task.FromResult(_posts);
    }
    public void LogCreation(string message)
    {
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }

    public async Task<Post> Get(int id)
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
        return await Task.FromResult(post);
    }

    public async Task Add(Post post, int userId)
    {
        _idCounter++;
        post.SetId(_idCounter);
        _posts.Add(post);
        await _userRepositoryService.AssignPosts(userId, post);
        LogCreation(post.Title);
        return;
    }

    public async Task Remove(int id, int userId)
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post != null)
        {
            await _userRepositoryService.RemovePosts(userId, post);
            _posts.Remove(post);
            Console.WriteLine($"Post removed: {post.Title}");
        }
        else
        {
            Console.WriteLine("Post not found.");
        }
        return;
    }

    public async Task Edit(int id, PostDTO post)
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
            throw new InvalidOperationException($"Post with ID {id} not found.");
        }
        return;
    }
}