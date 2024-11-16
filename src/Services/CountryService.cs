using ContiNet.Models;
using Microsoft.EntityFrameworkCore;

namespace ContiNet.Services
{
  public class CountryService
  {
    private readonly ApplicationDbContext _context;

    public CountryService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<List<Country>> GetAll()
    {
      return await _context.Countries
        .Include(c => c.Continent)
        .ToListAsync();
    }

    public async Task<Country?> GetById(int id)
    {
      return await _context.Countries
        .Include(c => c.Continent)
        .FirstAsync(c => c.Id == id);
    }

    public async Task Create(Country country)
    {
      _context.Countries.Add(country);
      await _context.SaveChangesAsync();
    }

    public async Task Update(Country country)
    {
      _context.Countries.Update(country);
      await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
      var country = await _context.Countries.FindAsync(id);
      if (country != null)
      {
        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();
      }
    }
  }
}

