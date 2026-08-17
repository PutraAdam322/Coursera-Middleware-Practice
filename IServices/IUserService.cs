public interface IUserService
{
    void LogCreation(string message);
    public Task<User?> GetUserAsync(int id);
    public Task<bool> EditUserAsync(int id);
    public Task<bool> RegisterUserAsync(User user);
    public Task<int?> LoginAsync(string username, string password);
}