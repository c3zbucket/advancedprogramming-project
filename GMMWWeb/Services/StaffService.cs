using Microsoft.EntityFrameworkCore;
using GMMWWeb.Data; 
using GMMWWeb.Data.Records;

namespace GMMWWeb.Services;

public class StaffService
{
    private readonly IDbContextFactory<GMMWDBContext> _dbFactory;

    public StaffService(IDbContextFactory<GMMWDBContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<List<Staff>> GetStaff()
    {
        await using var db = _dbFactory.CreateDbContext();
        return await db.Staff.ToListAsync();
    }
}