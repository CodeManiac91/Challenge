using Newtonsoft.Json;

namespace PowerplantCodingChallenge.API.Dtos;

public class GenerationPlanDto
{
	[JsonProperty("name")] 
	public string? Name { get; set; }

	[JsonProperty("p")] 
	public double Power { get; set; }
}
