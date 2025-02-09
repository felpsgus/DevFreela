using DevFreela.Domain.Entities;
using DevFreela.Domain.Exceptions;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Core;

public class UserTest
{
    [Fact]
    public void UserIsCreated_Update_Success()
    {
        // Arrange
        var user = FakeDataHelper.GetFakeUser();
        var userUpdate = FakeDataHelper.GetFakeUser();

        // Act
        user.Update(userUpdate.FullName, userUpdate.Email, userUpdate.Roles,
            userUpdate.UserSkills.Select(us => us.SkillId).ToList());

        // Assert
        user.FullName.Should().Be("User Test Updated");
        user.UserSkills.Count.Should().Be(3);
    }

    [Fact]
    public void UserIsInactivated_Inactivate_Success()
    {
        // Arrange
        var user = FakeDataHelper.GetFakeUser();

        // Act
        user.Inactivate();

        // Assert
        user.Active.Should().BeFalse();
    }

    [Fact]
    public void UserIsInactivated_Inactivate_ThrowException()
    {
        // Arrange
        var user = FakeDataHelper.GetFakeUser();
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
        var user = FakeDataHelper.GetFakeUser();
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
        var user = FakeDataHelper.GetFakeUser();

        // Act
        Action activate = user.Activate;

        // Assert
        activate.Should().Throw<DomainException>().WithMessage("User is already active");
    }
}