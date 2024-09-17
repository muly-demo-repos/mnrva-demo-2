using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;

namespace MxnrvaDemo.APIs;

public interface IBookingsService
{
    /// <summary>
    /// Create one Booking
    /// </summary>
    public Task<Booking> CreateBooking(BookingCreateInput booking);

    /// <summary>
    /// Delete one Booking
    /// </summary>
    public Task DeleteBooking(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Bookings
    /// </summary>
    public Task<List<Booking>> Bookings(BookingFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Booking records
    /// </summary>
    public Task<MetadataDto> BookingsMeta(BookingFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Booking
    /// </summary>
    public Task<Booking> Booking(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Booking
    /// </summary>
    public Task UpdateBooking(BookingWhereUniqueInput uniqueId, BookingUpdateInput updateDto);

    /// <summary>
    /// Get a Flight record for Booking
    /// </summary>
    public Task<Flight> GetFlight(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Passenger record for Booking
    /// </summary>
    public Task<Passenger> GetPassenger(BookingWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Seats records to Booking
    /// </summary>
    public Task ConnectSeats(BookingWhereUniqueInput uniqueId, SeatWhereUniqueInput[] seatsId);

    /// <summary>
    /// Disconnect multiple Seats records from Booking
    /// </summary>
    public Task DisconnectSeats(BookingWhereUniqueInput uniqueId, SeatWhereUniqueInput[] seatsId);

    /// <summary>
    /// Find multiple Seats records for Booking
    /// </summary>
    public Task<List<Seat>> FindSeats(
        BookingWhereUniqueInput uniqueId,
        SeatFindManyArgs SeatFindManyArgs
    );

    /// <summary>
    /// Update multiple Seats records for Booking
    /// </summary>
    public Task UpdateSeats(BookingWhereUniqueInput uniqueId, SeatWhereUniqueInput[] seatsId);
}
