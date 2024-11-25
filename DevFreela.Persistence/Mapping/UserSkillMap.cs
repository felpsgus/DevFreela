using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Persistence.Mapping;

public class UserSkillMap : BaseMap<UserSkill>
{
    protected override string TableName => nameof(UserSkill);

    protected override void MapFields(EntityTypeBuilder<UserSkill> builder)
    {
    }
}