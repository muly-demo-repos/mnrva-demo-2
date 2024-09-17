using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;

namespace MxnrvaDemo.APIs;

public interface ISeatsService
{
    /// <summary>
    /// Create one Seat
    /// </summary>
    public Task<Seat> CreateSeat(SeatCreateInput seat);

    /// <summary>
    /// Delete one Seat
    /// </summary>
    public Task DeleteSeat(SeatWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Seats
    /// </summary>
    public Task<List<Seat>> Seats(SeatFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Seat records
    /// </summary>
    public Task<MetadataDto> SeatsMeta(SeatFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Seat
    /// </summary>
    public Task<Seat> Seat(SeatWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Seat
    /// </summary>
    public Task UpdateSeat(SeatWhereUniqueInput uniqueId, SeatUpdateInput updateDto);

    /// <summary>
    /// Get a Booking record for Seat
    /// </summary>
    public Task<Booking> GetBooking(SeatWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Flight record for Seat
    /// </summary>
    public Task<Flight> GetFlight(SeatWhereUniqueInput uniqueId);
}
