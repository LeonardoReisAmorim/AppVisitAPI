using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opt) : base(opt)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileVR>()
                .HasOne(file => file.Place)
                .WithOne(place => place.FileVR)
                .HasForeignKey<Place>(place => place.FileVRId);

            modelBuilder.Entity<State>()
                .HasOne(state => state.Country)
                .WithMany(country => country.States)
                .HasForeignKey(state => state.CountryId);

            modelBuilder.Entity<City>()
                .HasOne(city => city.State)
                .WithMany(state => state.Cities)
                .HasForeignKey(city => city.StateId);

            modelBuilder.Entity<Place>()
                .HasOne(place => place.City)
                .WithMany(city => city.Places)
                .HasForeignKey(place => place.CityId);

            modelBuilder.Entity<Place>()
                .HasOne(place => place.TypePlace)
                .WithMany(typeplace => typeplace.Places)
                .HasForeignKey(place => place.TypePlaceId);
        }

        public DbSet<FileVR> FilesVr { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TypePlace> TypePlaces { get; set; }
    }
}
