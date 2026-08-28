public class PostService : IPostService
{
    public void LogCreation(string message)
    {
        Console.WriteLine($"Post created: {message}");
    }
    private readonly IPostRepositoryService _postRepository;

    public PostService(IPostRepositoryService postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _postRepository.Get(id);
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await _postRepository.GetAll();
    }

    public async Task AddPostAsync(Post post)
    {
        await _postRepository.Add(post);
    }

    public async Task UpdatePostAsync(int id, Post post)
    {
        await _postRepository.Edit(id, post);
    }

    public async Task DeletePostAsync(int id)
    {
        await _postRepository.Remove(id);
    }

}