using ContiNet.Models;
using Microsoft.EntityFrameworkCore;

namespace ContiNet.Services
{
  public class ContinentService
  {
    private readonly ApplicationDbContext _context;

    public ContinentService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Boolean> Exists(int id)
    {
      return await _context.Continents.AnyAsync(c => c.Id == id);
    }

    public async Task<Boolean> ExistsByCountryId(int countryId)
    {
      return await _context.Continents
        .AnyAsync(c => c.Countries.Any(c => c.Id == countryId));
    }

    public async Task<List<Continent>> GetAll()
    {
      return await _context.Continents
        .Include(c => c.Countries)
        .ToListAsync();
    }

    public async Task<Continent?> GetById(int id)
    {
      return await _context.Continents
        .Include(c => c.Countries)
        .FirstAsync(c => c.Id == id);
    }

    public async Task<Continent?> GetByCountryId(int countryId)
    {
      return await _context.Continents
        .Include(c => c.Countries)
        .FirstOrDefaultAsync(c => c.Countries.Any(c => c.Id == countryId));
    }

    public async Task Create(Continent continent)
    {
      _context.Continents.Add(continent);
      await _context.SaveChangesAsync();
    }

    public async Task Update(Continent continent)
    {
      _context.Continents.Update(continent);
      await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
      var continent = await _context.Continents.FindAsync(id);
      if (continent != null)
      {
        _context.Continents.Remove(continent);
        await _context.SaveChangesAsync();
      }
    }
  }
}

