// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ReadServices.ReadOperationBuilder
// Assembly: TSS.Audit.ReadServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A10258C3-6446-4BF1-813B-45DC267811FE
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ReadServices.dll

using System.Collections.Generic;
using System.Linq;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Response;

#nullable disable
namespace TSS.Audit.ReadServices;

public static class ReadOperationBuilder
{
  public static ReadOperationResponse WithMessage(string message, OperationStatus status)
  {
    ReadOperationResponse operationResponse = new ReadOperationResponse();
    operationResponse.Messages = new List<string>()
    {
      message
    };
    operationResponse.Status = status;
    return operationResponse;
  }

  public static ReadOperationResponse WithMessages(List<string> messages, OperationStatus status)
  {
    ReadOperationResponse operationResponse = new ReadOperationResponse();
    operationResponse.Messages = messages;
    operationResponse.Status = status;
    return operationResponse;
  }

  public static ReadOperationResponse<T> WithMessage<T>(
    string message,
    OperationStatus status,
    T data = default)
  {
    ReadOperationResponse<T> operationResponse = new ReadOperationResponse<T>();
    operationResponse.Messages = new List<string>()
    {
      message
    };
    operationResponse.Status = status;
    operationResponse.Data = data;
    return operationResponse;
  }

  public static ReadOperationResponse<T> WithMessages<T>(
    List<string> messages,
    OperationStatus status,
    T data = default)
  {
    ReadOperationResponse<T> operationResponse = new ReadOperationResponse<T>();
    operationResponse.Messages = messages;
    operationResponse.Status = status;
    operationResponse.Data = data;
    return operationResponse;
  }

  public static ReadOperationResponse Correct()
  {
    ReadOperationResponse operationResponse = new ReadOperationResponse();
    operationResponse.IsCorrect = true;
    return operationResponse;
  }

  public static ReadOperationResponse<T> Correct<T>(T data = default)
  {
    ReadOperationResponse<T> operationResponse = new ReadOperationResponse<T>();
    operationResponse.IsCorrect = true;
    operationResponse.Data = data;
    return operationResponse;
  }

  public static ReadOperationResponse WithStatus(
    this OperationResponse operationResponse,
    OperationStatus status)
  {
    return new ReadOperationResponse(operationResponse)
    {
      Status = status
    };
  }

  public static ReadOperationResponse<T> WithStatus<T>(
    this OperationResponse operationResponse,
    OperationStatus status,
    T data = default)
  {
    ReadOperationResponse<T> operationResponse1 = new ReadOperationResponse<T>(operationResponse);
    operationResponse1.Status = status;
    operationResponse1.Data = data;
    return operationResponse1;
  }

  public static ReadOperationResponse CreateReadOperationResponse(
    string message = null,
    OperationStatus status = OperationStatus.Ok)
  {
    return string.IsNullOrWhiteSpace(message) ? ReadOperationBuilder.Correct() : ReadOperationBuilder.WithMessage(message?.Trim(), status);
  }

  public static ReadOperationResponse CreateReadOperationResponse(
    List<string> messages,
    OperationStatus status = OperationStatus.Ok)
  {
    return messages == null && !messages.Any<string>() ? ReadOperationBuilder.Correct() : ReadOperationBuilder.WithMessages(messages, status);
  }

  public static ReadOperationResponse<T> CreateReadOperationResponse<T>(
    string message = null,
    OperationStatus status = OperationStatus.Ok,
    T data = default)
  {
    return string.IsNullOrWhiteSpace(message) ? ReadOperationBuilder.Correct<T>(data) : ReadOperationBuilder.WithMessage<T>(message?.Trim(), status, data);
  }

  public static ReadOperationResponse<T> CreateReadOperationResponse<T>(
    List<string> messages,
    OperationStatus status = OperationStatus.Ok,
    T data = default)
  {
    return messages == null && !messages.Any<string>() ? ReadOperationBuilder.Correct<T>(data) : ReadOperationBuilder.WithMessages<T>(messages, status, data);
  }
}
