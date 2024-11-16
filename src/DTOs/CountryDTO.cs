namespace ContiNet.DTOs
{
  public class CountryDTO
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ContinentName { get; set; }
    public int Population { get; set; }
  }
}
