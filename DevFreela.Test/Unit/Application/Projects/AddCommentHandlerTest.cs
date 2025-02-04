using DevFreela.Application.Projects.Commands.AddComment;
using DevFreela.Domain.Interfaces;
using DevFreela.Test.Fakes;

namespace DevFreela.Test.Unit.Application.Projects;

public class AddCommentHandlerTest
{
    [Fact]
    public async Task ShouldInsertCommentIntoProjectWhenCommentIsSent()
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

        var addCommentHandler = new AddCommentHandler(projectRepository.Object, unitOfWork.Object);
        var addCommentCommand = new AddCommentCommand
        {
            Content = "Comment test",
            ProjectId = 1,
            UserId = 1
        };

        // Act
        var result = await addCommentHandler.Handle(addCommentCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        project.Comments.Should().HaveCount(1);
        project.Comments.Should().OnlyContain(c => c.Content == addCommentCommand.Content);
    }
}