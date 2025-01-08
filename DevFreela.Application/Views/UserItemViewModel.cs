namespace DevFreela.Application.Views;

public class UserItemViewModel
{
    public UserItemViewModel(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
}