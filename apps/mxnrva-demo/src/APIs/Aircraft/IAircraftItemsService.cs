using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;

namespace MxnrvaDemo.APIs;

public interface IAircraftItemsService
{
    /// <summary>
    /// Create one Aircraft
    /// </summary>
    public Task<Aircraft> CreateAircraft(AircraftCreateInput aircraft);

    /// <summary>
    /// Delete one Aircraft
    /// </summary>
    public Task DeleteAircraft(AircraftWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many AircraftItems
    /// </summary>
    public Task<List<Aircraft>> AircraftItems(AircraftFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Aircraft records
    /// </summary>
    public Task<MetadataDto> AircraftItemsMeta(AircraftFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Aircraft
    /// </summary>
    public Task<Aircraft> Aircraft(AircraftWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Aircraft
    /// </summary>
    public Task UpdateAircraft(AircraftWhereUniqueInput uniqueId, AircraftUpdateInput updateDto);

    /// <summary>
    /// Get a Airline record for Aircraft
    /// </summary>
    public Task<Airline> GetAirline(AircraftWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple Flights records to Aircraft
    /// </summary>
    public Task ConnectFlights(
        AircraftWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] flightsId
    );

    /// <summary>
    /// Disconnect multiple Flights records from Aircraft
    /// </summary>
    public Task DisconnectFlights(
        AircraftWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] flightsId
    );

    /// <summary>
    /// Find multiple Flights records for Aircraft
    /// </summary>
    public Task<List<Flight>> FindFlights(
        AircraftWhereUniqueInput uniqueId,
        FlightFindManyArgs FlightFindManyArgs
    );

    /// <summary>
    /// Update multiple Flights records for Aircraft
    /// </summary>
    public Task UpdateFlights(
        AircraftWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] flightsId
    );
}
