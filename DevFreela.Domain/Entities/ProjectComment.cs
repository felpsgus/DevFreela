namespace DevFreela.Domain.Entities;

public class ProjectComment : Entity
{
    protected ProjectComment()
    {
    }

    public ProjectComment(string content, long idProject, long idUser)
    {
        Content = content;
        IdProject = idProject;
        IdUser = idUser;
    }

    public string Content { get; private set; }
    public long IdProject { get; private set; }
    public Project? Project { get; private set; }
    public long IdUser { get; private set; }
    public User? User { get; private set; }
}