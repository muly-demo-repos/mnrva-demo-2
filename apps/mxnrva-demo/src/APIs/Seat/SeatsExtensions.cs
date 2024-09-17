using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs.Extensions;

public static class SeatsExtensions
{
    public static Seat ToDto(this SeatDbModel model)
    {
        return new Seat
        {
            Booking = model.BookingId,
            CreatedAt = model.CreatedAt,
            Flight = model.FlightId,
            Id = model.Id,
            NumberField = model.NumberField,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SeatDbModel ToModel(this SeatUpdateInput updateDto, SeatWhereUniqueInput uniqueId)
    {
        var seat = new SeatDbModel { Id = uniqueId.Id, NumberField = updateDto.NumberField };

        if (updateDto.Booking != null)
        {
            seat.BookingId = updateDto.Booking;
        }
        if (updateDto.CreatedAt != null)
        {
            seat.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Flight != null)
        {
            seat.FlightId = updateDto.Flight;
        }
        if (updateDto.UpdatedAt != null)
        {
            seat.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return seat;
    }
}
