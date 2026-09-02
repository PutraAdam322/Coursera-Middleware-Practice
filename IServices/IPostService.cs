public interface IPostService
{
    void LogCreation(string message);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task<Post> GetPostByIdAsync(int id);
    //Task<IEnumerable<Post>> GetAllPostsAsync();
    Task AddPostAsync(Post post, int userId);
    Task UpdatePostAsync(int id, PostDTO post);
    Task DeletePostAsync(int id, int userId);
}