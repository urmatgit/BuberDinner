using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Bill;
using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations
{
    public class BillConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => BillId.Create(value));
            builder.OwnsOne(x => x.Price);
            builder.HasKey(nameof(DinnerId), nameof(GuestId), nameof(HostId));
            builder.Property(p => p.HostId)
              .HasConversion(
              id => id.Value,
              value => HostId.Create(value));
            builder.Property(p => p.DinnerId)
              .HasConversion(
              id => id.Value,
              value => DinnerId.Create(value));
            builder.Property(p => p.GuestId)
              .HasConversion(
              id => id.Value,
              value => GuestId.Create(value));

        }
    }
}
