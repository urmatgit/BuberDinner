using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu;
using BuberDinner.Domain.Menu.Entities;
using BuberDinner.Domain.Menu.ValueObjects;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations
{
    public class MenuConfigurations : IEntityTypeConfiguration<Menu>
    {
        void IEntityTypeConfiguration<Menu>.Configure(EntityTypeBuilder<Menu> builder)
        {
            ConfigureMenusTable(builder);
            ConfigureMenuSectionsTable(builder);
            ConfigureMenuDinnerIdTable(builder);
            ConfigureMenuReviewIdTable(builder);
        }

        private void ConfigureMenuReviewIdTable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.DinnerIds, di =>{
                di.ToTable("MenuDinnerIds");
                di.WithOwner().HasForeignKey("MenuId");
                di.HasKey("Id");
                di.Property(d => d.Value)
                .HasColumnName("DinnerId")
                .ValueGeneratedNever();
            });
            builder.Metadata.FindNavigation(nameof(Menu.DinnerIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

        }

        private void ConfigureMenuDinnerIdTable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.MenuReviewIds, di => {
                di.ToTable("MenuReviewIds");
                di.WithOwner().HasForeignKey("MenuId");
                di.HasKey("Id");
                di.Property(d => d.Value)
                .HasColumnName("ReviewId")
                .ValueGeneratedNever();
            });
            builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);

        }

        private void ConfigureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
        {
            builder.OwnsMany(m => m.Sections, sb =>
            {
                sb.ToTable("MenuSections");
                sb.WithOwner().HasForeignKey("MenuId");
                sb.HasKey("Id", "MenuId");
                sb.Property(s => s.Id)
                .HasColumnName("MenuSectionId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MenuSectionId.Create(value));
                sb.Property(sb => sb.Name)
                .HasMaxLength(100);
                sb.Property(sb => sb.Description)
                .HasMaxLength(100);
                sb.OwnsMany(s => s.Items, ib =>
                {
                    ib.ToTable("MenuItems");
                    ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");
                    ib.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");
                    ib.Property(i => i.Id)
                    .HasColumnName("MenuItemId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => MenuItemId.Create(value));
                    ib.Property(sb => sb.Name)
                    .HasMaxLength(100);
                    ib.Property(sb => sb.Description)
                    .HasMaxLength(100);


                });
                sb.Navigation(i => i.Items).Metadata.SetField("_items");
                sb.Navigation(s => s.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
            });
            builder.Metadata.FindNavigation(nameof(Menu.Sections))
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

        private void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                id=>id.Value,
                value=>MenuId.Create(value));

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .HasMaxLength(100);
            builder.OwnsOne(m => m.AverageRating);

            builder.Property(p => p.HostId)
                .HasConversion(
                id => id.Value,
                value => HostId.Create(value));
        }
    }
}
