using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GMMWWeb.Data;
using GMMWWeb.Data.Records;

namespace GMMWWeb.Services;

public class MotoristService
{
    private readonly IDbContextFactory<GMMWDBContext> _dbFactory;

    public MotoristService(IDbContextFactory<GMMWDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Motorist>> GetMotorists()
    {
        using var db = _dbFactory.CreateDbContext();
        return await db.Motorists.ToListAsync();
    }

public async Task<Motorist> genID(Motorist motor)
{
    using var db = _dbFactory.CreateDbContext();

    var lastMotorist = await db.Motorists
        .OrderByDescending(m => m.ID)
        .FirstOrDefaultAsync();

    int nextNumber = 1000;
    if (lastMotorist != null)
    {
        var lastNumber = int.Parse(lastMotorist.ID);
        nextNumber = ++lastNumber;
    }

    motor.ID = nextNumber.ToString();
    return motor;
}

    public async Task<Motorist> GetMotoristById(string id)
    {
        using var db = _dbFactory.CreateDbContext();
        return await db.Motorists.FirstOrDefaultAsync(m => m.ID == id);
    }

    public async Task Add(Motorist motorist)
    {
        motorist = await genID(motorist);
        await using var db = _dbFactory.CreateDbContext();
        db.Motorists.Add(motorist);
        await db.SaveChangesAsync();
    }

    public async Task Update(Motorist motorist)
    {
        using var db = _dbFactory.CreateDbContext();
        db.Motorists.Update(motorist);
        await db.SaveChangesAsync();
    }

    public async Task Delete(string id)
    {
        using var db = _dbFactory.CreateDbContext();
        var motorist = await GetMotoristById(id);
        if (motorist != null)
        {
            db.Motorists.Remove(motorist);
            await db.SaveChangesAsync();
        }
    }
}