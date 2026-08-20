// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.NPoco.Mapping.AuditEntityMapping
// Assembly: TSS.Audit.Persistence.NPoco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1D411E7-D536-4883-86F6-699D5668BAB4
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.NPoco.dll

using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TSS.Audit.QueryModel;

#nullable disable
namespace TSS.Audit.Persistence.NPoco.Mapping;

public class AuditEntityMapping : Map<AuditEntity>
{
  public AuditEntityMapping()
  {
    this.TableName("AuditEntity");
    this.Columns((Action<ColumnConfigurationBuilder<AuditEntity>>) (x =>
    {
      x.Column<int>((Expression<Func<AuditEntity, int>>) (e => e.AuditEntityId));
      x.Column<string>((Expression<Func<AuditEntity, string>>) (e => e.ApplicationCode));
      x.Column<string>((Expression<Func<AuditEntity, string>>) (e => e.Name));
      x.Column<string>((Expression<Func<AuditEntity, string>>) (e => e.Module));
      x.Column<bool>((Expression<Func<AuditEntity, bool>>) (e => e.Enabled));
      x.Column<List<AuditEntityTable>>((Expression<Func<AuditEntity, List<AuditEntityTable>>>) (e => e.AuditEntityTables)).Ignore();
    }));
  }
}
