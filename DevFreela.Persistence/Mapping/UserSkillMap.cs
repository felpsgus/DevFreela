using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Persistence.Mapping;

public class UserSkillMap : BaseMap<UserSkill>
{
    protected override string TableName => nameof(UserSkill);

    protected override void MapFields(EntityTypeBuilder<UserSkill> builder)
    {
        builder
            .HasOne(us => us.User)
            .WithMany(u => u.Skills)
            .HasForeignKey(us => us.IdUser)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(us => us.Skill)
            .WithMany(s => s.UserSkills)
            .HasForeignKey(us => us.IdSkill)
            .OnDelete(DeleteBehavior.Restrict);
    }
}