using CabinCrew.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CabinCrew.Infrastructure.Persistence.Configurations
{
    public class CabinAttendantConfiguration : IEntityTypeConfiguration<CabinAttendant>
    {
        public void Configure(EntityTypeBuilder<CabinAttendant> builder)
        {
            builder.ToTable("CabinAttendants");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .IsRequired()
                   .ValueGeneratedNever();

            builder.OwnsOne(x => x.Info, info =>
            {
                info.Property(i => i.Name).HasColumnName("Name").IsRequired();
                info.Property(i => i.Age).HasColumnName("Age").IsRequired();
                info.Property(i => i.Gender).HasColumnName("Gender").IsRequired();
                info.Property(i => i.Nationality).HasColumnName("Nationality").IsRequired();

                // KnownLanguages listesi -> JSON saklama
                info.Property<List<string>>("_knownLanguages")
                    .HasColumnName("KnownLanguages")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
                    ).HasColumnType("nvarchar(max)");
            });

            builder.Property(x => x.AttendantType)
                   .IsRequired()
                   .HasConversion<string>();

            // VehicleRestrictions listesi -> JSON
            builder.Property<List<string>>("_vehicleRestrictions")
                   .HasColumnName("VehicleRestrictions")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                       v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
                   ).HasColumnType("nvarchar(max)");

            // Recipes listesi -> JSON
            builder.Property<List<string>>("_recipes")
                   .HasColumnName("Recipes")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                       v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
                   ).HasColumnType("nvarchar(max)");
        }
    }
}
