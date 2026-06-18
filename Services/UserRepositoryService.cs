using System;
using System.Linq;

public class UserRepositoryService : IService
{
    private readonly int _serviceId;
    private List<User> Users = new List<User>{};
    private int idCounter = 0;

    public UserRepositoryService()
    {
        _serviceId = new Random().Next(100000,999999);
    }

    public User GetUser(int id)
    {
        return Users.Find(u => u.Id == id);
    }

    public void InsertUser(User user)
    {
        idCounter++;
        user.SetId(idCounter);
        Users.Add(user);
    }

    public void RemoveUser(int id)
    {
        Users.RemoveAll(u => u.Id==id);
    }

    public void EditUser(int id, User user)
    {
        Users.Find(u => u.Id==id).Username = user.Username;
    }

    public void LogCreation(string message){
        Console.WriteLine($"{message} - Service ID: {_serviceId}");
    }
    
}