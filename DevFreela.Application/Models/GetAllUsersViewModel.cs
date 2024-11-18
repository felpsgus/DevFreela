namespace DevFreela.Application.Models;

public class GetAllUsersViewModel
{
    public GetAllUsersViewModel(long id, string fullName, DateTime birthDate)
    {
        Id = id;
        FullName = fullName;
        BirthDate = birthDate;
    }

    public long Id { get; private set; }
    public string FullName { get; private set; }
    public DateTime BirthDate { get; private set; }
}