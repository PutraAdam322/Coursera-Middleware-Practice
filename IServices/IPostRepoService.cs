public interface IPostRepositoryService
{
    void LogCreation(string message);
    Task<List<Post>> GetAll();
    Task<Post> Get(int id);
    Task Add(Post post, int userId);
    Task Remove(int id, int userId);
    Task Edit(int id, PostDTO post);
}