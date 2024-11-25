namespace DevFreela.Application.Models;

public class ProjectCommentViewModel
{
    public ProjectCommentViewModel(string content, string commentedBy)
    {
        Content = content;
        CommentedBy = commentedBy;
    }

    public string Content { get; set; }
    public string CommentedBy { get; set; }
}