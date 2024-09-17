using Microsoft.EntityFrameworkCore;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;
using MxnrvaDemo.APIs.Extensions;
using MxnrvaDemo.Infrastructure;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs;

public abstract class FlightsServiceBase : IFlightsService
{
    protected readonly MxnrvaDemoDbContext _context;

    public FlightsServiceBase(MxnrvaDemoDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Flight
    /// </summary>
    public async Task<Flight> CreateFlight(FlightCreateInput createDto)
    {
        var flight = new FlightDbModel
        {
            ArrivalTime = createDto.ArrivalTime,
            CreatedAt = createDto.CreatedAt,
            DepartureTime = createDto.DepartureTime,
            FlightNumber = createDto.FlightNumber,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            flight.Id = createDto.Id;
        }
        if (createDto.Aircraft != null)
        {
            flight.Aircraft = await _context
                .AircraftItems.Where(aircraft => createDto.Aircraft.Id == aircraft.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Airline != null)
        {
            flight.Airline = await _context
                .Airlines.Where(airline => createDto.Airline.Id == airline.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Bookings != null)
        {
            flight.Bookings = await _context
                .Bookings.Where(booking =>
                    createDto.Bookings.Select(t => t.Id).Contains(booking.Id)
                )
                .ToListAsync();
        }

        if (createDto.Seats != null)
        {
            flight.Seats = await _context
                .Seats.Where(seat => createDto.Seats.Select(t => t.Id).Contains(seat.Id))
                .ToListAsync();
        }

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FlightDbModel>(flight.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Flight
    /// </summary>
    public async Task DeleteFlight(FlightWhereUniqueInput uniqueId)
    {
        var flight = await _context.Flights.FindAsync(uniqueId.Id);
        if (flight == null)
        {
            throw new NotFoundException();
        }

        _context.Flights.Remove(flight);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Flights
    /// </summary>
    public async Task<List<Flight>> Flights(FlightFindManyArgs findManyArgs)
    {
        var flights = await _context
            .Flights.Include(x => x.Airline)
            .Include(x => x.Seats)
            .Include(x => x.Bookings)
            .Include(x => x.Aircraft)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return flights.ConvertAll(flight => flight.ToDto());
    }

    /// <summary>
    /// Meta data about Flight records
    /// </summary>
    public async Task<MetadataDto> FlightsMeta(FlightFindManyArgs findManyArgs)
    {
        var count = await _context.Flights.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Flight
    /// </summary>
    public async Task<Flight> Flight(FlightWhereUniqueInput uniqueId)
    {
        var flights = await this.Flights(
            new FlightFindManyArgs { Where = new FlightWhereInput { Id = uniqueId.Id } }
        );
        var flight = flights.FirstOrDefault();
        if (flight == null)
        {
            throw new NotFoundException();
        }

        return flight;
    }

    /// <summary>
    /// Update one Flight
    /// </summary>
    public async Task UpdateFlight(FlightWhereUniqueInput uniqueId, FlightUpdateInput updateDto)
    {
        var flight = updateDto.ToModel(uniqueId);

        if (updateDto.Aircraft != null)
        {
            flight.Aircraft = await _context
                .AircraftItems.Where(aircraft => updateDto.Aircraft == aircraft.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Airline != null)
        {
            flight.Airline = await _context
                .Airlines.Where(airline => updateDto.Airline == airline.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Bookings != null)
        {
            flight.Bookings = await _context
                .Bookings.Where(booking => updateDto.Bookings.Select(t => t).Contains(booking.Id))
                .ToListAsync();
        }

        if (updateDto.Seats != null)
        {
            flight.Seats = await _context
                .Seats.Where(seat => updateDto.Seats.Select(t => t).Contains(seat.Id))
                .ToListAsync();
        }

        _context.Entry(flight).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Flights.Any(e => e.Id == flight.Id))
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
    /// Get a Aircraft record for Flight
    /// </summary>
    public async Task<Aircraft> GetAircraft(FlightWhereUniqueInput uniqueId)
    {
        var flight = await _context
            .Flights.Where(flight => flight.Id == uniqueId.Id)
            .Include(flight => flight.Aircraft)
            .FirstOrDefaultAsync();
        if (flight == null)
        {
            throw new NotFoundException();
        }
        return flight.Aircraft.ToDto();
    }

    /// <summary>
    /// Get a Airline record for Flight
    /// </summary>
    public async Task<Airline> GetAirline(FlightWhereUniqueInput uniqueId)
    {
        var flight = await _context
            .Flights.Where(flight => flight.Id == uniqueId.Id)
            .Include(flight => flight.Airline)
            .FirstOrDefaultAsync();
        if (flight == null)
        {
            throw new NotFoundException();
        }
        return flight.Airline.ToDto();
    }

    /// <summary>
    /// Connect multiple Bookings records to Flight
    /// </summary>
    public async Task ConnectBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Flights.Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Bookings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Bookings);

        foreach (var child in childrenToConnect)
        {
            parent.Bookings.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Bookings records from Flight
    /// </summary>
    public async Task DisconnectBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Flights.Include(x => x.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Bookings.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Bookings?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Bookings records for Flight
    /// </summary>
    public async Task<List<Booking>> FindBookings(
        FlightWhereUniqueInput uniqueId,
        BookingFindManyArgs flightFindManyArgs
    )
    {
        var bookings = await _context
            .Bookings.Where(m => m.FlightId == uniqueId.Id)
            .ApplyWhere(flightFindManyArgs.Where)
            .ApplySkip(flightFindManyArgs.Skip)
            .ApplyTake(flightFindManyArgs.Take)
            .ApplyOrderBy(flightFindManyArgs.SortBy)
            .ToListAsync();

        return bookings.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Bookings records for Flight
    /// </summary>
    public async Task UpdateBookings(
        FlightWhereUniqueInput uniqueId,
        BookingWhereUniqueInput[] childrenIds
    )
    {
        var flight = await _context
            .Flights.Include(t => t.Bookings)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (flight == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Bookings.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        flight.Bookings = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Seats records to Flight
    /// </summary>
    public async Task ConnectSeats(
        FlightWhereUniqueInput uniqueId,
        SeatWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Flights.Include(x => x.Seats)
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
    /// Disconnect multiple Seats records from Flight
    /// </summary>
    public async Task DisconnectSeats(
        FlightWhereUniqueInput uniqueId,
        SeatWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Flights.Include(x => x.Seats)
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
    /// Find multiple Seats records for Flight
    /// </summary>
    public async Task<List<Seat>> FindSeats(
        FlightWhereUniqueInput uniqueId,
        SeatFindManyArgs flightFindManyArgs
    )
    {
        var seats = await _context
            .Seats.Where(m => m.FlightId == uniqueId.Id)
            .ApplyWhere(flightFindManyArgs.Where)
            .ApplySkip(flightFindManyArgs.Skip)
            .ApplyTake(flightFindManyArgs.Take)
            .ApplyOrderBy(flightFindManyArgs.SortBy)
            .ToListAsync();

        return seats.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Seats records for Flight
    /// </summary>
    public async Task UpdateSeats(
        FlightWhereUniqueInput uniqueId,
        SeatWhereUniqueInput[] childrenIds
    )
    {
        var flight = await _context
            .Flights.Include(t => t.Seats)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (flight == null)
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

        flight.Seats = children;
        await _context.SaveChangesAsync();
    }
}
