using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;

namespace MxnrvaDemo.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FlightsControllerBase : ControllerBase
{
    protected readonly IFlightsService _service;

    public FlightsControllerBase(IFlightsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Flight
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Flight>> CreateFlight(FlightCreateInput input)
    {
        var flight = await _service.CreateFlight(input);

        return CreatedAtAction(nameof(Flight), new { id = flight.Id }, flight);
    }

    /// <summary>
    /// Delete one Flight
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteFlight([FromRoute()] FlightWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteFlight(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Flights
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Flight>>> Flights([FromQuery()] FlightFindManyArgs filter)
    {
        return Ok(await _service.Flights(filter));
    }

    /// <summary>
    /// Meta data about Flight records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FlightsMeta(
        [FromQuery()] FlightFindManyArgs filter
    )
    {
        return Ok(await _service.FlightsMeta(filter));
    }

    /// <summary>
    /// Get one Flight
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Flight>> Flight([FromRoute()] FlightWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Flight(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Flight
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateFlight(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromQuery()] FlightUpdateInput flightUpdateDto
    )
    {
        try
        {
            await _service.UpdateFlight(uniqueId, flightUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Aircraft record for Flight
    /// </summary>
    [HttpGet("{Id}/aircraft")]
    public async Task<ActionResult<List<Aircraft>>> GetAircraft(
        [FromRoute()] FlightWhereUniqueInput uniqueId
    )
    {
        var aircraft = await _service.GetAircraft(uniqueId);
        return Ok(aircraft);
    }

    /// <summary>
    /// Get a Airline record for Flight
    /// </summary>
    [HttpGet("{Id}/airline")]
    public async Task<ActionResult<List<Airline>>> GetAirline(
        [FromRoute()] FlightWhereUniqueInput uniqueId
    )
    {
        var airline = await _service.GetAirline(uniqueId);
        return Ok(airline);
    }

    /// <summary>
    /// Connect multiple Bookings records to Flight
    /// </summary>
    [HttpPost("{Id}/bookings")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectBookings(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromQuery()] BookingWhereUniqueInput[] bookingsId
    )
    {
        try
        {
            await _service.ConnectBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Bookings records from Flight
    /// </summary>
    [HttpDelete("{Id}/bookings")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectBookings(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromBody()] BookingWhereUniqueInput[] bookingsId
    )
    {
        try
        {
            await _service.DisconnectBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Bookings records for Flight
    /// </summary>
    [HttpGet("{Id}/bookings")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Booking>>> FindBookings(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromQuery()] BookingFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindBookings(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Bookings records for Flight
    /// </summary>
    [HttpPatch("{Id}/bookings")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateBookings(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromBody()] BookingWhereUniqueInput[] bookingsId
    )
    {
        try
        {
            await _service.UpdateBookings(uniqueId, bookingsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Seats records to Flight
    /// </summary>
    [HttpPost("{Id}/seats")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectSeats(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromQuery()] SeatWhereUniqueInput[] seatsId
    )
    {
        try
        {
            await _service.ConnectSeats(uniqueId, seatsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Seats records from Flight
    /// </summary>
    [HttpDelete("{Id}/seats")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectSeats(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromBody()] SeatWhereUniqueInput[] seatsId
    )
    {
        try
        {
            await _service.DisconnectSeats(uniqueId, seatsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Seats records for Flight
    /// </summary>
    [HttpGet("{Id}/seats")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Seat>>> FindSeats(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromQuery()] SeatFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindSeats(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Seats records for Flight
    /// </summary>
    [HttpPatch("{Id}/seats")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateSeats(
        [FromRoute()] FlightWhereUniqueInput uniqueId,
        [FromBody()] SeatWhereUniqueInput[] seatsId
    )
    {
        try
        {
            await _service.UpdateSeats(uniqueId, seatsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
