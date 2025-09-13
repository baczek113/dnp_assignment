using Entities;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView
{
    private IUserRepository userRepository;
    public ManageUsersView(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void startView()
    {
        Console.WriteLine();
        Console.WriteLine("To exit the manage users view type EXIT");
        Console.WriteLine("Add User - 1");
        Console.WriteLine("View Users Overview - 2");
    }
    
    public async Task<User> AddAsync()
    {
        User user = new User();
        Console.WriteLine("To Stop adding user type STOP");
        Console.WriteLine("Enter Username");
        user.Username = Console.ReadLine();
        if (user.Username == "STOP")
        {
            return user;
        }
        Console.WriteLine("Enter Password");
        user.Password = Console.ReadLine();
        if (user.Password == "STOP")
        {
            return user;
        }
        user =  await userRepository.AddAsync(user);
        Console.WriteLine("User Added Successfully");
        return user;
    }
    
    public async Task GetAllAsync()
    {
        IQueryable<User> users = userRepository.GetAllAsync();
        Console.WriteLine("Currently registered users:");
        foreach (var user in users)
        {
            Console.WriteLine($"Id: {user.Id}, Username: {user.Username}");
        }
    }
}