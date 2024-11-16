using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace ContiNet.Models
{
  public class Country
  {
    [SwaggerIgnore]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public required string Name { get; set; } = null!;

    [Required(ErrorMessage = "Continent is required.")]
    public required int? ContinentId { get; set; } = null!;

    [ForeignKey(nameof(ContinentId))]
    [SwaggerIgnore]
    public Continent? Continent { get; set; } = null!;

    [Required(ErrorMessage = "Population is required.")]
    public required int? Population { get; set; } = null!;
  }
}
