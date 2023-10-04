using AccountingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingApp.Infrastructure.Data.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.Property(x => x.RatePerHour)
            .IsRequired();

        builder.Property(x => x.OvertimeMultiplier)
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(x => x.WorkingHoursPerMonth)
            .IsRequired();

        builder.Property(x => x.FormOfRemuneration)
            .IsRequired();

        builder.ToTable("Positions");
    }
}