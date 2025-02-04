using DevFreela.Application.Projects.Commands.StartProject;
using DevFreela.Domain.Enums;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Projects;

public class StartProjectHandlerTest
{
    [Fact]
    public async Task ShouldChangeStatusWhenProjectIsStarted()
    {
        // Arrange
        var projectRepository = new Mock<IProjectRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var project = FakeDataHelper.GetFakeProject();

        projectRepository
            .Setup(pr => pr.GetByIdAsync(It.IsAny<long>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var startProjectHandler = new StartProjectHandler(projectRepository.Object, unitOfWork.Object);
        var startProjectCommand = new StartProjectCommand(1);

        // Act
        var result = await startProjectHandler.Handle(startProjectCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        project.Status.Should().Be(ProjectStatusEnum.InProgress);
        project.StartedAt.Should().NotBeNull();
    }
}