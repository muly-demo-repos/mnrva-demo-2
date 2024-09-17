using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;

namespace MxnrvaDemo.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AirlinesControllerBase : ControllerBase
{
    protected readonly IAirlinesService _service;

    public AirlinesControllerBase(IAirlinesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Airline
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Airline>> CreateAirline(AirlineCreateInput input)
    {
        var airline = await _service.CreateAirline(input);

        return CreatedAtAction(nameof(Airline), new { id = airline.Id }, airline);
    }

    /// <summary>
    /// Delete one Airline
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteAirline([FromRoute()] AirlineWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAirline(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Airlines
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Airline>>> Airlines(
        [FromQuery()] AirlineFindManyArgs filter
    )
    {
        return Ok(await _service.Airlines(filter));
    }

    /// <summary>
    /// Meta data about Airline records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AirlinesMeta(
        [FromQuery()] AirlineFindManyArgs filter
    )
    {
        return Ok(await _service.AirlinesMeta(filter));
    }

    /// <summary>
    /// Get one Airline
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Airline>> Airline([FromRoute()] AirlineWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Airline(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Airline
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateAirline(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromQuery()] AirlineUpdateInput airlineUpdateDto
    )
    {
        try
        {
            await _service.UpdateAirline(uniqueId, airlineUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple AircraftItems records to Airline
    /// </summary>
    [HttpPost("{Id}/aircraftItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectAircraftItems(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromQuery()] AircraftWhereUniqueInput[] aircraftItemsId
    )
    {
        try
        {
            await _service.ConnectAircraftItems(uniqueId, aircraftItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple AircraftItems records from Airline
    /// </summary>
    [HttpDelete("{Id}/aircraftItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectAircraftItems(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromBody()] AircraftWhereUniqueInput[] aircraftItemsId
    )
    {
        try
        {
            await _service.DisconnectAircraftItems(uniqueId, aircraftItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple AircraftItems records for Airline
    /// </summary>
    [HttpGet("{Id}/aircraftItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Aircraft>>> FindAircraftItems(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromQuery()] AircraftFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindAircraftItems(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple AircraftItems records for Airline
    /// </summary>
    [HttpPatch("{Id}/aircraftItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateAircraftItems(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromBody()] AircraftWhereUniqueInput[] aircraftItemsId
    )
    {
        try
        {
            await _service.UpdateAircraftItems(uniqueId, aircraftItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Flights records to Airline
    /// </summary>
    [HttpPost("{Id}/flights")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectFlights(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromQuery()] FlightWhereUniqueInput[] flightsId
    )
    {
        try
        {
            await _service.ConnectFlights(uniqueId, flightsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Flights records from Airline
    /// </summary>
    [HttpDelete("{Id}/flights")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectFlights(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromBody()] FlightWhereUniqueInput[] flightsId
    )
    {
        try
        {
            await _service.DisconnectFlights(uniqueId, flightsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Flights records for Airline
    /// </summary>
    [HttpGet("{Id}/flights")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Flight>>> FindFlights(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromQuery()] FlightFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindFlights(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Flights records for Airline
    /// </summary>
    [HttpPatch("{Id}/flights")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateFlights(
        [FromRoute()] AirlineWhereUniqueInput uniqueId,
        [FromBody()] FlightWhereUniqueInput[] flightsId
    )
    {
        try
        {
            await _service.UpdateFlights(uniqueId, flightsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
