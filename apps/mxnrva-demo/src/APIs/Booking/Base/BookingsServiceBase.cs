using Microsoft.EntityFrameworkCore;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;
using MxnrvaDemo.APIs.Extensions;
using MxnrvaDemo.Infrastructure;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs;

public abstract class BookingsServiceBase : IBookingsService
{
    protected readonly MxnrvaDemoDbContext _context;

    public BookingsServiceBase(MxnrvaDemoDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Booking
    /// </summary>
    public async Task<Booking> CreateBooking(BookingCreateInput createDto)
    {
        var booking = new BookingDbModel
        {
            BookingDate = createDto.BookingDate,
            CreatedAt = createDto.CreatedAt,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            booking.Id = createDto.Id;
        }
        if (createDto.Flight != null)
        {
            booking.Flight = await _context
                .Flights.Where(flight => createDto.Flight.Id == flight.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Passenger != null)
        {
            booking.Passenger = await _context
                .Passengers.Where(passenger => createDto.Passenger.Id == passenger.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Seats != null)
        {
            booking.Seats = await _context
                .Seats.Where(seat => createDto.Seats.Select(t => t.Id).Contains(seat.Id))
                .ToListAsync();
        }

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BookingDbModel>(booking.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Booking
    /// </summary>
    public async Task DeleteBooking(BookingWhereUniqueInput uniqueId)
    {
        var booking = await _context.Bookings.FindAsync(uniqueId.Id);
        if (booking == null)
        {
            throw new NotFoundException();
        }

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Bookings
    /// </summary>
    public async Task<List<Booking>> Bookings(BookingFindManyArgs findManyArgs)
    {
        var bookings = await _context
            .Bookings.Include(x => x.Passenger)
            .Include(x => x.Flight)
            .Include(x => x.Seats)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return bookings.ConvertAll(booking => booking.ToDto());
    }

    /// <summary>
    /// Meta data about Booking records
    /// </summary>
    public async Task<MetadataDto> BookingsMeta(BookingFindManyArgs findManyArgs)
    {
        var count = await _context.Bookings.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Booking
    /// </summary>
    public async Task<Booking> Booking(BookingWhereUniqueInput uniqueId)
    {
        var bookings = await this.Bookings(
            new BookingFindManyArgs { Where = new BookingWhereInput { Id = uniqueId.Id } }
        );
        var booking = bookings.FirstOrDefault();
        if (booking == null)
        {
            throw new NotFoundException();
        }

        return booking;
    }

    /// <summary>
    /// Update one Booking
    /// </summary>
    public async Task UpdateBooking(BookingWhereUniqueInput uniqueId, BookingUpdateInput updateDto)
    {
        var booking = updateDto.ToModel(uniqueId);

        if (updateDto.Flight != null)
        {
            booking.Flight = await _context
                .Flights.Where(flight => updateDto.Flight == flight.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Passenger != null)
        {
            booking.Passenger = await _context
                .Passengers.Where(passenger => updateDto.Passenger == passenger.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Seats != null)
        {
            booking.Seats = await _context
                .Seats.Where(seat => updateDto.Seats.Select(t => t).Contains(seat.Id))
                .ToListAsync();
        }

        _context.Entry(booking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Bookings.Any(e => e.Id == booking.Id))
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
    /// Get a Flight record for Booking
    /// </summary>
    public async Task<Flight> GetFlight(BookingWhereUniqueInput uniqueId)
    {
        var booking = await _context
            .Bookings.Where(booking => booking.Id == uniqueId.Id)
            .Include(booking => booking.Flight)
            .FirstOrDefaultAsync();
        if (booking == null)
        {
            throw new NotFoundException();
        }
        return booking.Flight.ToDto();
    }

    /// <summary>
    /// Get a Passenger record for Booking
    /// </summary>
    public async Task<Passenger> GetPassenger(BookingWhereUniqueInput uniqueId)
    {
        var booking = await _context
            .Bookings.Where(booking => booking.Id == uniqueId.Id)
            .Include(booking => booking.Passenger)
            .FirstOrDefaultAsync();
        if (booking == null)
        {
            throw new NotFoundException();
        }
        return booking.Passenger.ToDto();
    }

    /// <summary>
    /// Connect multiple Seats records to Booking
    /// </summary>
    public async Task ConnectSeats(
        BookingWhereUniqueInput uniqueId,
        SeatWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Bookings.Include(x => x.Seats)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Seats.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Seats);

        foreach (var child in childrenToConnect)
        {
            parent.Seats.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Seats records from Booking
    /// </summary>
    public async Task DisconnectSeats(
        BookingWhereUniqueInput uniqueId,
        SeatWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Bookings.Include(x => x.Seats)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Seats.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Seats?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Seats records for Booking
    /// </summary>
    public async Task<List<Seat>> FindSeats(
        BookingWhereUniqueInput uniqueId,
        SeatFindManyArgs bookingFindManyArgs
    )
    {
        var seats = await _context
            .Seats.Where(m => m.BookingId == uniqueId.Id)
            .ApplyWhere(bookingFindManyArgs.Where)
            .ApplySkip(bookingFindManyArgs.Skip)
            .ApplyTake(bookingFindManyArgs.Take)
            .ApplyOrderBy(bookingFindManyArgs.SortBy)
            .ToListAsync();

        return seats.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Seats records for Booking
    /// </summary>
    public async Task UpdateSeats(
        BookingWhereUniqueInput uniqueId,
        SeatWhereUniqueInput[] childrenIds
    )
    {
        var booking = await _context
            .Bookings.Include(t => t.Seats)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (booking == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Seats.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        booking.Seats = children;
        await _context.SaveChangesAsync();
    }
}
