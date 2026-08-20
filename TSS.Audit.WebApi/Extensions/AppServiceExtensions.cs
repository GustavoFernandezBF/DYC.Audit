// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Extensions.AppServiceExtensions
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Microsoft.AspNetCore.Mvc;
using System;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Response;
using TSS.Audit.ReadServices;
using TSS.Audit.WriteServices;

#nullable disable
namespace TSS.Audit.WebApi.Extensions;

public static class AppServiceExtensions
{
  public static IActionResult ToActionResult(this WriteOperationResponse operationResponse)
  {
    OperationResponse error = operationResponse != null ? (OperationResponse) operationResponse : throw new ArgumentNullException(nameof (operationResponse));
    switch (operationResponse.Status)
    {
      case OperationStatus.Ok:
        return (IActionResult) new OkResult();
      case OperationStatus.NotFound:
        return (IActionResult) new NotFoundObjectResult((object) error);
      case OperationStatus.Conflict:
        return (IActionResult) new ObjectResult((object) error)
        {
          StatusCode = (int?) new int?(409)
        };
      case OperationStatus.ServerError:
        return (IActionResult) new ObjectResult((object) error)
        {
          StatusCode = (int?) new int?(500)
        };
      case OperationStatus.BadGateway:
        return (IActionResult) new ObjectResult((object) error)
        {
          StatusCode = (int?) new int?(502)
        };
      default:
        return (IActionResult) new BadRequestObjectResult((object) error);
    }
  }

  public static IActionResult ToActionResult(this ReadOperationResponse operationResponse)
  {
    OperationResponse error = operationResponse != null ? (OperationResponse) operationResponse : throw new ArgumentNullException(nameof (operationResponse));
    switch (operationResponse.Status)
    {
      case OperationStatus.Ok:
        return (IActionResult) new OkResult();
      case OperationStatus.NotFound:
        return (IActionResult) new NotFoundObjectResult((object) error);
      case OperationStatus.Conflict:
        return (IActionResult) new ObjectResult((object) error)
        {
          StatusCode = (int?) new int?(409)
        };
      case OperationStatus.ServerError:
        return (IActionResult) new ObjectResult((object) error)
        {
          StatusCode = (int?) new int?(500)
        };
      case OperationStatus.BadGateway:
        return (IActionResult) new ObjectResult((object) error)
        {
          StatusCode = (int?) new int?(502)
        };
      default:
        return (IActionResult) new BadRequestObjectResult((object) error);
    }
  }
}
