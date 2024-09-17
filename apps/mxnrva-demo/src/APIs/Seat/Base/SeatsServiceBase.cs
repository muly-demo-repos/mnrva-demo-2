using Microsoft.EntityFrameworkCore;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;
using MxnrvaDemo.APIs.Extensions;
using MxnrvaDemo.Infrastructure;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs;

public abstract class SeatsServiceBase : ISeatsService
{
    protected readonly MxnrvaDemoDbContext _context;

    public SeatsServiceBase(MxnrvaDemoDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Seat
    /// </summary>
    public async Task<Seat> CreateSeat(SeatCreateInput createDto)
    {
        var seat = new SeatDbModel
        {
            CreatedAt = createDto.CreatedAt,
            NumberField = createDto.NumberField,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            seat.Id = createDto.Id;
        }
        if (createDto.Booking != null)
        {
            seat.Booking = await _context
                .Bookings.Where(booking => createDto.Booking.Id == booking.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Flight != null)
        {
            seat.Flight = await _context
                .Flights.Where(flight => createDto.Flight.Id == flight.Id)
                .FirstOrDefaultAsync();
        }

        _context.Seats.Add(seat);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SeatDbModel>(seat.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Seat
    /// </summary>
    public async Task DeleteSeat(SeatWhereUniqueInput uniqueId)
    {
        var seat = await _context.Seats.FindAsync(uniqueId.Id);
        if (seat == null)
        {
            throw new NotFoundException();
        }

        _context.Seats.Remove(seat);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Seats
    /// </summary>
    public async Task<List<Seat>> Seats(SeatFindManyArgs findManyArgs)
    {
        var seats = await _context
            .Seats.Include(x => x.Flight)
            .Include(x => x.Booking)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return seats.ConvertAll(seat => seat.ToDto());
    }

    /// <summary>
    /// Meta data about Seat records
    /// </summary>
    public async Task<MetadataDto> SeatsMeta(SeatFindManyArgs findManyArgs)
    {
        var count = await _context.Seats.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Seat
    /// </summary>
    public async Task<Seat> Seat(SeatWhereUniqueInput uniqueId)
    {
        var seats = await this.Seats(
            new SeatFindManyArgs { Where = new SeatWhereInput { Id = uniqueId.Id } }
        );
        var seat = seats.FirstOrDefault();
        if (seat == null)
        {
            throw new NotFoundException();
        }

        return seat;
    }

    /// <summary>
    /// Update one Seat
    /// </summary>
    public async Task UpdateSeat(SeatWhereUniqueInput uniqueId, SeatUpdateInput updateDto)
    {
        var seat = updateDto.ToModel(uniqueId);

        if (updateDto.Booking != null)
        {
            seat.Booking = await _context
                .Bookings.Where(booking => updateDto.Booking == booking.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Flight != null)
        {
            seat.Flight = await _context
                .Flights.Where(flight => updateDto.Flight == flight.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(seat).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Seats.Any(e => e.Id == seat.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Booking record for Seat
    /// </summary>
    public async Task<Booking> GetBooking(SeatWhereUniqueInput uniqueId)
    {
        var seat = await _context
            .Seats.Where(seat => seat.Id == uniqueId.Id)
            .Include(seat => seat.Booking)
            .FirstOrDefaultAsync();
        if (seat == null)
        {
            throw new NotFoundException();
        }
        return seat.Booking.ToDto();
    }

    /// <summary>
    /// Get a Flight record for Seat
    /// </summary>
    public async Task<Flight> GetFlight(SeatWhereUniqueInput uniqueId)
    {
        var seat = await _context
            .Seats.Where(seat => seat.Id == uniqueId.Id)
            .Include(seat => seat.Flight)
            .FirstOrDefaultAsync();
        if (seat == null)
        {
            throw new NotFoundException();
        }
        return seat.Flight.ToDto();
    }
}
