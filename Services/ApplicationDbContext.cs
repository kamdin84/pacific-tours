using ccse_cw1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace ccse_cw1.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> //Add the namespace on top: using Microsoft.EntityFrameworkCore;
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) //Constructor for the class
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            var seller = new IdentityRole("seller");
            seller.NormalizedName = "seller";

            builder.Entity<IdentityRole>().HasData(admin, client, seller);

            builder.Entity<ApplicationUser>()
            .HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserID);

            // lets seed some data :D YAY
            
            var hotels = new List<Hotel> {
                new() { HotelName = "Hilton London Hotel", HotelLocation = "London", SinglePrice = 375, DoublePrice = 775, FamilyPrice = 950 },
                new() { HotelName = "London Marriott Hotel", HotelLocation = "London", SinglePrice = 300, DoublePrice = 500, FamilyPrice = 900 },
                new() { HotelName = "Travelodge Brighton Seafront", HotelLocation = "Brighton", SinglePrice = 80, DoublePrice = 120, FamilyPrice = 150 },
                new() { HotelName = "Kings Hotel Brighton", HotelLocation = "Brighton", SinglePrice = 180, DoublePrice = 400, FamilyPrice = 520},
                new() { HotelName = "Leonardo Hotel Brighton", HotelLocation = "Brighton", SinglePrice = 180, DoublePrice = 400, FamilyPrice = 520},
                new() { HotelName = "Nevis Bank Inn, Fort William", HotelLocation = "Fort William", SinglePrice = 90, DoublePrice = 100, FamilyPrice = 155 },
            };

            var id = 0;
            foreach (var hotel in hotels)
            {
                id += 1;
                hotel.HotelID = id;
                builder.Entity<Hotel>().HasData(hotel);
            }


            var tours = new List<Tour> {
                new() { TourName = "Real Britain", TourPrice = 1200, TourSpaces = 30, Duration = 6 },
                new() { TourName = "Britain and Ireland Explorer", TourPrice = 2000, TourSpaces = 40, Duration = 16 },
                new() { TourName = "Best of Britain", TourPrice = 2900, TourSpaces = 30, Duration = 12 },
            };

            id = 0;
            foreach (var tour in tours)
            {
                id += 1;
                tour.TourID = id;
                builder.Entity<Tour>().HasData(tour);
            }
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Tour> Tours { get; set; }


    
}
    }
