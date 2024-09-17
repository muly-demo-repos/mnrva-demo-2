using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;

namespace MxnrvaDemo.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AircraftItemsControllerBase : ControllerBase
{
    protected readonly IAircraftItemsService _service;

    public AircraftItemsControllerBase(IAircraftItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Aircraft
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Aircraft>> CreateAircraft(AircraftCreateInput input)
    {
        var aircraft = await _service.CreateAircraft(input);

        return CreatedAtAction(nameof(Aircraft), new { id = aircraft.Id }, aircraft);
    }

    /// <summary>
    /// Delete one Aircraft
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteAircraft([FromRoute()] AircraftWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteAircraft(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many AircraftItems
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Aircraft>>> AircraftItems(
        [FromQuery()] AircraftFindManyArgs filter
    )
    {
        return Ok(await _service.AircraftItems(filter));
    }

    /// <summary>
    /// Meta data about Aircraft records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> AircraftItemsMeta(
        [FromQuery()] AircraftFindManyArgs filter
    )
    {
        return Ok(await _service.AircraftItemsMeta(filter));
    }

    /// <summary>
    /// Get one Aircraft
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Aircraft>> Aircraft(
        [FromRoute()] AircraftWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Aircraft(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Aircraft
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateAircraft(
        [FromRoute()] AircraftWhereUniqueInput uniqueId,
        [FromQuery()] AircraftUpdateInput aircraftUpdateDto
    )
    {
        try
        {
            await _service.UpdateAircraft(uniqueId, aircraftUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Airline record for Aircraft
    /// </summary>
    [HttpGet("{Id}/airline")]
    public async Task<ActionResult<List<Airline>>> GetAirline(
        [FromRoute()] AircraftWhereUniqueInput uniqueId
    )
    {
        var airline = await _service.GetAirline(uniqueId);
        return Ok(airline);
    }

    /// <summary>
    /// Connect multiple Flights records to Aircraft
    /// </summary>
    [HttpPost("{Id}/flights")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectFlights(
        [FromRoute()] AircraftWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Flights records from Aircraft
    /// </summary>
    [HttpDelete("{Id}/flights")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectFlights(
        [FromRoute()] AircraftWhereUniqueInput uniqueId,
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
    /// Find multiple Flights records for Aircraft
    /// </summary>
    [HttpGet("{Id}/flights")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Flight>>> FindFlights(
        [FromRoute()] AircraftWhereUniqueInput uniqueId,
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
    /// Update multiple Flights records for Aircraft
    /// </summary>
    [HttpPatch("{Id}/flights")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateFlights(
        [FromRoute()] AircraftWhereUniqueInput uniqueId,
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
