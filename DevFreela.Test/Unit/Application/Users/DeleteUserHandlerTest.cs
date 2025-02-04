using DevFreela.Application.Users.Commands.DeleteUser;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Users;

public class DeleteUserHandlerTest
{
    [Fact]
    public async Task ShouldDeleteUser()
    {
        // Arrange
        var fakeUser = FakeDataHelper.GetFakeUser();

        var userRepository = new Mock<IUserRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        userRepository
            .Setup(ur => ur.GetByIdAsync(fakeUser.Id, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeUser);
        userRepository
            .Setup(ur => ur.Delete(fakeUser))
            .Callback((User user) => user.Delete());

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var userSkills = FakeDataHelper.GetFakeUserSkills(3);
        var handler = new DeleteUserHandler(userRepository.Object, unitOfWork.Object);
        var command = new DeleteUserCommand(fakeUser.Id);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        fakeUser.Deleted.Should().BeTrue();
    }
}