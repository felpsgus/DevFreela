using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Persistence.Mapping;

public class ProjectMap : BaseMap<Project>
{
    protected override string TableName => nameof(Project);

    protected override void MapFields(EntityTypeBuilder<Project> builder)
    {
        builder
            .Property(p => p.TotalCost)
            .IsRequired();

        builder
            .HasOne(p => p.Client)
            .WithMany(u => u.OwnedProjects)
            .HasForeignKey(p => p.IdClient)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(p => p.Freelancer)
            .WithMany(u => u.FreelanceProjects)
            .HasForeignKey(p => p.IdFreelancer)
            .OnDelete(DeleteBehavior.Restrict);
    }
}