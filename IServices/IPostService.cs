public interface IPostService
{
    void LogCreation(string message);
    Task<Post> GetPostByIdAsync(int id);
    //Task<IEnumerable<Post>> GetAllPostsAsync();
    Task AddPostAsync(Post post);
    Task UpdatePostAsync(int id, Post post);
    Task DeletePostAsync(int id);
}