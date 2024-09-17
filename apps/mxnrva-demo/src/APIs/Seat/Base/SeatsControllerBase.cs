using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;

namespace MxnrvaDemo.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SeatsControllerBase : ControllerBase
{
    protected readonly ISeatsService _service;

    public SeatsControllerBase(ISeatsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Seat
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Seat>> CreateSeat(SeatCreateInput input)
    {
        var seat = await _service.CreateSeat(input);

        return CreatedAtAction(nameof(Seat), new { id = seat.Id }, seat);
    }

    /// <summary>
    /// Delete one Seat
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteSeat([FromRoute()] SeatWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSeat(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Seats
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Seat>>> Seats([FromQuery()] SeatFindManyArgs filter)
    {
        return Ok(await _service.Seats(filter));
    }

    /// <summary>
    /// Meta data about Seat records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SeatsMeta([FromQuery()] SeatFindManyArgs filter)
    {
        return Ok(await _service.SeatsMeta(filter));
    }

    /// <summary>
    /// Get one Seat
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Seat>> Seat([FromRoute()] SeatWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Seat(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Seat
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateSeat(
        [FromRoute()] SeatWhereUniqueInput uniqueId,
        [FromQuery()] SeatUpdateInput seatUpdateDto
    )
    {
        try
        {
            await _service.UpdateSeat(uniqueId, seatUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Booking record for Seat
    /// </summary>
    [HttpGet("{Id}/booking")]
    public async Task<ActionResult<List<Booking>>> GetBooking(
        [FromRoute()] SeatWhereUniqueInput uniqueId
    )
    {
        var booking = await _service.GetBooking(uniqueId);
        return Ok(booking);
    }

    /// <summary>
    /// Get a Flight record for Seat
    /// </summary>
    [HttpGet("{Id}/flight")]
    public async Task<ActionResult<List<Flight>>> GetFlight(
        [FromRoute()] SeatWhereUniqueInput uniqueId
    )
    {
        var flight = await _service.GetFlight(uniqueId);
        return Ok(flight);
    }
}
