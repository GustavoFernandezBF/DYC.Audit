// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.NPoco.Mapping.AuditLogDataMapping
// Assembly: TSS.Audit.Persistence.NPoco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1D411E7-D536-4883-86F6-699D5668BAB4
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.NPoco.dll

using NPoco.FluentMappings;
using TSS.Audit.QueryModel;

#nullable disable
namespace TSS.Audit.Persistence.NPoco.Mapping;

public class AuditLogDataMapping : Map<AuditLogData>
{
  public AuditLogDataMapping() => this.TableName("AuditLogDataView");
}
