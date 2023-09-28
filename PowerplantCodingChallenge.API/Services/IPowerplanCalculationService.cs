using PowerplantCodingChallenge.API.Models;

namespace PowerplantCodingChallenge.API.Services
{
	public interface IPowerplanCalculationService
	{
		IEnumerable<GenerationPlan> CalculatePowerplan(GenerationDetails payload);
	}
}
