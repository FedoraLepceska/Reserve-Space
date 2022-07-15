using Domain.Identity;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reserve_Space.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Space> Spaces { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservedSpaces> ReservedSpaces { get; set; }

        public virtual DbSet<SpacesInOrder> SpacesInOrders { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<EmailMessage> EmailMessages { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Space>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Reservation>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ReservedSpaces>()
                .HasOne(z => z.Reservation)
                .WithMany(z => z.ReservedSpaces)
                .HasForeignKey(z => z.ReservationId);

            builder.Entity<ReservedSpaces>()
                .HasOne(z => z.Space)
                .WithMany(z => z.ReservedSpaces)
                .HasForeignKey(z => z.SpaceId);

            builder.Entity<Reservation>()
                .HasOne<ApplicationUser>(z => z.User)
                .WithOne(z => z.Reservation)
                .HasForeignKey<Reservation>(z => z.ApplicationUserId);


            builder.Entity<SpacesInOrder>()
                .HasOne(z => z.SelectedSpace)
                .WithMany(z => z.Orders)
                .HasForeignKey(z => z.SpaceId);

            builder.Entity<SpacesInOrder>()
                .HasOne(z => z.Order)
                .WithMany(z => z.Spaces)
                .HasForeignKey(z => z.OrderId);
        }
    }
}
