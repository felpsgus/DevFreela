using DevFreela.Application.Users.Commands.UpdateUser;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Users;

public class UpdateUserHandlerTest
{
    [Fact]
    public async Task ShouldUpdateUser()
    {
        // Arrange
        var fakeUser = FakeDataHelper.GetFakeUser();
        var newFakeUser = FakeDataHelper.GetFakeUser();

        var userRepository = new Mock<IUserRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        userRepository
            .Setup(ur => ur.GetByIdAsync(fakeUser.Id, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeUser);

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var userSkills = FakeDataHelper.GetFakeUserSkills(3);
        var updateUserHandler = new UpdateUserHandler(userRepository.Object, unitOfWork.Object);
        var updateUserCommand = new UpdateUserCommand()
        {
            Id = fakeUser.Id,
            Name = newFakeUser.FullName,
            Email = newFakeUser.Email,
            Skills = userSkills.Select(s => s.SkillId).ToHashSet()
        };

        // Act
        var result = await updateUserHandler.Handle(updateUserCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        fakeUser.FullName.Should().Be(newFakeUser.FullName);
        fakeUser.Email.Should().Be(newFakeUser.Email);
        fakeUser.UserSkills.Count.Should().Be(3);
        fakeUser.UserSkills.Select(us => us.SkillId).Should().Equal(userSkills.Select(s => s.SkillId));
    }
}