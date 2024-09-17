using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs.Extensions;

public static class AirlinesExtensions
{
    public static Airline ToDto(this AirlineDbModel model)
    {
        return new Airline
        {
            AircraftItems = model.AircraftItems?.Select(x => x.Id).ToList(),
            Country = model.Country,
            CreatedAt = model.CreatedAt,
            Flights = model.Flights?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Name = model.Name,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AirlineDbModel ToModel(
        this AirlineUpdateInput updateDto,
        AirlineWhereUniqueInput uniqueId
    )
    {
        var airline = new AirlineDbModel
        {
            Id = uniqueId.Id,
            Country = updateDto.Country,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            airline.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            airline.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return airline;
    }
}
