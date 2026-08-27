public interface IHasherService
    {
        void LogCreation(string message);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }