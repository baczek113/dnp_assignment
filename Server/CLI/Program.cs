using CLI.UI;
using FileRepositories;
using RepositoryContracts;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("The CLI app is starting...");

        IUserRepository userRepository = new UserFileRepository();
        IPostRepository postRepository = new PostFileRepository();
        ICommentRepository commentRepository = new CommentFileRepository();

        await CliApp.StartAsync(userRepository, postRepository, commentRepository);
    }
}