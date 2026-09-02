public interface IUserRepositoryService
{
    void LogCreation(string message);
    public Task<User> Get(int id);
    public Task<bool> Insert(User user);
    public Task Remove(int id);
    public Task Edit(int id, User user);
    public Task<List<User>> GetAll();
    public Task AssignPosts(int userId, Post post);
    public Task RemovePosts(int userId, Post post);
    public Task<User?> Validate(string username, string password);
}