using AutoMapper;
using ContiNet.Models;
using ContiNet.Services;
using ContiNet.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ContiNet.Controllers.Api
{
  [Route("api/[controller]")]
  [ApiController]
  public class CountryController : ControllerBase
  {
    private readonly CountryService _countryService;
    private readonly ContinentService _continentService;
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

    // GET: api/country
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CountryDTO>>> GetAll()
    {
      var countries = await _countryService.GetAll();
      var countryDTOs = _mapper.Map<List<CountryDTO>>(countries);
      return Ok(countryDTOs);
    }

    // GET: api/country/:id
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDTO>> Get(int id)
    {
      var country = await _countryService.GetById(id);
      if (country == null) return NotFound();

      var countryDTO = _mapper.Map<CountryDTO>(country);
      return Ok(countryDTO);
    }

    // POST: api/country
    [HttpPost]
    public async Task<ActionResult<CountryDTO>> Post(Country country)
    {
      if (country == null) return BadRequest("Country is null");
      if (!ModelState.IsValid) return BadRequest(ModelState);

      if (country.ContinentId == null) return BadRequest("ContinentId is required");

      var continent = await _continentService.GetById(country.ContinentId.Value);
      if (continent == null) return BadRequest("Continent not found");

      country.Continent = continent;

      await _countryService.Create(country);
      var countryDTO = _mapper.Map<CountryDTO>(country);
      return CreatedAtAction(nameof(Get), new { id = countryDTO.Id }, countryDTO);
    }

    // PUT: api/country/:id
    [HttpPut("{id}")]
    public async Task<ActionResult<CountryDTO>> Put(int id, Country country)
    {
      var existingCountry = await _countryService.GetById(id);
      if (existingCountry == null) return NotFound();

      existingCountry.Name = country.Name ?? existingCountry.Name;
      existingCountry.Population = country.Population ?? existingCountry.Population;

      if (country.ContinentId != null) {
        var continent = await _continentService.GetById(country.ContinentId.Value);
        if (continent == null) return BadRequest("Continent not found");

        existingCountry.ContinentId = continent.Id;
      }

      await _countryService.Update(existingCountry);
      var countryDTO = _mapper.Map<CountryDTO>(existingCountry);

      return Ok(countryDTO);
    }

    // DELETE: api/country/:id
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var country = await _countryService.GetById(id);
      if (country == null) return NotFound();
      await _countryService.Delete(id);
      return NoContent();
    }
  }
}
