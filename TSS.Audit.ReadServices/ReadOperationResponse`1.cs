// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ReadServices.ReadOperationResponse`1
// Assembly: TSS.Audit.ReadServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A10258C3-6446-4BF1-813B-45DC267811FE
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ReadServices.dll

using TSS.Audit.DTOs.Response;

#nullable disable
namespace TSS.Audit.ReadServices;

public class ReadOperationResponse<T> : ReadOperationResponse
{
  public T Data { get; set; }

  public ReadOperationResponse()
  {
  }

  public ReadOperationResponse(OperationResponse operationResponse)
    : base(operationResponse)
  {
  }
}
