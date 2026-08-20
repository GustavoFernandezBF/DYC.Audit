// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.AuditProcessDescription
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.Domain;

public class AuditProcessDescription
{
  protected AuditProcessDescription() => this.Enabled = true;

  public AuditProcessDescription(string appCode, string name, string module)
    : this()
  {
    this.ApplicationCode = appCode;
    this.Module = module;
    this.Name = name;
  }

  public int AuditProcessDescriptionId { get; set; }

  public string ApplicationCode { get; protected set; }

  public string Name { get; protected set; }

  public string Module { get; protected set; }

  public string Description { get; set; }

  public bool Enabled { get; set; }

  public virtual ICollection<AuditProcessLog> AuditProcessLogs { get; protected set; } = (ICollection<AuditProcessLog>) new List<AuditProcessLog>();
}
