using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using InnovoAssignment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Persistence.Configurations
{
    public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(e => e.AddressLine1)
                 .IsRequired()
                 .HasMaxLength(60);

          

            builder.Property(e => e.AddressLine2)
               
                 .HasMaxLength(60);


            builder.Property(e => e.City)
                 .IsRequired()
                 .HasMaxLength(60);


            builder.Property(e => e.State)
                 .IsRequired()
                 .HasMaxLength(60);


            builder.Property(e => e.ZipCode)
                 .IsRequired()
                 .HasMaxLength(6);


            builder.Property(e => e.Country)
                 .IsRequired()
                 .HasMaxLength(60);



        }
    }
}
