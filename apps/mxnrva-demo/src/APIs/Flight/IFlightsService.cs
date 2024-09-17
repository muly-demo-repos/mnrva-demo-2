using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;

namespace MxnrvaDemo.APIs;

public interface IFlightsService
{
    /// <summary>
    /// Create one Flight
    /// </summary>
    public Task<Flight> CreateFlight(FlightCreateInput flight);

    /// <summary>
    /// Delete one Flight
    /// </summary>
    public Task DeleteFlight(FlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Flights
    /// </summary>
    public Task<List<Flight>> Flights(FlightFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Flight records
    /// </summary>
    public Task<MetadataDto> FlightsMeta(FlightFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Flight
    /// </summary>
    public Task<Flight> Flight(FlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Flight
    /// </summary>
    public Task UpdateFlight(FlightWhereUniqueInput uniqueId, FlightUpdateInput updateDto);

    /// <summary>
    /// Get a Aircraft record for Flight
    /// </summary>
    public Task<Aircraft> GetAircraft(FlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Airline record for Flight
    /// </summary>
    public Task<Airline> GetAirline(FlightWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Bookings records to Flight
    /// </summary>
    public Task ConnectBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Disconnect multiple Bookings records from Flight
    /// </summary>
    public Task DisconnectBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Find multiple Bookings records for Flight
    /// </summary>
    public Task<List<Booking>> FindBookings(
        FlightWhereUniqueInput uniqueId,
        BookingFindManyArgs BookingFindManyArgs
    );

    /// <summary>
    /// Update multiple Bookings records for Flight
    /// </summary>
    public Task UpdateBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] bookingsId
    );

    /// <summary>
    /// Connect multiple Seats records to Flight
    /// </summary>
    public Task ConnectSeats(FlightWhereUniqueInput uniqueId, SeatWhereUniqueInput[] seatsId);

    /// <summary>
    /// Disconnect multiple Seats records from Flight
    /// </summary>
    public Task DisconnectSeats(FlightWhereUniqueInput uniqueId, SeatWhereUniqueInput[] seatsId);

    /// <summary>
    /// Find multiple Seats records for Flight
    /// </summary>
    public Task<List<Seat>> FindSeats(
        FlightWhereUniqueInput uniqueId,
        SeatFindManyArgs SeatFindManyArgs
    );

    /// <summary>
    /// Update multiple Seats records for Flight
    /// </summary>
    public Task UpdateSeats(FlightWhereUniqueInput uniqueId, SeatWhereUniqueInput[] seatsId);
}
