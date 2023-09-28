namespace PowerplantCodingChallenge.API.Models;

public class Forecast
{
	public decimal GasPricePerMwh { get; set; }

	public decimal KerosinePricePerMwh { get; set; }

	public decimal Co2PricePerTon { get; set; }

	public float WindPercentage { get; set; }
}
