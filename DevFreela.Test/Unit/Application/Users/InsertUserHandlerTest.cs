using DevFreela.Application.Users.Commands.InsertUser;
using DevFreela.Domain.Entities;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Users;

public class InsertUserHandlerTest
{
    [Fact]
    public async Task ShouldReturnIdWhenUserIsInserted()
    {
        // Arrange
        var fakeUser = FakeDataHelper.GetFakeUser();

        var userRepository = new Mock<IUserRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        userRepository
            .Setup(ur => ur.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback((User user, CancellationToken cancellationToken) => user.Id = fakeUser.Id)
            .Returns(Task.CompletedTask);

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var insertUserHandler = new InsertUserHandler(userRepository.Object, unitOfWork.Object);
        var insertUserCommand = new InsertUserCommand(fakeUser.FullName, fakeUser.Email, fakeUser.BirthDate, null);

        // Act
        var result = await insertUserHandler.Handle(insertUserCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().Be(fakeUser.Id);
    }
}