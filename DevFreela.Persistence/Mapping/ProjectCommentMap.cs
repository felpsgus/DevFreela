using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Persistence.Mapping;

public class ProjectCommentMap : BaseMap<ProjectComment>
{
    protected override string TableName => nameof(ProjectComment);

    protected override void MapFields(EntityTypeBuilder<ProjectComment> builder)
    {
        builder
            .HasOne(pc => pc.Project)
            .WithMany(p => p.Comments)
            .HasForeignKey(pc => pc.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(pc => pc.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(pc => pc.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}