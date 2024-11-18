using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Persistence.Mapping;

public class SkillMap : BaseMap<Skill>
{
    protected override string TableName => nameof(Skill);

    protected override void MapFields(EntityTypeBuilder<Skill> builder)
    {
    }
}