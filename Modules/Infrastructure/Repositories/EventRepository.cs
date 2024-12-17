using System.Net;
using CareTrack.Server.Modules.Domain.Models;
using CareTrack.Server.Modules.Domain.Repositories;
using CareTrack.Server.Modules.Infrastructure.Entities;
using CareTrack.Server.Modules.Infrastructure.presistance;
using Microsoft.EntityFrameworkCore;
using Sprache;

namespace CareTrack.Server.Modules.Infrastructure.Repositories;

public class EventRepository(CareTrackDbContext context) : IEventRepository
{
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
    public async Task<Result<IEvent>> Add(IEvent ev)
    {
        try
        {
            var eventToAdd = new Event
            {
                Name = ev.Name,
                Description = ev.Description,
                Date = ev.Date.ToUniversalTime(),
                PatientId = ev.PatientId
            };
            context.Events.Add(eventToAdd);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return Result<IEvent>.Error("Błąd podczas dodawania zdarzenia: " + e.Message, HttpStatusCode.BadRequest);
        }
        return new Result<IEvent>(ev);
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