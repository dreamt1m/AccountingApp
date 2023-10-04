using AccountingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingApp.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH)
            .IsRequired();

        builder.HasOne(x => x.Position)
            .WithMany();

        builder.HasMany(x => x.Reports)
            .WithOne(x => x.Employee);

        builder.HasMany(x => x.Bonuses)
            .WithOne(x => x.Employee);

        builder.ToTable("Employees");
    }
}