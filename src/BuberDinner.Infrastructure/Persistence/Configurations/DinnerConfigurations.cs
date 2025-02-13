using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Bill;
using BuberDinner.Domain.Bill.ValueObjects;
using BuberDinner.Domain.Dinner;
using BuberDinner.Domain.Dinner.ValueObjects;
using BuberDinner.Domain.Guest.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.Reservation;
using BuberDinner.Domain.Reservation.ValueObjects;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations
{
    public class DinnerConfigurations : IEntityTypeConfiguration<Dinner>
    {
        public void Configure(EntityTypeBuilder<Dinner> builder)
        {
            builder.ToTable("Dinners");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id => id.Value,
                value => DinnerId.Create(value));
            builder.OwnsOne(x => x.Price);
            builder.OwnsOne(x => x.Location);
           // builder.HasKey( nameof(MenuId), nameof(HostId));
            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(100);
            builder.Property(p => p.HostId)
             .HasConversion(
             id => id.Value,
             value => HostId.Create(value));
            
            builder.Property(p => p.MenuId)
              .HasConversion(
              id => id.Value,
              value => MenuId.Create(value));
            builder.OwnsMany(r => r.Reservations, rs =>
            {
                rs.ToTable("Reservations");
                rs.WithOwner().HasForeignKey("DinnerId");
                rs.HasKey(nameof(Reservation.Id), "DinnerId");
                rs.Property(p => p.Id)
                .HasColumnName("ReservationId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ReservationId.Create(value));
                rs.Property(p => p.DinnerId)
                .HasConversion(
                    id => id.Value,
                    value => DinnerId.Create(value));
                rs.Property(p => p.GuestId)
                .HasConversion(
                    id => id.Value,
                    value => GuestId.Create(value));
                rs.Property(p => p.BillId)
                .HasConversion(
                    id => id.Value,
                    value => BillId.Create(value));

            });
            builder.Metadata.FindNavigation(nameof(Dinner.Reservations))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
