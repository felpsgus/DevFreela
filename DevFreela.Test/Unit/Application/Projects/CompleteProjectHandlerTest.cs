using DevFreela.Application.Projects.Commands.CompleteProject;
using DevFreela.Domain.Enums;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Projects;

public class CompleteProjectHandlerTest
{
    [Fact]
    public async Task ShouldChangeProjectStatusToCompletedWhenProjectIsFinished()
    {
        // Arrange
        var project = FakeDataHelper.GetFakeProject();
        project.Start();

        var projectRepository = new Mock<IProjectRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        projectRepository
            .Setup(pr => pr.GetByIdAsync(project.Id, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var completeProjectCommand = new CompleteProjectCommand(project.Id);
        var completeProjectHandler = new CompleteProjectHandler(projectRepository.Object, unitOfWork.Object);

        // Act
        var result = await completeProjectHandler.Handle(completeProjectCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        project.Status.Should().Be(ProjectStatusEnum.Completed);
        project.CompletedAt.Should().NotBeNull();
    }
}