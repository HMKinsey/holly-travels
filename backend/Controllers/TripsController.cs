using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TripsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trip>>> GetTrips()
    {
        return Ok(await _db.Trips.OrderBy(t => t.StartDate).ToListAsync());
    }

    [HttpPost]
    public async Task<ActionResult<Trip>> CreateTrip(Trip trip)
    {
        trip.Id = Guid.NewGuid();
        _db.Trips.Add(trip);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTrips), new { id = trip.Id }, trip);
    }
}