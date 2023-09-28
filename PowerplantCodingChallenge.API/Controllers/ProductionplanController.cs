using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PowerplantCodingChallenge.API.Dtos;
using PowerplantCodingChallenge.API.Models;
using PowerplantCodingChallenge.API.Services;

namespace PowerplantCodingChallenge.API.Controllers;

[Route("api/productionplan")]
[ApiController]
public class ProductionplanController : ControllerBase
{
	private readonly ILogger<ProductionplanController> _logger;
	private readonly IMapper _mapper;
	private readonly IPowerplanCalculationService _powerplanCalculationService;

	public ProductionplanController(ILogger<ProductionplanController> logger
		, IPowerplanCalculationService powerplanCalculationService
		, IMapper mapper)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_powerplanCalculationService = powerplanCalculationService ?? throw new ArgumentNullException(nameof(powerplanCalculationService));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
	}

	/// <summary>
	/// Calculates the power plan.
	/// </summary>
	/// <param name="request">The request to calculate.</param>
	/// <returns>A power plan</returns>
	[HttpPost]
	public ActionResult<GenerationPlanDto> CalculatePowerplan(GenerationDetailsDto request)
	{
		var generationPlan = _mapper.Map<GenerationDetails>(request);

		try
		{
			var powerplan = _powerplanCalculationService.CalculatePowerplan(generationPlan);

			var returnValue = _mapper.Map<IEnumerable<GenerationPlanDto>>(powerplan);

			return Ok(JsonConvert.SerializeObject(returnValue));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return BadRequest(ex.Message);

		}
	}
}
