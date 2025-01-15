using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Persistence.Mapping;

public class UserMap : BaseMap<User>
{
    protected override string TableName => nameof(User);

    protected override void MapFields(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(u => u.Skills)
            .WithMany()
            .UsingEntity<UserSkill>(
                l => l.HasOne<Skill>().WithMany().HasForeignKey(s => s.SkillId).OnDelete(DeleteBehavior.Restrict),
                r => r.HasOne<User>().WithMany(us => us.UserSkills).HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict));
    }
}