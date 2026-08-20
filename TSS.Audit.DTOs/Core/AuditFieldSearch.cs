// Decompiled with JetBrains decompiler
// Type: TSS.Audit.DTOs.Core.AuditFieldSearch
// Assembly: TSS.Audit.DTOs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8CA03111-1FE1-44F2-8494-9D295448C35A
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.DTOs.dll

#nullable disable
namespace TSS.Audit.DTOs.Core;

public class AuditFieldSearch
{
  public string Entity { get; set; }

  public string EntityModule { get; set; }

  public string Table { get; set; }

  public string FieldName { get; set; }

  public string OldValue { get; set; }

  public string NewValue { get; set; }
}
