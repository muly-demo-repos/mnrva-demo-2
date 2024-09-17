using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.Infrastructure;

public class MxnrvaDemoDbContext : IdentityDbContext<IdentityUser>
{
    public MxnrvaDemoDbContext(DbContextOptions<MxnrvaDemoDbContext> options)
        : base(options) { }

    public DbSet<PassengerDbModel> Passengers { get; set; }

    public DbSet<FlightDbModel> Flights { get; set; }

    public DbSet<AirlineDbModel> Airlines { get; set; }

    public DbSet<SeatDbModel> Seats { get; set; }

    public DbSet<BookingDbModel> Bookings { get; set; }

    public DbSet<AircraftDbModel> AircraftItems { get; set; }
}
