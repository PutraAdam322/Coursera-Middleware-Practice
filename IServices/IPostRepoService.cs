public interface IPostRepositoryService
{
    void LogCreation(string message);
    Task<Post> Get(int id);
    Task Add(Post post);
    Task Remove(int id);
    Task Edit(int id, Post post);
}