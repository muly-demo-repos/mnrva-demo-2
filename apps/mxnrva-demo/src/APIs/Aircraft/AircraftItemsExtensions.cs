using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs.Extensions;

public static class AircraftItemsExtensions
{
    public static Aircraft ToDto(this AircraftDbModel model)
    {
        return new Aircraft
        {
            Airline = model.AirlineId,
            Capacity = model.Capacity,
            CreatedAt = model.CreatedAt,
            Flights = model.Flights?.Select(x => x.Id).ToList(),
            Id = model.Id,
            Model = model.Model,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static AircraftDbModel ToModel(
        this AircraftUpdateInput updateDto,
        AircraftWhereUniqueInput uniqueId
    )
    {
        var aircraft = new AircraftDbModel
        {
            Id = uniqueId.Id,
            Capacity = updateDto.Capacity,
            Model = updateDto.Model
        };

        if (updateDto.Airline != null)
        {
            aircraft.AirlineId = updateDto.Airline;
        }
        if (updateDto.CreatedAt != null)
        {
            aircraft.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            aircraft.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return aircraft;
    }
}
