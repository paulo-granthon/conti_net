using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace ContiNet.Models
{
  public class Continent
  {
    [SwaggerIgnore]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public required string Name { get; set; } = default!;

    [SwaggerIgnore]
    [JsonIgnore]
    public ICollection<Country> Countries { get; set; } = new List<Country>();
  }
}
