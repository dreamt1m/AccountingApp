using AccountingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingApp.Infrastructure.Data.Configurations;

public class BonusConfiguration : IEntityTypeConfiguration<Bonus>
{
    public void Configure(EntityTypeBuilder<Bonus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.Date).IsRequired();

        builder.Property(x => x.Value).IsRequired();

        builder.Property(x => x.BonusType).IsRequired();

        builder.HasOne(x => x.Employee)
            .WithMany(x => x.Bonuses);

        builder.ToTable("Bonuses");
    }
}