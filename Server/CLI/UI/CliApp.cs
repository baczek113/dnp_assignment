using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;
using InMemoryRepositories;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public static async Task StartAsync(IUserRepository userRepository, IPostRepository postRepository,
        ICommentRepository commentRepository)
    {
        ManageUsersView manageUsersView = new(userRepository); 
        ManagePostsView managePostsView = new(postRepository);
        ManageCommentsView manageCommentsView = new(commentRepository);
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("Manage users - 1");
            Console.WriteLine("Manage posts - 2");
            Console.WriteLine("Manage comments - 3");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    while (true)
                    {
                        manageUsersView.startView();
                        string userInputUsers = Console.ReadLine();
                        if (userInputUsers == "EXIT")
                        {
                            break;
                        }
                        switch (userInputUsers)
                        {
                            case "1":
                                await manageUsersView.AddAsync();
                                break;
                            case "2":
                                await manageUsersView.GetAllAsync();
                                break;
                        }
                    }
                    break;
                case "2":
                    while (true)
                    {
                        managePostsView.startView();
                        string userInputPosts = Console.ReadLine();
                        if (userInputPosts == "EXIT")
                        {
                            break;
                        }
                        switch (userInputPosts)
                        {
                            case "1":
                                await managePostsView.AddAsync();
                                break;
                            case "2":
                                await managePostsView.GetAllAsync();
                                break;
                            case "3":
                                await managePostsView.GetSingleAsync(commentRepository);
                                break;
                        }
                    }
                    break;
                case "3":
                    while (true)
                    {
                        manageCommentsView.startView();
                        string userInputComments = Console.ReadLine();
                        if (userInputComments == "EXIT")
                        {
                            break;
                        }
                        switch (userInputComments)
                        {
                            case "1":
                                await manageCommentsView.AddAsync();
                                break;
                        }
                    }
                    break;
            }
        }
    }
}