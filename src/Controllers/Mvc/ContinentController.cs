using ContiNet.Models;
using ContiNet.Services;
using ContiNet.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ContiNet.Controllers.Mvc
{
  public class ContinentController : Controller
  {
    private readonly ContinentService _continentService;
    private readonly IMapper _mapper;

    public ContinentController(ContinentService continentService, IMapper mapper)
    {
      _continentService = continentService;
      _mapper = mapper;
    }

    // GET: Continent
    public async Task<IActionResult> Index()
    {
      var continents = await _continentService.GetAll();
      var continentDTOs = _mapper.Map<List<ContinentDTO>>(continents);
      return View(continentDTOs);
    }

    // GET: Continent/Details/:id
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null) return NotFound();

      var continent = await _continentService.GetById(id.Value);
      if (continent == null) return NotFound();

      var continentDTO = _mapper.Map<ContinentDTO>(continent);
      return View(continentDTO);
    }

    // GET: Continent/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Continent/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Continent continent)
    {
      if (ModelState.IsValid)
      {
        await _continentService.Create(continent);
        return RedirectToAction(nameof(Index));
      }
      return View(continent);
    }

    // GET: Continent/Edit/:id
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null) return NotFound();

      var continent = await _continentService.GetById(id.Value);
      if (continent == null) return NotFound();

      return View(continent);
    }

    // POST: Continent/Edit/:id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Continent continent)
    {
      Console.WriteLine("id: " + id);
      Console.WriteLine("continent.Id: " + continent.Id);
      Console.WriteLine("continent.Name: " + continent.Name);
      Console.WriteLine("ModelState.IsValid: " + ModelState.IsValid);
      if (id != continent.Id) return NotFound();

      if (ModelState.IsValid)
      {
        await _continentService.Update(continent);
        return RedirectToAction(nameof(Index));
      }
      return View(continent);
    }

    // GET: Continent/Delete/:id
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null) return NotFound();

      var continent = await _continentService.GetById(id.Value);
      if (continent == null) return NotFound();

      var continentDTO = _mapper.Map<ContinentDTO>(continent);
      return View(continentDTO);
    }

    // POST: Continent/Delete/:id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _continentService.Delete(id);
      return RedirectToAction(nameof(Index));
    }
  }
}

