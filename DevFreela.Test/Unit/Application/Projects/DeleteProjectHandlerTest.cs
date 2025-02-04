using DevFreela.Application.Projects.Commands.DeleteProject;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Projects;

public class DeleteProjectHandlerTest
{
    [Fact]
    public async Task ShouldDeleteProjectWhenProjectExists()
    {
        // Arrange
        var project = FakeDataHelper.GetFakeProject();

        var projectRepository = new Mock<IProjectRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var deleteProjectCommand = new DeleteProjectCommand(project.Id);
        var deleteProjectHandler = new DeleteProjectHandler(projectRepository.Object, unitOfWork.Object);

        projectRepository
            .Setup(pr => pr.GetByIdAsync(project.Id, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);
        projectRepository
            .Setup(pr => pr.Delete(project))
            .Callback(() => project.Delete());

        unitOfWork
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await deleteProjectHandler.Handle(deleteProjectCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        project.Deleted.Should().BeTrue();
    }
}