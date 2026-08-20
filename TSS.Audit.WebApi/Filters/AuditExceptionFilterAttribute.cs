// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Filters.AuditExceptionFilterAttribute
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using TSS.Audit.DTOs.Core;
using TSS.Audit.ReadServices;
using TSS.Audit.Resources;
using TSS.Audit.WebApi.Extensions;

#nullable disable
namespace TSS.Audit.WebApi.Filters;

public class AuditExceptionFilterAttribute : ExceptionFilterAttribute
{
  public override void OnException(ExceptionContext context)
  {
    Guid propertyValue = Guid.NewGuid();
    Log.Logger.Error<Guid>(((Exception) context.Exception).GetBaseException(), "Error ID: {errorId}", propertyValue);
    ReadOperationResponse operationResponse = ReadOperationBuilder.WithMessage(string.Format(Messages.UnexpectedErrorMessage, (object) propertyValue), OperationStatus.ServerError);
    context.Result = operationResponse.ToActionResult();
  }
}
