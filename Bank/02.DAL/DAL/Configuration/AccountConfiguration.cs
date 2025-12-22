using Entities.TableModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class AccountConfiguration: IEntityTypeConfiguration<Account>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Account> builder)
        {
            builder.Property(a => a.AccountNumber).IsRequired().HasColumnType("nvarchar").HasMaxLength(20);
            builder.Property(a => a.AccountType).IsRequired().HasColumnType("nvarchar").HasMaxLength(30);
            builder.Property(a => a.Balance).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(a => a.Customer).WithMany(c => c.Accounts).HasForeignKey(a => a.CustomerId);
        }
    }
}
