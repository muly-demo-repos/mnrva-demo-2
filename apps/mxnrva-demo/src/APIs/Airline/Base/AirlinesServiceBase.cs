using Microsoft.EntityFrameworkCore;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;
using MxnrvaDemo.APIs.Extensions;
using MxnrvaDemo.Infrastructure;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs;

public abstract class AirlinesServiceBase : IAirlinesService
{
    protected readonly MxnrvaDemoDbContext _context;

    public AirlinesServiceBase(MxnrvaDemoDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Airline
    /// </summary>
    public async Task<Airline> CreateAirline(AirlineCreateInput createDto)
    {
        var airline = new AirlineDbModel
        {
            Country = createDto.Country,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            airline.Id = createDto.Id;
        }
        if (createDto.AircraftItems != null)
        {
            airline.AircraftItems = await _context
                .AircraftItems.Where(aircraft =>
                    createDto.AircraftItems.Select(t => t.Id).Contains(aircraft.Id)
                )
                .ToListAsync();
        }

        if (createDto.Flights != null)
        {
            airline.Flights = await _context
                .Flights.Where(flight => createDto.Flights.Select(t => t.Id).Contains(flight.Id))
                .ToListAsync();
        }

        _context.Airlines.Add(airline);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AirlineDbModel>(airline.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Airline
    /// </summary>
    public async Task DeleteAirline(AirlineWhereUniqueInput uniqueId)
    {
        var airline = await _context.Airlines.FindAsync(uniqueId.Id);
        if (airline == null)
        {
            throw new NotFoundException();
        }

        _context.Airlines.Remove(airline);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Airlines
    /// </summary>
    public async Task<List<Airline>> Airlines(AirlineFindManyArgs findManyArgs)
    {
        var airlines = await _context
            .Airlines.Include(x => x.Flights)
            .Include(x => x.AircraftItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return airlines.ConvertAll(airline => airline.ToDto());
    }

    /// <summary>
    /// Meta data about Airline records
    /// </summary>
    public async Task<MetadataDto> AirlinesMeta(AirlineFindManyArgs findManyArgs)
    {
        var count = await _context.Airlines.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Airline
    /// </summary>
    public async Task<Airline> Airline(AirlineWhereUniqueInput uniqueId)
    {
        var airlines = await this.Airlines(
            new AirlineFindManyArgs { Where = new AirlineWhereInput { Id = uniqueId.Id } }
        );
        var airline = airlines.FirstOrDefault();
        if (airline == null)
        {
            throw new NotFoundException();
        }

        return airline;
    }

    /// <summary>
    /// Update one Airline
    /// </summary>
    public async Task UpdateAirline(AirlineWhereUniqueInput uniqueId, AirlineUpdateInput updateDto)
    {
        var airline = updateDto.ToModel(uniqueId);

        if (updateDto.AircraftItems != null)
        {
            airline.AircraftItems = await _context
                .AircraftItems.Where(aircraft =>
                    updateDto.AircraftItems.Select(t => t).Contains(aircraft.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Flights != null)
        {
            airline.Flights = await _context
                .Flights.Where(flight => updateDto.Flights.Select(t => t).Contains(flight.Id))
                .ToListAsync();
        }

        _context.Entry(airline).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Airlines.Any(e => e.Id == airline.Id))
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
    /// Connect multiple AircraftItems records to Airline
    /// </summary>
    public async Task ConnectAircraftItems(
        AirlineWhereUniqueInput uniqueId,
        AircraftWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Airlines.Include(x => x.AircraftItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .AircraftItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.AircraftItems);

        foreach (var child in childrenToConnect)
        {
            parent.AircraftItems.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple AircraftItems records from Airline
    /// </summary>
    public async Task DisconnectAircraftItems(
        AirlineWhereUniqueInput uniqueId,
        AircraftWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Airlines.Include(x => x.AircraftItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .AircraftItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.AircraftItems?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple AircraftItems records for Airline
    /// </summary>
    public async Task<List<Aircraft>> FindAircraftItems(
        AirlineWhereUniqueInput uniqueId,
        AircraftFindManyArgs airlineFindManyArgs
    )
    {
        var aircraftItems = await _context
            .AircraftItems.Where(m => m.AirlineId == uniqueId.Id)
            .ApplyWhere(airlineFindManyArgs.Where)
            .ApplySkip(airlineFindManyArgs.Skip)
            .ApplyTake(airlineFindManyArgs.Take)
            .ApplyOrderBy(airlineFindManyArgs.SortBy)
            .ToListAsync();

        return aircraftItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple AircraftItems records for Airline
    /// </summary>
    public async Task UpdateAircraftItems(
        AirlineWhereUniqueInput uniqueId,
        AircraftWhereUniqueInput[] childrenIds
    )
    {
        var airline = await _context
            .Airlines.Include(t => t.AircraftItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (airline == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .AircraftItems.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        airline.AircraftItems = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Flights records to Airline
    /// </summary>
    public async Task ConnectFlights(
        AirlineWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Airlines.Include(x => x.Flights)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Flights.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Flights);

        foreach (var child in childrenToConnect)
        {
            parent.Flights.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Flights records from Airline
    /// </summary>
    public async Task DisconnectFlights(
        AirlineWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Airlines.Include(x => x.Flights)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Flights.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Flights?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Flights records for Airline
    /// </summary>
    public async Task<List<Flight>> FindFlights(
        AirlineWhereUniqueInput uniqueId,
        FlightFindManyArgs airlineFindManyArgs
    )
    {
        var flights = await _context
            .Flights.Where(m => m.AirlineId == uniqueId.Id)
            .ApplyWhere(airlineFindManyArgs.Where)
            .ApplySkip(airlineFindManyArgs.Skip)
            .ApplyTake(airlineFindManyArgs.Take)
            .ApplyOrderBy(airlineFindManyArgs.SortBy)
            .ToListAsync();

        return flights.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Flights records for Airline
    /// </summary>
    public async Task UpdateFlights(
        AirlineWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] childrenIds
    )
    {
        var airline = await _context
            .Airlines.Include(t => t.Flights)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (airline == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Flights.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        airline.Flights = children;
        await _context.SaveChangesAsync();
    }
}
