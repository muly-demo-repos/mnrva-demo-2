using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs.Extensions;

public static class FlightsExtensions
{
    public static Flight ToDto(this FlightDbModel model)
    {
        return new Flight
        {
            Aircraft = model.AircraftId,
            Airline = model.AirlineId,
            ArrivalTime = model.ArrivalTime,
            Bookings = model.Bookings?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            DepartureTime = model.DepartureTime,
            FlightNumber = model.FlightNumber,
            Id = model.Id,
            Seats = model.Seats?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FlightDbModel ToModel(
        this FlightUpdateInput updateDto,
        FlightWhereUniqueInput uniqueId
    )
    {
        var flight = new FlightDbModel
        {
            Id = uniqueId.Id,
            ArrivalTime = updateDto.ArrivalTime,
            DepartureTime = updateDto.DepartureTime,
            FlightNumber = updateDto.FlightNumber
        };

        if (updateDto.Aircraft != null)
        {
            flight.AircraftId = updateDto.Aircraft;
        }
        if (updateDto.Airline != null)
        {
            flight.AirlineId = updateDto.Airline;
        }
        if (updateDto.CreatedAt != null)
        {
            flight.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            flight.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return flight;
    }
}
