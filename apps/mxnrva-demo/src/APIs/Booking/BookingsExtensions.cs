using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs.Extensions;

public static class BookingsExtensions
{
    public static Booking ToDto(this BookingDbModel model)
    {
        return new Booking
        {
            BookingDate = model.BookingDate,
            CreatedAt = model.CreatedAt,
            Flight = model.FlightId,
            Id = model.Id,
            Passenger = model.PassengerId,
            Seats = model.Seats?.Select(x => x.Id).ToList(),
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BookingDbModel ToModel(
        this BookingUpdateInput updateDto,
        BookingWhereUniqueInput uniqueId
    )
    {
        var booking = new BookingDbModel
        {
            Id = uniqueId.Id,
            BookingDate = updateDto.BookingDate,
            Status = updateDto.Status
        };

        if (updateDto.CreatedAt != null)
        {
            booking.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Flight != null)
        {
            booking.FlightId = updateDto.Flight;
        }
        if (updateDto.Passenger != null)
        {
            booking.PassengerId = updateDto.Passenger;
        }
        if (updateDto.UpdatedAt != null)
        {
            booking.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return booking;
    }
}
