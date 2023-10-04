using AccountingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountingApp.Infrastructure.Data.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.HoursWorked).IsRequired();

        builder.Property(x => x.Date).IsRequired();

        builder.HasOne(x => x.Employee)
            .WithMany(x => x.Reports);

        builder.ToTable("Reports");
    }
}