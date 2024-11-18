using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Persistence.Mapping;

public class UserMap : BaseMap<User>
{
    protected override string TableName => nameof(User);

    protected override void MapFields(EntityTypeBuilder<User> builder)
    {
    }
}