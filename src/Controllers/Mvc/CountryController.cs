using ContiNet.Models;
using ContiNet.Services;
using ContiNet.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ContiNet.Controllers.Mvc
{
  public class CountryController : Controller
  {
    private readonly ContinentService _continentService;
    private readonly CountryService _countryService;
    private readonly IMapper _mapper;

    public CountryController(
      CountryService countryService,
      ContinentService continentService,
      IMapper mapper
    )
    {
      _countryService = countryService;
      _continentService = continentService;
      _mapper = mapper;
    }

    // GET: Country
    public async Task<IActionResult> Index()
    {
      var countries = await _countryService.GetAll();
      var countryDTOs = _mapper.Map<List<CountryDTO>>(countries);
      return View(countryDTOs);
    }

    // GET: Country/Details/:id
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null) return NotFound();

      var country = await _countryService.GetById(id.Value);
      if (country == null) return NotFound();

      var countryDTO = _mapper.Map<CountryDTO>(country);
      return View(countryDTO);
    }

    // GET: Country/Create
    public async Task<IActionResult> Create()
    {
      var continents = await _continentService.GetAll();
      ViewBag.Continents = continents;
      return View();
    }

    // POST: Country/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Country country)
    {
      if (ModelState.IsValid)
      {
        await _countryService.Create(country);
        return RedirectToAction(nameof(Index));
      }

      var continents = await _continentService.GetAll();
      ViewBag.Continents = continents;
      return View(country);
    }

    // GET: Country/Edit/:id
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null) return NotFound();

      var country = await _countryService.GetById(id.Value);
      if (country == null) return NotFound();

      var continents = await _continentService.GetAll();
      ViewBag.Continents = continents;
      return View(country);
    }

    // POST: Country/Edit/:id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Country country)
    {
      if (id != country.Id) return NotFound();

      if (ModelState.IsValid)
      {
        try
        {
          await _countryService.Update(country);
          return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
          if (!await _continentService.ExistsByCountryId(country.Id))
            return NotFound();

          throw;
        }
      }

      var continents = await _continentService.GetAll();
      var continentDTOs = _mapper.Map<List<ContinentDTO>>(continents);
      ViewBag.Continents = continentDTOs;
      return View(country);
    }

    // GET: Country/Delete/:id
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null) return NotFound();

      var country = await _countryService.GetById(id.Value);
      if (country == null) return NotFound();

      var countryDTO = _mapper.Map<CountryDTO>(country);
      return View(countryDTO);
    }

    // POST: Country/Delete/:id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _countryService.Delete(id);
      return RedirectToAction(nameof(Index));
    }
  }
}
