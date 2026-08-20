// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Response.TableOperation
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

using System;
using TSS.Audit.Common;

#nullable disable
namespace TSS.Audit.DTOs.Response;

public class TableOperation
{
  public TableOperation(Constants.TableOperation operation) => this.Operation = operation;

  public Constants.TableOperation Operation { get; set; }

  public string Name => Enum.GetName(typeof (Constants.TableOperation), (object) this.Operation);
}
