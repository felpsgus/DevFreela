using DevFreela.Application.Skills.Commands.DeleteSkill;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Skills;

public class DeleteSkillHandlerTest
{
    [Fact]
    public async Task ShouldDeleteSkill()
    {
        // Arrange
        var fakeSkill = FakeDataHelper.GetFakeSkill();

        var repository = new Mock<ISkillRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        repository
            .Setup(ur => ur.GetByIdAsync(fakeSkill.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeSkill);
        repository
            .Setup(ur => ur.Delete(fakeSkill))
            .Callback((Skill user) => user.Delete());

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteSkillHandler(repository.Object, unitOfWork.Object);
        var command = new DeleteSkillCommand(fakeSkill.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        fakeSkill.Deleted.Should().BeTrue();
    }
}