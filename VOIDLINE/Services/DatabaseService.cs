using Microsoft.EntityFrameworkCore;
using VOIDLINE.Models;

namespace VOIDLINE.Services;

public class DatabaseService<T> where T : class
{
    private readonly DBContext _context;

    public DatabaseService(DBContext context)
    {
        _context = context;
    }

    public async Task<List<T>> ReadFunc() 
    {
        List<T> list = await _context.Set<T>()
            .ToListAsync();

        return list;
    }

    public async Task FillForeignTables()
    {
        _ = await _context.Categoryvariants
            .Include(v => v.Categories)
            .ToListAsync();
    }
}
