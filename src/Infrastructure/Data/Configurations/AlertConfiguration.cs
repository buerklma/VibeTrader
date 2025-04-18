using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VibeTrader.Domain.Entities;

namespace VibeTrader.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Entity configuration for the Alert entity
    /// </summary>
    public class AlertConfiguration : IEntityTypeConfiguration<Alert>
    {
        /// <summary>
        /// Configures the entity of type Alert
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type</param>
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Symbol)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.TargetPrice)
                .IsRequired()
                .HasColumnType("decimal(18,4)");

            builder.Property(a => a.Type)
                .IsRequired();

            builder.Property(a => a.CreatedOn)
                .IsRequired();

            builder.Property(a => a.TriggeredOn)
                .IsRequired(false);

            builder.Property(a => a.IsActive)
                .IsRequired();
        }
    }
}