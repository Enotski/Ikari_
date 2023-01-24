using Ikari.Data.Models.Entities.ShopItems;
using Ikari.Data.Models.Entities.UserProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Abstraction
{
    public class IkariDbContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Sword> Swords { get; set; }
        public DbSet<Armour> Armours { get; set; }
        public DbSet<Role> Roles { get; set; }

        //public IkariDbContext() {
        //    //Database.EnsureDeleted();
        //    //Database.EnsureCreated();
        //}
        public IkariDbContext(DbContextOptions<IkariDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            //if (!optionsBuilder.IsConfigured) {
            //    IConfigurationRoot configuration = new ConfigurationBuilder()
            //       .SetBasePath(Directory.GetCurrentDirectory())
            //       .AddJsonFile("appsettings.json")
            //       .Build();
            //    var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
            //    optionsBuilder.UseSqlServer(connectionString);
            //}
            //    optionsBuilder.UseSqlServer(@"Server=LAZARUS\SQLEXPRESS;User Id=zabig;Database=ikari_db;Trusted_Connection=True;TrustServerCertificate=Yes;Encrypt=False;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<Armour>()
                .HasMany(c => c.Users)
                .WithMany(s => s.Armours)
                .UsingEntity(j => j.ToTable("ArmourUsers"));
            modelBuilder.Entity<Sword>()
                .HasMany(c => c.Users)
                .WithMany(s => s.Swords)
                .UsingEntity(j => j.ToTable("SwordUsers"));

            var adminId = Guid.NewGuid();
            var moderId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            modelBuilder.Entity<Role>().HasData(
                    new Role { Id = adminId, Name = "Admin" },
                    new Role { Id = moderId, Name = "Moderator" },
                    new Role { Id = userId, Name = "User" }
                );
            modelBuilder.Entity<User>().HasData(
                    new User { Id = Guid.NewGuid(), Login = "user1", Email = "em1@.com", Password = "111", RoleId = adminId },
                    new User { Id = Guid.NewGuid(), Login = "user2", Email = "em2@.com", Password = "222", RoleId = moderId },
                    new User { Id = Guid.NewGuid(), Login = "user3", Email = "em3@.com", Password = "333", RoleId = userId }
                );
            modelBuilder.Entity<Sword>().HasData(
                    new Sword { Id = Guid.NewGuid(), Name = "weapon1" },
                    new Sword { Id = Guid.NewGuid(), Name = "weapon2" },
                    new Sword { Id = Guid.NewGuid(), Name = "weapon3" }
                );
            modelBuilder.Entity<Armour>().HasData(
                    new Armour { Id = Guid.NewGuid(), Name = "armour1" },
                    new Armour { Id = Guid.NewGuid(), Name = "armour2" },
                    new Armour { Id = Guid.NewGuid(), Name = "armour3" }
                );
        }
    }
}
