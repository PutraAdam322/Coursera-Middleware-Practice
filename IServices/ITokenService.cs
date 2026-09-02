public interface ITokenService
{
    void LogCreation(string message);
    Task<string> GenerateToken(User user);
    Task<int> GetUserIdFromToken(string token);
}