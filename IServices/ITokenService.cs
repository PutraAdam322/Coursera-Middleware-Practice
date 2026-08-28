public interface ITokenService
{
    void LogCreation(string message);
    string GenerateToken(User user);
}