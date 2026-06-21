using System;
using System.Threading.Tasks;

public class UserRepositoryService : IService
{
    private readonly int _serviceId;
    private List<User> Users = new List<User>{};
    private int idCounter = 0;

    public UserRepositoryService()
    {
        _serviceId = new Random().Next(100000,999999);
        LogCreation($"Message: UserRepositoryService {_serviceId} created.");
    }

    public async Task<User?> Get(int id)
    {
        return Users.Find(u => u.Id == id);
    }

    public async Task<bool> Insert(User user)
    {
        if (Users.Contains(user)) return false;
        idCounter++;
        user.SetId(idCounter);
        Users.Add(user);
        return true;
    }

    public async Task Remove(int id)
    {
        Users.RemoveAll(u => u.Id==id);
    }

    public async Task Edit(int id, User user)
    {
        Users.Find(u => u.Id==id).Username = user.Username;
        Users.Find(u => u.Id==id).Password = user.Password;
    }

    public void LogCreation(string message){
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }
    
}