// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.WriteOperationBuilder
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using System.Collections.Generic;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Response;

#nullable disable
namespace TSS.Audit.WriteServices;

public static class WriteOperationBuilder
{
  public static WriteOperationResponse WithMessage(string message, OperationStatus status)
  {
    WriteOperationResponse operationResponse = new WriteOperationResponse();
    operationResponse.Messages = new List<string>()
    {
      message
    };
    operationResponse.Status = status;
    return operationResponse;
  }

  public static WriteOperationResponse Correct()
  {
    WriteOperationResponse operationResponse = new WriteOperationResponse();
    operationResponse.IsCorrect = true;
    return operationResponse;
  }

  public static WriteOperationResponse WithStatus(
    this OperationResponse operationResponse,
    OperationStatus status)
  {
    return new WriteOperationResponse(operationResponse)
    {
      Status = status
    };
  }

  public static WriteOperationResponse CreateWriteOperationResponse(
    string message = null,
    OperationStatus status = OperationStatus.Ok)
  {
    return string.IsNullOrWhiteSpace(message) ? WriteOperationBuilder.Correct() : WriteOperationBuilder.WithMessage(message.Trim(), status);
  }
}
