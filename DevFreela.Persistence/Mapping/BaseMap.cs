using DevFreela.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Persistence.Mapping;

public abstract class BaseMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
{
    protected abstract string TableName { get; }

    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .ToTable(TableName);

        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder
            .Property(p => p.CreatedAt)
            .HasDefaultValue(new DateTimeOffset())
            .ValueGeneratedOnAdd();

        builder
            .Property(p => p.UpdatedAt)
            .HasDefaultValue(new DateTimeOffset())
            .ValueGeneratedOnAddOrUpdate();

        builder
            .Property(p => p.Deleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder
            .HasQueryFilter(q => q.Deleted == false);

        MapFields(builder);
    }

    protected abstract void MapFields(EntityTypeBuilder<TEntity> builder);
}