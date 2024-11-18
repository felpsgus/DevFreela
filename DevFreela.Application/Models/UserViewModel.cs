namespace DevFreela.Application.Models;

public class UserViewModel
{
    public UserViewModel(long id, string fullName, string email, DateTime birthDate)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
    }

    public long Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
}