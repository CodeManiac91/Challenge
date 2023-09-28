using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PowerplantCodingChallenge.API.Dtos;

public class ForecastDto
{
	[Required]
	[JsonProperty("gas(euro/MWh)")]
	public decimal GasPricePerMwh { get; set; }

	[Required]
	[JsonProperty("kerosine(euro/MWh)")]
	public decimal KerosinePricePerMwh { get; set; }

	[Required]
	[JsonProperty("co2(euro/ton)")]
	public decimal Co2PricePerTon { get; set; }

	[Required] [JsonProperty("wind(%)")] public double WindPercentage { get; set; }
}
