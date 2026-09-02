public class PostService : IPostService
{
    private readonly int _serviceId;
    private readonly IPostRepositoryService _postRepository;
    public PostService(IPostRepositoryService postRepository)
    {
        _serviceId = new Random().Next(100000,999999);
        _postRepository = postRepository;
        LogCreation($"Message: PostService {_serviceId} created.");
    }
    public void LogCreation(string message)
    {
        Console.WriteLine($"Post created: {message}");
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _postRepository.Get(id);
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await _postRepository.GetAll();
    }

    public async Task AddPostAsync(Post post, int userId)
    {
        await _postRepository.Add(post, userId);
    }

    public async Task UpdatePostAsync(int id, PostDTO post)
    {
        await _postRepository.Edit(id, post);
    }

    public async Task DeletePostAsync(int id, int userId)
    {
        await _postRepository.Remove(id, userId);
    }

}