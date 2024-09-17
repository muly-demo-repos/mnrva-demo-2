using Microsoft.EntityFrameworkCore;
using MxnrvaDemo.APIs;
using MxnrvaDemo.APIs.Common;
using MxnrvaDemo.APIs.Dtos;
using MxnrvaDemo.APIs.Errors;
using MxnrvaDemo.APIs.Extensions;
using MxnrvaDemo.Infrastructure;
using MxnrvaDemo.Infrastructure.Models;

namespace MxnrvaDemo.APIs;

public abstract class AircraftItemsServiceBase : IAircraftItemsService
{
    protected readonly MxnrvaDemoDbContext _context;

    public AircraftItemsServiceBase(MxnrvaDemoDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Aircraft
    /// </summary>
    public async Task<Aircraft> CreateAircraft(AircraftCreateInput createDto)
    {
        var aircraft = new AircraftDbModel
        {
            Capacity = createDto.Capacity,
            CreatedAt = createDto.CreatedAt,
            Model = createDto.Model,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            aircraft.Id = createDto.Id;
        }
        if (createDto.Airline != null)
        {
            aircraft.Airline = await _context
                .Airlines.Where(airline => createDto.Airline.Id == airline.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Flights != null)
        {
            aircraft.Flights = await _context
                .Flights.Where(flight => createDto.Flights.Select(t => t.Id).Contains(flight.Id))
                .ToListAsync();
        }

        _context.AircraftItems.Add(aircraft);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<AircraftDbModel>(aircraft.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Aircraft
    /// </summary>
    public async Task DeleteAircraft(AircraftWhereUniqueInput uniqueId)
    {
        var aircraft = await _context.AircraftItems.FindAsync(uniqueId.Id);
        if (aircraft == null)
        {
            throw new NotFoundException();
        }

        _context.AircraftItems.Remove(aircraft);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many AircraftItems
    /// </summary>
    public async Task<List<Aircraft>> AircraftItems(AircraftFindManyArgs findManyArgs)
    {
        var aircraftItems = await _context
            .AircraftItems.Include(x => x.Flights)
            .Include(x => x.Airline)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return aircraftItems.ConvertAll(aircraft => aircraft.ToDto());
    }

    /// <summary>
    /// Meta data about Aircraft records
    /// </summary>
    public async Task<MetadataDto> AircraftItemsMeta(AircraftFindManyArgs findManyArgs)
    {
        var count = await _context.AircraftItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Aircraft
    /// </summary>
    public async Task<Aircraft> Aircraft(AircraftWhereUniqueInput uniqueId)
    {
        var aircraftItems = await this.AircraftItems(
            new AircraftFindManyArgs { Where = new AircraftWhereInput { Id = uniqueId.Id } }
        );
        var aircraft = aircraftItems.FirstOrDefault();
        if (aircraft == null)
        {
            throw new NotFoundException();
        }

        return aircraft;
    }

    /// <summary>
    /// Update one Aircraft
    /// </summary>
    public async Task UpdateAircraft(
        AircraftWhereUniqueInput uniqueId,
        AircraftUpdateInput updateDto
    )
    {
        var aircraft = updateDto.ToModel(uniqueId);

        if (updateDto.Airline != null)
        {
            aircraft.Airline = await _context
                .Airlines.Where(airline => updateDto.Airline == airline.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Flights != null)
        {
            aircraft.Flights = await _context
                .Flights.Where(flight => updateDto.Flights.Select(t => t).Contains(flight.Id))
                .ToListAsync();
        }

        _context.Entry(aircraft).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.AircraftItems.Any(e => e.Id == aircraft.Id))
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
    /// Get a Airline record for Aircraft
    /// </summary>
    public async Task<Airline> GetAirline(AircraftWhereUniqueInput uniqueId)
    {
        var aircraft = await _context
            .AircraftItems.Where(aircraft => aircraft.Id == uniqueId.Id)
            .Include(aircraft => aircraft.Airline)
            .FirstOrDefaultAsync();
        if (aircraft == null)
        {
            throw new NotFoundException();
        }
        return aircraft.Airline.ToDto();
    }

    /// <summary>
    /// Connect multiple Flights records to Aircraft
    /// </summary>
    public async Task ConnectFlights(
        AircraftWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .AircraftItems.Include(x => x.Flights)
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
    /// Disconnect multiple Flights records from Aircraft
    /// </summary>
    public async Task DisconnectFlights(
        AircraftWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .AircraftItems.Include(x => x.Flights)
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
    /// Find multiple Flights records for Aircraft
    /// </summary>
    public async Task<List<Flight>> FindFlights(
        AircraftWhereUniqueInput uniqueId,
        FlightFindManyArgs aircraftFindManyArgs
    )
    {
        var flights = await _context
            .Flights.Where(m => m.AircraftId == uniqueId.Id)
            .ApplyWhere(aircraftFindManyArgs.Where)
            .ApplySkip(aircraftFindManyArgs.Skip)
            .ApplyTake(aircraftFindManyArgs.Take)
            .ApplyOrderBy(aircraftFindManyArgs.SortBy)
            .ToListAsync();

        return flights.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Flights records for Aircraft
    /// </summary>
    public async Task UpdateFlights(
        AircraftWhereUniqueInput uniqueId,
        FlightWhereUniqueInput[] childrenIds
    )
    {
        var aircraft = await _context
            .AircraftItems.Include(t => t.Flights)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (aircraft == null)
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

        aircraft.Flights = children;
        await _context.SaveChangesAsync();
    }
}
