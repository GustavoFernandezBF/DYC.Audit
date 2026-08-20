// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.FluentValidationService
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using System.Collections.Generic;
using TSS.Audit.DTOs.Response;

#nullable disable
namespace TSS.Audit.WriteServices.Validation;

public class FluentValidationService : IValidationService
{
  private readonly IValidatorProvider _validatorProvider;

  public FluentValidationService(IValidatorProvider validatorProvider)
  {
    this._validatorProvider = validatorProvider;
  }

  public OperationResponse Validate<T>(T entity, string ruleset = "")
  {
    AbstractValidator<T> validatorFor = this._validatorProvider.GetValidatorFor<T>();
    ValidationResult validationResult = !string.IsNullOrWhiteSpace(ruleset) ? validatorFor.Validate<T>(entity, (IValidatorSelector) null, ruleset) : validatorFor.Validate(entity);
    OperationResponse operationResponse = new OperationResponse()
    {
      IsCorrect = validationResult.IsValid
    };
    foreach (ValidationFailure error in (IEnumerable<ValidationFailure>) validationResult.Errors)
      operationResponse.Messages.Add(error.ErrorMessage);
    return operationResponse;
  }
}
