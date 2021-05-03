using InnovoAssignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);


         

            builder.Property(e => e.FullName)
              
                .HasMaxLength(60);


            builder.Property(e => e.PhoneNumber)
              
               .HasMaxLength(10);

            builder.Property(e => e.Password)
              .IsRequired()
              .HasMaxLength(250);

           



        }
    }
}
