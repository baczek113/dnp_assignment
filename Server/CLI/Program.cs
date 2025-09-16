using CLI.UI;
using Entities;
using FileRepositories;
using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("The CLI app is starting...");

IUserRepository userRepository = new UserFileRepository();

IPostRepository postRepository = new PostFileRepository();

ICommentRepository commentRepository = new CommentFileRepository();
    
CliApp.StartAsync(userRepository, postRepository, commentRepository);
