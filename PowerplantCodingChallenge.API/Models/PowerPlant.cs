namespace PowerplantCodingChallenge.API.Models;

public class PowerPlant
{
	public string Name { get; set; } = null!;

	public PowerPlantType Type { get; set; }

	public double Efficiency { get; set; }

	public int MinimumPower { get; set; }

	public int MaximumPower { get; set; }

	public double CostPerMegaWattHour { get; set; }

	public double PMaxWhenOn { get; set; }

	public double PMaxWhenOff { get; set; }

	public bool IsOn { get; set; }

	public double CurrentPower { get; set; }
}
