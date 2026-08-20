// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.Mapping.AuditTransactionEntityTableLogMapping
// Assembly: TSS.Audit.Persistence.EFCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F067C905-29AB-47FD-BDFA-B984FDF57185
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.EFCore.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TSS.Audit.Common;
using TSS.Audit.Domain;

#nullable disable
namespace TSS.Audit.Persistence.EFCore.Mapping;

public class AuditTransactionEntityTableLogMapping : 
  IEntityTypeConfiguration<AuditTransactionEntityTableLog>
{
  public void Configure(
    EntityTypeBuilder<AuditTransactionEntityTableLog> builder)
  {
    builder.HasKey((Expression<Func<AuditTransactionEntityTableLog, object>>) (e => (object) e.AuditTransactionEntityTableId));
    builder.ToTable<AuditTransactionEntityTableLog>("AuditTransactionEntityTableLog");
    builder.Property<string>((Expression<Func<AuditTransactionEntityTableLog, string>>) (e => e.IdColumnValue)).HasMaxLength(250).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditTransactionEntityTableLog, string>>) (e => e.KeyFieldValue)).HasMaxLength(250).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditTransactionEntityTableLog, string>>) (e => e.RowVersion)).HasMaxLength(250).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditTransactionEntityTableLog, string>>) (e => e.UpdateMask)).IsUnicode(false);
    builder.Property<DateTime>((Expression<Func<AuditTransactionEntityTableLog, DateTime>>) (e => e.Timestamp)).IsRequired(true);
    builder.Property<Constants.TableOperation>((Expression<Func<AuditTransactionEntityTableLog, Constants.TableOperation>>) (e => e.Operation)).IsRequired(true);
    builder.Property<bool>((Expression<Func<AuditTransactionEntityTableLog, bool>>) (e => e.IsMain)).IsRequired(true);
    builder.HasOne<AuditEntityTable>((Expression<Func<AuditTransactionEntityTableLog, AuditEntityTable>>) (tl => tl.AuditEntityTable)).WithMany((Expression<Func<AuditEntityTable, IEnumerable<AuditTransactionEntityTableLog>>>) (t => t.AuditTransactionEntityTableLogs)).HasForeignKey((Expression<Func<AuditTransactionEntityTableLog, object>>) (tl => (object) tl.AuditEntityTableId));
    builder.HasOne<AuditTransactionEntityLog>((Expression<Func<AuditTransactionEntityTableLog, AuditTransactionEntityLog>>) (tl => tl.AuditTransactionEntity)).WithMany((Expression<Func<AuditTransactionEntityLog, IEnumerable<AuditTransactionEntityTableLog>>>) (el => el.AuditTransactionEntityTableLogs)).HasForeignKey((Expression<Func<AuditTransactionEntityTableLog, object>>) (tl => (object) tl.AuditTransactionEntityId));
  }
}
