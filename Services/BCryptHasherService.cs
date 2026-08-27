using BCrypt.Net;

public class BCryptHasherService : IHasherService
 {

    private readonly int _serviceId;
    public BCryptHasherService()
    {
        _serviceId = new Random().Next(100000,999999);
        LogCreation($"Message: BCryptHasherService {_serviceId} created.");
    }

    public string HashPassword(string password)
    {
         return BCrypt.Net.BCrypt.HashPassword(password);
    }
 
    public bool VerifyPassword(string password, string hashedPassword)
    {
        Console.WriteLine($"Verifying password: {password} against hashed password: {hashedPassword}");
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

    public void LogCreation(string message){
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }
 }