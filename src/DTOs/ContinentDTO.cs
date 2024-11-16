namespace ContiNet.DTOs
{
  public class ContinentDTO
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<ContinentCountryDTO> Countries { get; set; } = new List<ContinentCountryDTO>();
    public long Population { get; set; }
  }
}
