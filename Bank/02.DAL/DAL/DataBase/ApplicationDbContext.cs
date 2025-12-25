using Entities.TableModels;
using Entities.TableModels.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataBase
{
    public class ApplicationDbContext: IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=BankingSystemDb;Trusted_Connection=True; TrustServerCertificate=True;");
            }
        }

        //public ApplicationDbContext(
        //DbContextOptions<ApplicationDbContext> options)
        //: base(options)
        //{
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
           .HasOne(a => a.Customer)
           .WithMany(c => c.Accounts)
           .HasForeignKey(a => a.CustomerId)
           .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }


    }
}
