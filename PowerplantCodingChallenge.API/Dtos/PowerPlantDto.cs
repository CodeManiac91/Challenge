using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PowerplantCodingChallenge.API.DataTransferModels.Validators;

namespace PowerplantCodingChallenge.API.Dtos;

public class PowerPlantDto
{
	[Required] 
	public string? Name { get; set; }

	[Required]
	[JsonConverter(typeof(StringEnumConverter))]
	public PowerPlantType Type { get; set; }

	[Required] 
	[Range(0.0, 1.0)] 
	public double Efficiency { get; set; }

	[Required] 
	[JsonProperty("pmin")] 
	public int MinimumPower { get; set; }

	[Required]
	[GreaterThan(nameof(MinimumPower))]
	[JsonProperty("pmax")]
	public int MaximumPower { get; set; }
}
