using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private IPostRepository postRepository;

    public ManagePostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public void startView()
    {
        Console.WriteLine();
        Console.WriteLine("To exit the manage posts view type EXIT");
        Console.WriteLine("Add Post - 1");
        Console.WriteLine("View Posts Overview - 2");
        Console.WriteLine("View Specific Post - 3");
    }
    
    public async Task<Post> AddAsync()
    {
        Post post = new();
        Console.WriteLine("To Stop adding post type STOP");
        Console.WriteLine("Enter Title");
        post.Title = Console.ReadLine();
        if (post.Title == "STOP")
        {
            return post;
        }
        Console.WriteLine("Enter Body");
        post.Body = Console.ReadLine();
        if (post.Body == "STOP")
        {
            return post;
        }
        Console.WriteLine("Enter Author Id");
        string idInput = Console.ReadLine();
        if (idInput == "STOP")
        {
            return post;
        }
        post.UserId = int.Parse(idInput);
        post =  await postRepository.AddAsync(post);
        Console.WriteLine("Post Added Successfully");
        return post;
    }
    
    public async Task GetAllAsync()
    {
        var posts = postRepository.GetAll();
        Console.WriteLine("All posts:");
        foreach (var post in posts)
        {
            Console.WriteLine($"Id: {post.Id}, Title: {post.Title}");
        }
    }

    public async Task GetSingleAsync(ICommentRepository commentRepository)
    {
        Console.WriteLine("What post would you like to get?");
        var postId = int.Parse(Console.ReadLine());
        var post = await postRepository.GetSingleAsync(postId);
        Console.WriteLine($"Post ID: {post.Id}, Title: {post.Title}, Body: {post.Body}");
        Console.WriteLine("Comments:");
        foreach(Comment comment in commentRepository.GetAll())
        {
            if (comment.PostId == postId)
            {
                Console.WriteLine($"AuthorId: {comment.UserId}, Body: {comment.Body}");
            }
        }
        return;
    }
}