namespace PowerplantCodingChallenge.API.Models;

public class GenerationDetails
{
	public int Load { get; set; }

	public Forecast FuelForecast { get; set; } = new();

	public IEnumerable<PowerPlant> Powerplants { get; set; } = Enumerable.Empty<PowerPlant>();
}
