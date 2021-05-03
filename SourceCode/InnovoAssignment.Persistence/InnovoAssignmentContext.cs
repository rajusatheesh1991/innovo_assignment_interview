using InnovoAssignment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnovoAssignment.Persistence
{
    public class InnovoAssignmentContext:DbContext
    {


        public InnovoAssignmentContext(DbContextOptions<InnovoAssignmentContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InnovoAssignmentContext).Assembly);
         

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }




        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<UserPreferences> UserPreferences { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
       
        #endregion
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach(var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.ModifiedAt = DateTime.Now; 
                       
                       
                        break;
                    case EntityState.Modified:

                    

                        break;
                }
            }



            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.ModifiedAt = DateTime.Now;


                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.Now;


                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
