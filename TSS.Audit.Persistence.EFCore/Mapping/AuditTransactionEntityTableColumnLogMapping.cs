// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.Mapping.AuditTransactionEntityTableColumnLogMapping
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

public class AuditTransactionEntityTableColumnLogMapping : 
  IEntityTypeConfiguration<AuditTransactionEntityTableColumnLog>
{
  public void Configure(
    EntityTypeBuilder<AuditTransactionEntityTableColumnLog> builder)
  {
    builder.HasKey((Expression<Func<AuditTransactionEntityTableColumnLog, object>>) (e => (object) e.AuditLogId));
    builder.ToTable<AuditTransactionEntityTableColumnLog>("AuditTransactionEntityTableColumnLog");
    builder.Property<string>((Expression<Func<AuditTransactionEntityTableColumnLog, string>>) (e => e.CurrentValue)).HasMaxLength(250);
    builder.Property<string>((Expression<Func<AuditTransactionEntityTableColumnLog, string>>) (e => e.PreviousValue)).HasMaxLength(250);
    builder.HasOne<AuditEntityTableColumn>((Expression<Func<AuditTransactionEntityTableColumnLog, AuditEntityTableColumn>>) (cl => cl.AuditEntityTableColumn)).WithMany((Expression<Func<AuditEntityTableColumn, IEnumerable<AuditTransactionEntityTableColumnLog>>>) (c => c.AuditTransactionEntityTableColumnLogs)).HasForeignKey((Expression<Func<AuditTransactionEntityTableColumnLog, object>>) (cl => (object) cl.AuditEntityTableColumnId));
    builder.HasOne<AuditTransactionEntityTableLog>((Expression<Func<AuditTransactionEntityTableColumnLog, AuditTransactionEntityTableLog>>) (cl => cl.AuditTransactionEntityTable)).WithMany((Expression<Func<AuditTransactionEntityTableLog, IEnumerable<AuditTransactionEntityTableColumnLog>>>) (tl => tl.AuditTransactionEntityTableColumnLogs)).HasForeignKey((Expression<Func<AuditTransactionEntityTableColumnLog, object>>) (cl => (object) cl.AuditTransactionEntityTableId));
  }
}
