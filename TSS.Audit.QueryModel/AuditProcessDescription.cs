// Decompiled with JetBrains decompiler
// Type: TSS.Audit.QueryModel.AuditProcessDescription
// Assembly: TSS.Audit.QueryModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DF217321-56CA-450D-84E4-3813C3160EAD
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.QueryModel.dll

#nullable disable
namespace TSS.Audit.QueryModel;

public class AuditProcessDescription
{
  public int AuditProcessDescriptionId { get; set; }

  public string Name { get; set; }

  public string ApplicationCode { get; set; }

  public string Description { get; set; }

  public string Module { get; set; }

  public bool Enabled { get; set; }
}
