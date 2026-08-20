// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.WriteOperationResponse
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using Newtonsoft.Json;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Response;

#nullable disable
namespace TSS.Audit.WriteServices;

public class WriteOperationResponse : OperationResponse
{
  public WriteOperationResponse()
  {
  }

  public WriteOperationResponse(OperationResponse operationResponse)
  {
    this.IsCorrect = operationResponse.IsCorrect;
    this.Messages = operationResponse.Messages;
  }

  [JsonIgnore]
  public OperationStatus Status { get; set; }
}
