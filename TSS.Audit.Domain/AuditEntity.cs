// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Domain.AuditEntity
// Assembly: TSS.Audit.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9D95FB3D-318C-4872-B305-85847E8E57E6
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Domain.dll

using System.Collections.Generic;

#nullable disable
namespace TSS.Audit.Domain;

public class AuditEntity
{
  protected AuditEntity() => this.Enabled = true;

  public AuditEntity(string appCode, string name, string module)
    : this()
  {
    this.ApplicationCode = appCode;
    this.Name = name;
    this.Module = module;
  }

  public int AuditEntityId { get; protected set; }

  public string ApplicationCode { get; protected set; }

  public string Name { get; protected set; }

  public string Module { get; protected set; }

  public bool Enabled { get; set; }

  public virtual ICollection<AuditEntityTable> AuditEntityTables { get; protected set; } = (ICollection<AuditEntityTable>) new List<AuditEntityTable>();

  public virtual ICollection<AuditTransactionEntityLog> AuditTransactionEntityLogs { get; protected set; } = (ICollection<AuditTransactionEntityLog>) new List<AuditTransactionEntityLog>();

  public void RegisterEntityTable(AuditEntityTable auditEntityTable)
  {
    this.AuditEntityTables.Add(auditEntityTable);
  }

  public void RemoveEntityTable(AuditEntityTable auditEntityTable)
  {
    this.AuditEntityTables.Remove(auditEntityTable);
  }
}
