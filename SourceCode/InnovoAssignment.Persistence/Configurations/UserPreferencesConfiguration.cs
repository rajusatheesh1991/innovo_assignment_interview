using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InnovoAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Persistence.Configurations
{
    public class UserPreferencesConfiguration : IEntityTypeConfiguration<UserPreferences>
    {
        public void Configure(EntityTypeBuilder<UserPreferences> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(e => e.EnablePromotionNotifications)
                 .IsRequired();
          

        }
    }
}
