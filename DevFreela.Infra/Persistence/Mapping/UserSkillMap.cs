using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infra.Persistence.Mapping;

public class UserSkillMap : BaseMap<UserSkill>
{
    protected override string TableName => nameof(UserSkill);

    protected override void MapFields(EntityTypeBuilder<UserSkill> builder)
    {
    }
}