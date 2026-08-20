// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.NPoco.Mapping.AuditEntityTableColumnMapping
// Assembly: TSS.Audit.Persistence.NPoco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1D411E7-D536-4883-86F6-699D5668BAB4
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.NPoco.dll

using NPoco.FluentMappings;
using System;
using System.Linq.Expressions;
using TSS.Audit.QueryModel;

#nullable disable
namespace TSS.Audit.Persistence.NPoco.Mapping;

public class AuditEntityTableColumnMapping : Map<AuditEntityTableColumn>
{
  public AuditEntityTableColumnMapping()
  {
    this.TableName("AuditEntityTableColumn");
    this.Columns((Action<ColumnConfigurationBuilder<AuditEntityTableColumn>>) (x =>
    {
      x.Column<int>((Expression<Func<AuditEntityTableColumn, int>>) (e => e.AuditEntityTableId));
      x.Column<int>((Expression<Func<AuditEntityTableColumn, int>>) (e => e.AuditEntityTableColumnId));
      x.Column<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.ColumnName));
      x.Column<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.ColumnDotNetType));
      x.Column<int?>((Expression<Func<AuditEntityTableColumn, int?>>) (e => e.DisplayOrder));
      x.Column<bool>((Expression<Func<AuditEntityTableColumn, bool>>) (e => e.Enabled));
      x.Column<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.ColumnLabel));
      x.Column<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.MasterTableName));
      x.Column<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.MasterTablePkname));
      x.Column<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.MasterTableDescColumnName));
    }));
  }
}
