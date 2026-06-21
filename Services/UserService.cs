using System.Collections.Generic;
using System.Threading.Tasks;

public class UserService : IService
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
        User user = await _userRepositoryService.Get(id);
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
        User user = await _userRepositoryService.Get(id);
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
        var isReg = await _userRepositoryService.Insert(user);
        return isReg? true:false;
    }

    public void LogCreation(string message){
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }
}