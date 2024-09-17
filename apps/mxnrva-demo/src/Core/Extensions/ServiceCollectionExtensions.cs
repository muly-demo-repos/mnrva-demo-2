using MxnrvaDemo.APIs;

namespace MxnrvaDemo;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAircraftService, AircraftService>();
        services.AddScoped<IAirlinesService, AirlinesService>();
        services.AddScoped<IBookingsService, BookingsService>();
        services.AddScoped<IFlightsService, FlightsService>();
        services.AddScoped<IPassengersService, PassengersService>();
        services.AddScoped<ISeatsService, SeatsService>();
    }
}
