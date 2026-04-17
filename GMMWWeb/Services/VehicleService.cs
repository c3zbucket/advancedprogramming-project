using Microsoft.EntityFrameworkCore;
using GMMWWeb.Data; 
using GMMWWeb.Data.Records;

namespace GMMWWeb.Services;

public class VehicleService
{
    private readonly IDbContextFactory<GMMWDBContext> _dbFactory;

    public VehicleService(IDbContextFactory<GMMWDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Vehicle>> GetVehicles()
    {
        using var db = _dbFactory.CreateDbContext();
        return await db.Vehicles.Include(v => v.owner).ToListAsync();
    }

    public async Task<Vehicle> SearchPlate(string plate)
    {
        await using var db = _dbFactory.CreateDbContext();
        return await db.Vehicles.Include(v => v.owner).FirstOrDefaultAsync(v => v.ID == plate);
    }

    public async Task Add(Vehicle vehicle)
    {
        await using var db = _dbFactory.CreateDbContext();
        db.Vehicles.Add(vehicle);
        await db.SaveChangesAsync();
    }

    public async Task UpdateVehicle(Vehicle vehicle)
    {
        using var db = _dbFactory.CreateDbContext();
        db.Vehicles.Update(vehicle);
        await db.SaveChangesAsync();
    }

    public async Task DeleteVehicle(string plate)
    {
        using var db = _dbFactory.CreateDbContext();
        var vehicle = await SearchPlate(plate);
        if (vehicle != null)
        {
            db.Vehicles.Remove(vehicle);
            await db.SaveChangesAsync();
        }
    }
}

