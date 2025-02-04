using DevFreela.Domain.Entities;
using DevFreela.Domain.Enums;
using DevFreela.Domain.Exceptions;

namespace DevFreela.Test.Unit.Core;

public class ProjectTest
{
    [Fact]
    public void ProjectIsInProgress_Cancel_Success()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);
        project.Start();

        // Act
        project.Cancel();

        // Assert
        project.Status.Should().Be(ProjectStatusEnum.Cancelled);
    }

    [Fact]
    public void ProjectIsInInvalidStatus_Cancel_ThrowException()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);

        // Act
        Action cancel = project.Cancel;

        // Assert
        cancel.Should().Throw<DomainException>().WithMessage(Project.ProjectInvalidStatus);
    }

    [Fact]
    public void ProjectIsCretead_Start_Success()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);

        // Act
        project.Start();

        // Assert
        project.StartedAt.Should().NotBeNull();
        project.Status.Should().Be(ProjectStatusEnum.InProgress);
    }

    [Fact]
    public void ProjectIsInInvalidStatus_Start_ThrowException()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);
        project.Start();

        // Act
        Action start = project.Start;

        // Assert
        start.Should().Throw<DomainException>().WithMessage(Project.ProjectInvalidStatus);
    }

    [Fact]
    public void ProjectIsInProgress_Complete_Success()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);
        project.Start();

        // Act
        project.Complete();

        // Assert
        project.CompletedAt.Should().NotBeNull();
        project.Status.Should().Be(ProjectStatusEnum.Completed);
    }

    [Fact]
    public void ProjectIsInPaymentPendingStatus_Complete_Success()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);
        project.Start();
        project.SetPaymentPending();

        // Act
        project.Complete();

        // Assert
        project.CompletedAt.Should().NotBeNull();
        project.Status.Should().Be(ProjectStatusEnum.Completed);
    }

    [Fact]
    public void ProjectIsInInvalidStatus_Complete_ThrowException()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);

        // Act
        Action complete = project.Complete;

        // Assert
        complete.Should().Throw<DomainException>().WithMessage(Project.ProjectInvalidStatus);
    }

    [Fact]
    public void ProjectIsInInProgressStatus_SetPaymentPending_Success()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);
        project.Start();

        // Act
        project.SetPaymentPending();

        // Assert
        project.Status.Should().Be(ProjectStatusEnum.PaymentPending);
    }

    [Fact]
    public void ProjectIsInInvalidStatus_SetPaymentPending_ThrowException()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);

        // Act
        Action setPaymentPending = project.SetPaymentPending;

        // Assert
        setPaymentPending.Should().Throw<DomainException>().WithMessage(Project.ProjectInvalidStatus);
    }

    [Fact]
    public void ProjectIsCreated_Update_Success()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);

        // Act
        project.Update("New Project Updated", "Description Updated", 20000.0m);

        // Assert
        project.Status.Should().Be(ProjectStatusEnum.Created);
        project.StartedAt.Should().BeNull();
        project.CompletedAt.Should().BeNull();
        project.Title.Should().Be("New Project Updated");
        project.Description.Should().Be("Description Updated");
        project.TotalCost.Should().Be(20000.0m);
        project.FreelancerId.Should().Be(2);
        project.ClientId.Should().Be(1);
    }

    [Fact]
    public void ProjectIsCreated_UpdateFreelancer_Success()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);

        // Act
        project.UpdateFreelancer(3);

        // Assert
        project.FreelancerId.Should().Be(3);
    }

    [Fact]
    public void ProjectIsCreated_AddComment_Success()
    {
        // Arrange
        var project = new Project("New Project", "Description", 1, 2, 10000.0m);
        var comment = new ProjectComment("New Comment", 1, 1);

        // Act
        project.AddComment(comment);

        // Assert
        Assert.Single(project.Comments);
        project.Comments.Should().ContainSingle();
    }
}