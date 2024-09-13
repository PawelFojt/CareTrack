using System.Net;
using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using CareTrack.Server.Modules.Infrastructure.Entities;
using CareTrack.Server.Modules.Infrastructure.presistance;
using Microsoft.EntityFrameworkCore;

namespace CareTrack.Server.Modules.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly CareTrackDbContext context;
    public EventRepository(CareTrackDbContext context)
    {
        this.context = context;
    }
    public async Task<Result<IEnumerable<IEvent>>> List()
    {
        var events = await context.Events.ToListAsync();
        foreach (var e in events)
        {
            e.Patient = await context.Patients.FindAsync(e.PatientId);
        }
        return new Result<IEnumerable<IEvent>>(events);
    }
    
    public async Task<Result<IEnumerable<IEvent>>> ListByPatientId(int patientId)
    {
        var events = await context.Events.Where(e => e.PatientId == patientId).ToListAsync();
       
        return new Result<IEnumerable<IEvent>>(events);
    }
    public async Task<Result<IEvent>> Get(int id)
    {
        var e = await context.Events.FindAsync(id);
        if (e == null)
        {
            return Result<IEvent>.Error("Nie znaleziono wydarzenia", HttpStatusCode.NotFound);
        }
        e.Patient = await context.Patients.FindAsync(e.PatientId);
        return new Result<IEvent>(e);
    }
    public async Task<Result<IEvent>> Add(IEvent e)
    {
        var eventToAdd = new Event
        {
            Name = e.Name,
            Description = e.Description,
            Date = e.Date,
            PatientId = e.PatientId
        };
        context.Events.Add(eventToAdd);
        await context.SaveChangesAsync();
        return new Result<IEvent>(e);
    }
    public async Task<Result<IEvent>> Update(IEvent e)
    {
        var eventToUpdate = await context.Events.FindAsync(e.Id);
        if (eventToUpdate == null)
        {
            return Result<IEvent>.Error("Nie znaleziono wydarzenia", HttpStatusCode.NotFound);
        }
        eventToUpdate.Name = e.Name;
        eventToUpdate.Description = e.Description;
        eventToUpdate.Date = e.Date;
        eventToUpdate.PatientId = e.PatientId;
        await context.SaveChangesAsync();
        return new Result<IEvent>(e);
    }
    public async Task<Result<IEvent>> Delete(int id)
    {
        var e = await context.Events.FindAsync(id);
        if (e == null)
        {
            return Result<IEvent>.Error("Nie znaleziono wydarzenia", HttpStatusCode.NotFound);
        }
        context.Events.Remove(e);
        await context.SaveChangesAsync();
        return new Result<IEvent>(e);
    }
}