using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.Infrastructure;

public class MxnrvaDemoDbContext : IdentityDbContext<IdentityUser>
{
    public MxnrvaDemoDbContext(DbContextOptions<MxnrvaDemoDbContext> options)
        : base(options) { }

    public DbSet<OrderItemDbModel> OrderItems { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<OrderDbModel> Orders { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }
}
