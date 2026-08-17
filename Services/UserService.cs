using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly int _serviceId;
    private readonly UserRepositoryService _userRepositoryService;

    public UserService(UserRepositoryService userRepositoryService){
        _serviceId = new Random().Next(100000,999999);
        _userRepositoryService = userRepositoryService;
        LogCreation($"Message: UserService {_serviceId} created.");
    }

    public async Task<User?> GetUserAsync(int id)
    {
        var user = await _userRepositoryService.Get(id);
        if(user != null)
        {
            LogCreation("Message: Get user successful");
            return user;
        }
        LogCreation("Message: Get user failed");
        return null;
    }

    public async Task<bool> EditUserAsync(int id)
    {
        var user = await _userRepositoryService.Get(id);
        if(user != null)
        {
            await _userRepositoryService.Edit(id, user);
            LogCreation("Message: Edit user successful");
            return true;
        }
        LogCreation("Message: Get user failed");
        return false;
    }


    public async Task<bool> RegisterUserAsync(User user)
    {
        bool isReg = await _userRepositoryService.Insert(user);
        if(isReg)
        {
            LogCreation("Message: Register user successful");
            return true;
        }
        LogCreation("Message: Register user failed");
        return false;
    }
    public async Task<int?> LoginUserAsync(string username, string password)
    {
        var user = await _userRepositoryService.Validate(username, password);
        if(user != null)
        {
            LogCreation("Message: Login successful");
            return user.Id;
        }
        LogCreation("Message: Login failed");
        return null;
    }

    public void LogCreation(string message){
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }
}