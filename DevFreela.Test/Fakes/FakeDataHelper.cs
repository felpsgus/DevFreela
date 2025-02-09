using Bogus;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;

namespace DevFreela.Test.Fakes;

public static class FakeDataHelper
{
    public static Faker Faker => new Faker();

    private static readonly Faker<Project> ProjectFaker = new Faker<Project>()
        .CustomInstantiator(f => new Project(
            f.Commerce.ProductName(),
            f.Lorem.Paragraph(),
            f.Random.Int(1, 10),
            f.Random.Int(1, 10),
            f.Random.Decimal(100, 10000))
        );

    public static Project GetFakeProject() => ProjectFaker.Generate();

    private static readonly Faker<User> UserFaker = new Faker<User>()
        .CustomInstantiator(f => new User(
                f.Name.FirstName(),
                f.Internet.Email(),
                f.Date.BetweenDateOnly(DateOnly.FromDateTime(DateTime.Now.AddYears(-50)),
                    DateOnly.FromDateTime(DateTime.Now.AddYears(-18))),
                f.Random.String(8),
                [RoleEnum.Client, RoleEnum.Freelancer]
            )
        )
        .RuleFor(u => u.Id, f => f.Random.Int(1, 200))
        .RuleFor(u => u.Skills, f => GetFakeSkills(f.Random.Int(1, 5)))
        .RuleFor(u => u.UserSkills, f => GetFakeUserSkills(f.Random.Int(1, 5)));

    public static User GetFakeUser() => UserFaker.Generate();

    private static readonly Faker<Skill> SkillFaker = new Faker<Skill>()
        .CustomInstantiator(f => new Skill(f.Lorem.Word()))
        .RuleFor(s => s.Id, f => f.Random.Int(1, 200));

    public static Skill GetFakeSkill() => SkillFaker.Generate();

    public static List<Skill> GetFakeSkills(int count) => SkillFaker.Generate(count);

    private static readonly Faker<UserSkill> UserSkillFaker = new Faker<UserSkill>()
        .CustomInstantiator(f => new UserSkill(f.Random.Int(1, 200), f.Random.Int(1, 200)));

    public static List<UserSkill> GetFakeUserSkills(int count) => UserSkillFaker.Generate(count);
}