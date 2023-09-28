using PowerplantCodingChallenge.API.Dtos;
using System.ComponentModel.DataAnnotations;

namespace PowerplantCodingChallenge.API.DataTransferModels.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class GreaterThan : ValidationAttribute
{
	private readonly string _otherProperty;

	public GreaterThan(string otherProperty)
	{
		this._otherProperty = otherProperty;
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		var powerplant = (PowerPlantDto)validationContext.ObjectInstance;
		var propertyValue = (int)value;

		var propertyInfo
			= powerplant.GetType().GetProperties().FirstOrDefault(p => p.Name == _otherProperty);

		if (propertyInfo != null)
		{
			var otherValue = (int)propertyInfo.GetValue(powerplant);
			if (otherValue >= propertyValue) 
				return new ValidationResult("Value must be greater than MinimumPower");

			return ValidationResult.Success;
		}

		return new ValidationResult($"Property {_otherProperty} does not exists");
	}
}
