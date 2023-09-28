using Microsoft.OpenApi.Services;
using Microsoft.Win32.SafeHandles;
using PowerplantCodingChallenge.API.Models;

namespace PowerplantCodingChallenge.API.Services
{
	public class PowerplanCalculationService : IPowerplanCalculationService
	{

		public IEnumerable<GenerationPlan> CalculatePowerplan(GenerationDetails generationDetails)
		{
			var fuelsForecast = generationDetails.FuelForecast;
			var powerplants = generationDetails.Powerplants.ToList();

			PreCalculatePowerPlantCost(powerplants, fuelsForecast);

			var powerplans = GetPlans(generationDetails.Load, powerplants);

			return powerplans;
		}

		private IEnumerable<GenerationPlan> GetPlans(int load, IEnumerable<PowerPlant> powerplants)
		{
			double GetCurrentPower(IEnumerable<PowerPlant> pps) =>
				pps.Where(p => p.IsOn).Sum(p => p.CurrentPower);

			var plantOrderedByCost = powerplants.OrderBy(p => p.CostPerMegaWattHour).ToList();

			for (var i = 0; i < plantOrderedByCost.Count; i++)
			{
				var powerPlant = plantOrderedByCost[i];
				var actualLoad = GetCurrentPower(plantOrderedByCost);
				if (actualLoad < load)
				{
					if (actualLoad + powerPlant.PMaxWhenOff > load)
					{
						if (actualLoad + powerPlant.PMaxWhenOn > load)
						{
							if (i > 0)
							{
								var prevPowerplant = plantOrderedByCost[i - 1];
								var overload = actualLoad + powerPlant.PMaxWhenOn - load;
								prevPowerplant.CurrentPower -= overload;
								powerPlant.IsOn = true;
								powerPlant.CurrentPower = powerPlant.PMaxWhenOn;
							}
							else
							{
								throw new Exception("The load is too low");
							}
						}
						else
						{
							powerPlant.IsOn = true;
							powerPlant.CurrentPower = load - actualLoad;
						}
					}
					else
					{
						if (powerPlant.PMaxWhenOff > 0.01) 
						{
							powerPlant.IsOn = true;
							powerPlant.CurrentPower = powerPlant.PMaxWhenOff;
						}
					}
				}
			}

			if (GetCurrentPower(powerplants) < load)
			{
				throw new Exception("The load is higher than capacity available");
			}

			return plantOrderedByCost
				.Select(p => new GenerationPlan
				{
					Name = p.Name,
					Power = p.IsOn ? p.CurrentPower : 0
				});
		}

		private void PreCalculatePowerPlantCost(IEnumerable<PowerPlant> powerplants, Forecast fuelsForecast)
		{
			foreach (var powerplant in powerplants)
			{
				powerplant.CostPerMegaWattHour = GetCostPerMegaWattHour(fuelsForecast, powerplant);
				var (minimumPower, maximumPower) = GetPowerWhenOn(fuelsForecast, powerplant);
				powerplant.PMaxWhenOn = minimumPower;
				powerplant.PMaxWhenOff = maximumPower;
			}
		}

		private double GetCostPerMegaWattHour(Forecast fuels, PowerPlant powerPlant)
		{
			return powerPlant.Type switch
			{
				PowerPlantType.GasFired => (double)fuels.GasPricePerMwh / powerPlant.Efficiency +
										   0.3 * (double)fuels.Co2PricePerTon,
				PowerPlantType.TurboJet => (double)fuels.KerosinePricePerMwh / powerPlant.Efficiency,
				PowerPlantType.WindTurbine => 0.0,
				_ => throw new Exception("Unknow power plant price")
			};
		}

		private (double minimum, double maximum) GetPowerWhenOn(Forecast fuels, PowerPlant powerplant)
		{
			return powerplant.Type switch
			{
				PowerPlantType.GasFired => (powerplant.MinimumPower, powerplant.MaximumPower),
				PowerPlantType.TurboJet => (powerplant.MinimumPower, powerplant.MaximumPower),
				PowerPlantType.WindTurbine => (powerplant.MaximumPower * (double)fuels.WindPercentage / 100.0,
					powerplant.MaximumPower * (double)fuels.WindPercentage / 100.0),
                _ => throw new Exception("Unknow power power price")
            };
		}
	}
}
