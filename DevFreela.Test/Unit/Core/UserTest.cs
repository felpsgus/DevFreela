using DevFreela.Domain.Entities;
using DevFreela.Domain.Exceptions;

namespace DevFreela.Test.Unit.Core;

public class UserTest
{
    [Fact]
    public void UserIsCreated_Update_Success()
    {
        // Arrange
        var user = new User("User Test", "test@test", new DateOnly(1990, 1, 1));

        // Act
        user.Update("User Test Updated", "test@test", new List<long> { 1, 2, 3 });

        // Assert
        user.FullName.Should().Be("User Test Updated");
        user.UserSkills.Count.Should().Be(3);
    }

    [Fact]
    public void UserIsInactivated_Inactivate_Success()
    {
        // Arrange
        var user = new User("User Test", "test@test", new DateOnly(1990, 1, 1));

        // Act
        user.Inactivate();

        // Assert
        user.Active.Should().BeFalse();
    }

    [Fact]
    public void UserIsInactivated_Inactivate_ThrowException()
    {
        // Arrange
        var user = new User("User Test", "test@test", new DateOnly(1990, 1, 1));
        user.Inactivate();

        // Act
        Action inactivate = user.Inactivate;

        // Assert
        inactivate.Should().Throw<DomainException>().WithMessage("User is already inactive");
    }

    [Fact]
    public void UserIsInactivated_Activate_Success()
    {
        // Arrange
        var user = new User("User Test", "test@test", new DateOnly(1990, 1, 1));
        user.Inactivate();

        // Act
        user.Activate();

        // Assert
        user.Active.Should().BeTrue();
    }

    [Fact]
    public void UserIsActivated_Activate_ThrowException()
    {
        // Arrange
        var user = new User("User Test", "test@test", new DateOnly(1990, 1, 1));

        // Act
        Action activate = user.Activate;

        // Assert
        activate.Should().Throw<DomainException>().WithMessage("User is already active");
    }
}