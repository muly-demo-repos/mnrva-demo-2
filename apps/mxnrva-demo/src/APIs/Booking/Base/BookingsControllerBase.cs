using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;

namespace MxnrvaDemo.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BookingsControllerBase : ControllerBase
{
    protected readonly IBookingsService _service;

    public BookingsControllerBase(IBookingsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Booking
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Booking>> CreateBooking(BookingCreateInput input)
    {
        var booking = await _service.CreateBooking(input);

        return CreatedAtAction(nameof(Booking), new { id = booking.Id }, booking);
    }

    /// <summary>
    /// Delete one Booking
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteBooking([FromRoute()] BookingWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBooking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Bookings
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Booking>>> Bookings(
        [FromQuery()] BookingFindManyArgs filter
    )
    {
        return Ok(await _service.Bookings(filter));
    }

    /// <summary>
    /// Meta data about Booking records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BookingsMeta(
        [FromQuery()] BookingFindManyArgs filter
    )
    {
        return Ok(await _service.BookingsMeta(filter));
    }

    /// <summary>
    /// Get one Booking
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Booking>> Booking([FromRoute()] BookingWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Booking(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Booking
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateBooking(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
        [FromQuery()] BookingUpdateInput bookingUpdateDto
    )
    {
        try
        {
            await _service.UpdateBooking(uniqueId, bookingUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Flight record for Booking
    /// </summary>
    [HttpGet("{Id}/flight")]
    public async Task<ActionResult<List<Flight>>> GetFlight(
        [FromRoute()] BookingWhereUniqueInput uniqueId
    )
    {
        var flight = await _service.GetFlight(uniqueId);
        return Ok(flight);
    }

    /// <summary>
    /// Get a Passenger record for Booking
    /// </summary>
    [HttpGet("{Id}/passenger")]
    public async Task<ActionResult<List<Passenger>>> GetPassenger(
        [FromRoute()] BookingWhereUniqueInput uniqueId
    )
    {
        var passenger = await _service.GetPassenger(uniqueId);
        return Ok(passenger);
    }

    /// <summary>
    /// Connect multiple Seats records to Booking
    /// </summary>
    [HttpPost("{Id}/seats")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectSeats(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
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
    /// Disconnect multiple Seats records from Booking
    /// </summary>
    [HttpDelete("{Id}/seats")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectSeats(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
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
    /// Find multiple Seats records for Booking
    /// </summary>
    [HttpGet("{Id}/seats")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Seat>>> FindSeats(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
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
    /// Update multiple Seats records for Booking
    /// </summary>
    [HttpPatch("{Id}/seats")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateSeats(
        [FromRoute()] BookingWhereUniqueInput uniqueId,
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
