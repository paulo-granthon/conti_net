using AutoMapper;
using ContiNet.Models;
using ContiNet.Services;
using ContiNet.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ContiNet.Controllers.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class ContinentController : ControllerBase
  {
    private readonly ContinentService _continentService;
    private readonly IMapper _mapper;

    public ContinentController(
      ContinentService continentService,
      IMapper mapper
    )
    {
      _continentService = continentService;
      _mapper = mapper;
    }

    // GET: api/continent
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var continents = await _continentService.GetAll();
      var continentDTOs = _mapper.Map<List<ContinentDTO>>(continents);
      return Ok(continentDTOs);
    }

    // GET: api/continent/:id
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<ContinentDTO>>> Get(int id)
    {
      var continent = await _continentService.GetById(id);
      if (continent == null) return NotFound();

      var continentDTO = _mapper.Map<ContinentDTO>(continent);
      return Ok(continentDTO);
    }

    // POST: api/continent
    [HttpPost]
    public async Task<ActionResult<ContinentDTO>> Create([FromBody] Continent continent)
    {
      if (continent == null) return BadRequest("Continent is null");
      if (!ModelState.IsValid) return BadRequest(ModelState);
      await _continentService.Create(continent);

      var continentDTO = _mapper.Map<ContinentDTO>(continent);
      return CreatedAtAction(nameof(Get), new { id = continentDTO.Id }, continentDTO);
    }

    // PUT: api/continent/:id
    [HttpPut("{id}")]
    public async Task<ActionResult<ContinentDTO>> Update(int id, [FromBody] Continent continent)
    {
      var existingContinent = await _continentService.GetById(id);
      if (existingContinent == null) return NotFound();

      existingContinent.Name = continent.Name ?? existingContinent.Name;

      await _continentService.Update(existingContinent);
      var continentDTO = _mapper.Map<ContinentDTO>(existingContinent);

      return Ok(continentDTO);
    }

    // DELETE: api/continent/:id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var continent = await _continentService.GetById(id);
      if (continent == null) return NotFound();
      await _continentService.Delete(id);
      return NoContent();
    }
  }
}
