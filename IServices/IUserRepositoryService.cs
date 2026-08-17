public interface IUserRepositoryService
{
    void LogCreation(string message);
    public Task<User> Get(int id);
    public Task<bool> Insert(User user);
    public Task Remove(int id);
    public Task Edit(int id, User user);
    public Task<User?> Validate(string username, string password);
}