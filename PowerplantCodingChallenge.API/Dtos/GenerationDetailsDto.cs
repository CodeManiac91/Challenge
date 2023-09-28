using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PowerplantCodingChallenge.API.Dtos;

public class GenerationDetailsDto
{
	[Required] 
	public int Load { get; set; }

	[JsonProperty("fuels")] 
	public ForecastDto FuelForecast { get; set; } = new();

	public IEnumerable<PowerPlantDto> Powerplants { get; set; } = Enumerable.Empty<PowerPlantDto>();
}
