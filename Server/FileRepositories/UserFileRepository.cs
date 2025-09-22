using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
        user.Id = maxId + 1;

        users.Add(user);

        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        int userIndex = 0;
        
        foreach (User userToChange in users)
        {
            if (userToChange.Id != user.Id)
            {
                break;
            }
            userIndex++;
        }
        
        users[userIndex] = user;

        usersAsJson = JsonSerializer.Serialize(users);

        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public async Task DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        int userIndex = 0;
        
        foreach (User userToChange in users)
        {
            if (userToChange.Id != id)
            {
                break;
            }
            userIndex++;
        }
        
        users.RemoveAt(userIndex);

        usersAsJson = JsonSerializer.Serialize(users);

        await File.WriteAllTextAsync(filePath, usersAsJson); 
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);

        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        User user = users.FirstOrDefault(user => user.Id == id);
        
        return user;
    }

    public IQueryable<User> GetAll()
    {
        string usersAsJson = File.ReadAllTextAsync(filePath).Result;
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.AsQueryable();
    }
}