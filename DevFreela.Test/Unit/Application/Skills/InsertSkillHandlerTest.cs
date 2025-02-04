using DevFreela.Application.Skills.Commands.InsertSkill;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Skills;

public class InsertSkillHandlerTest
{
    [Fact]
    public async Task ShouldReturnIdWhenSkillIsCreated()
    {
        // Arrange
        var repository = new Mock<ISkillRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var skill = FakeDataHelper.GetFakeSkill();
        var id = FakeDataHelper.Faker.Random.Int();

        repository
            .Setup(pr => pr.AddAsync(It.IsAny<Skill>(), It.IsAny<CancellationToken>()))
            .Callback((Skill skill, CancellationToken cancellationToken) => skill.Id = id)
            .Returns(Task.CompletedTask);

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new InsertSkillHandler(repository.Object, unitOfWork.Object);
        var command = new InsertSkillCommand(skill.Description);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().Be(id);
    }
}