// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.Mapping.AuditEntityTableColumnMapping
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TSS.Audit.Domain;

#nullable disable
namespace TSS.Audit.Persistence.EFCore.Mapping;

public class AuditEntityTableColumnMapping : IEntityTypeConfiguration<AuditEntityTableColumn>
{
  public void Configure(EntityTypeBuilder<AuditEntityTableColumn> builder)
  {
    builder.ToTable<AuditEntityTableColumn>("AuditEntityTableColumn");
    builder.HasKey((Expression<Func<AuditEntityTableColumn, object>>) (x => (object) x.AuditEntityTableColumnId));
    builder.Property<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.ColumnDotNetType)).HasMaxLength(250).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.ColumnLabel)).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.ColumnName)).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.ColumnTsqltype)).HasColumnName<string>("ColumnTSQLType").HasMaxLength(250).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.MasterTableDescColumnName)).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.MasterTableName)).HasMaxLength(50).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditEntityTableColumn, string>>) (e => e.MasterTablePkname)).HasColumnName<string>("MasterTablePKName").HasMaxLength(50).IsUnicode(false);
    builder.Property<int?>((Expression<Func<AuditEntityTableColumn, int?>>) (e => e.DisplayOrder)).IsRequired(false);
    builder.Property<bool>((Expression<Func<AuditEntityTableColumn, bool>>) (e => e.Enabled)).IsRequired(true);
    builder.HasOne<AuditEntityTable>((Expression<Func<AuditEntityTableColumn, AuditEntityTable>>) (c => c.AuditEntityTable)).WithMany((Expression<Func<AuditEntityTable, IEnumerable<AuditEntityTableColumn>>>) (t => t.AuditEntityTableColumns)).HasForeignKey((Expression<Func<AuditEntityTableColumn, object>>) (c => (object) c.AuditEntityTableId));
  }
}
