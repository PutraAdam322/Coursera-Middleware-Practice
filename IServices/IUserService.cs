public interface IUserService
{
    void LogCreation(string message);
    public Task<User?> GetUserAsync(int id);
    public Task<bool> EditUserAsync(int id);
    public Task<bool> RegisterUserAsync(UserDTO user);
    public Task<User?> LoginUserAsync(UserDTO userDTO);
    public Task<List<User>> GetAllUserAsync();
}