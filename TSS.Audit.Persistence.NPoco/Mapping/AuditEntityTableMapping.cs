// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.NPoco.Mapping.AuditEntityTableMapping
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

public class AuditEntityTableMapping : Map<AuditEntityTable>
{
  public AuditEntityTableMapping()
  {
    this.TableName("AuditEntityTable");
    this.Columns((Action<ColumnConfigurationBuilder<AuditEntityTable>>) (x =>
    {
      x.Column<int>((Expression<Func<AuditEntityTable, int>>) (e => e.AuditEntityTableId));
      x.Column<string>((Expression<Func<AuditEntityTable, string>>) (e => e.TableName));
      x.Column<int?>((Expression<Func<AuditEntityTable, int?>>) (e => e.AuditEntityId));
      x.Column<string>((Expression<Func<AuditEntityTable, string>>) (e => e.KeyFieldName));
      x.Column<string>((Expression<Func<AuditEntityTable, string>>) (e => e.IdColumnName));
      x.Column<string>((Expression<Func<AuditEntityTable, string>>) (e => e.AuditByFieldName));
      x.Column<string>((Expression<Func<AuditEntityTable, string>>) (e => e.AuditDateFieldName));
      x.Column<bool>((Expression<Func<AuditEntityTable, bool>>) (e => e.Enabled));
      x.Column<string>((Expression<Func<AuditEntityTable, string>>) (e => e.TableDescriptionFormat));
      x.Column<List<AuditEntityTableColumn>>((Expression<Func<AuditEntityTable, List<AuditEntityTableColumn>>>) (e => e.AuditEntityTableColumns)).Ignore();
    }));
  }
}
