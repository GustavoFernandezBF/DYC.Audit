// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Response.OperationResponse
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.DTOs.Response;

public class OperationResponse
{
  public bool IsCorrect { get; set; }

  public List<string> Messages { get; set; } = new List<string>();
}
