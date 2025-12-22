using Entities.TableModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        { 
            builder.Property(c => c.FirstName).IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(c => c.LastName).IsRequired().HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(c => c.Age).IsRequired();
            builder.Property(c => c.City).IsRequired().HasColumnType("nvarchar").HasMaxLength(100);

        } 
    }
}
