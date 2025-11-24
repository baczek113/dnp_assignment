using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private ICommentRepository commentRepository;

    public ManageCommentsView(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }
    
    public void startView()
    {
        Console.WriteLine();
        Console.WriteLine("To exit the manage comments view type EXIT");
        Console.WriteLine("Add Comment - 1");
    }
    
    public async Task<Comment> AddAsync()
    {
        Comment comment = new();
        Console.WriteLine("To Stop adding comment type STOP");
        Console.WriteLine("Enter Body");
        comment.Body = Console.ReadLine();
        if (comment.Body == "STOP")
        {
            return comment;
        }
        Console.WriteLine("Enter Post Id");
        string postIdInput = Console.ReadLine();
        if (postIdInput == "STOP")
        {
            return comment;
        }
        comment.PostId = int.Parse(postIdInput);
        Console.WriteLine("Enter Author Id");
        string authorIdInput = Console.ReadLine();
        if (authorIdInput == "STOP")
        {
            return comment;
        }
        comment.UserId = int.Parse(authorIdInput);
        comment =  await commentRepository.AddAsync(comment);
        Console.WriteLine("Comment Added Successfully");
        return comment;
    }
}