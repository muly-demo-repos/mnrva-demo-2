using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;

namespace MxnrvaDemo.APIs;

public interface IAirlinesService
{
    /// <summary>
    /// Create one Airline
    /// </summary>
    public Task<Airline> CreateAirline(AirlineCreateInput airline);

    /// <summary>
    /// Delete one Airline
    /// </summary>
    public Task DeleteAirline(AirlineWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Airlines
    /// </summary>
    public Task<List<Airline>> Airlines(AirlineFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Airline records
    /// </summary>
    public Task<MetadataDto> AirlinesMeta(AirlineFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Airline
    /// </summary>
    public Task<Airline> Airline(AirlineWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Airline
    /// </summary>
    public Task UpdateAirline(AirlineWhereUniqueInput uniqueId, AirlineUpdateInput updateDto);

    /// <summary>
    /// Connect multiple AircraftItems records to Airline
    /// </summary>
    public Task ConnectAircraftItems(
        AirlineWhereUniqueInput uniqueId,
        AircraftWhereUniqueInput[] aircraftId
    );

    /// <summary>
    /// Disconnect multiple AircraftItems records from Airline
    /// </summary>
    public Task DisconnectAircraftItems(
        AirlineWhereUniqueInput uniqueId,
        AircraftWhereUniqueInput[] aircraftId
    );

    /// <summary>
    /// Find multiple AircraftItems records for Airline
    /// </summary>
    public Task<List<Aircraft>> FindAircraftItems(
        AirlineWhereUniqueInput uniqueId,
        AircraftFindManyArgs AircraftFindManyArgs
    );

    /// <summary>
    /// Update multiple AircraftItems records for Airline
    /// </summary>
    public Task UpdateAircraftItems(
        AirlineWhereUniqueInput uniqueId,
        AircraftWhereUniqueInput[] aircraftId
    );

    /// <summary>
    /// Connect multiple Flights records to Airline
    /// </summary>
    public Task ConnectFlights(
        AirlineWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] flightsId
    );

    /// <summary>
    /// Disconnect multiple Flights records from Airline
    /// </summary>
    public Task DisconnectFlights(
        AirlineWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] flightsId
    );

    /// <summary>
    /// Find multiple Flights records for Airline
    /// </summary>
    public Task<List<Flight>> FindFlights(
        AirlineWhereUniqueInput uniqueId,
        FlightFindManyArgs FlightFindManyArgs
    );

    /// <summary>
    /// Update multiple Flights records for Airline
    /// </summary>
    public Task UpdateFlights(AirlineWhereUniqueInput uniqueId, FlightWhereUniqueInput[] flightsId);
}
