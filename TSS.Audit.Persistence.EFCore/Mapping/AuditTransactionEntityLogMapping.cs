// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.Mapping.AuditTransactionEntityLogMapping
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

public class AuditTransactionEntityLogMapping : IEntityTypeConfiguration<AuditTransactionEntityLog>
{
  public void Configure(
    EntityTypeBuilder<AuditTransactionEntityLog> builder)
  {
    builder.HasKey((Expression<Func<AuditTransactionEntityLog, object>>) (e => (object) e.AuditTransactionEntityId));
    builder.ToTable<AuditTransactionEntityLog>("AuditTransactionEntityLog");
    builder.Property<string>((Expression<Func<AuditTransactionEntityLog, string>>) (e => e.AuditByFieldValue)).IsRequired(true).HasMaxLength(64 /*0x40*/).IsUnicode(false);
    builder.Property<DateTime>((Expression<Func<AuditTransactionEntityLog, DateTime>>) (e => e.AuditDateFieldValue)).IsRequired(true);
    builder.Property<bool>((Expression<Func<AuditTransactionEntityLog, bool>>) (e => e.IsMain)).IsRequired(true);
    builder.HasOne<AuditEntity>((Expression<Func<AuditTransactionEntityLog, AuditEntity>>) (el => el.AuditEntity)).WithMany((Expression<Func<AuditEntity, IEnumerable<AuditTransactionEntityLog>>>) (e => e.AuditTransactionEntityLogs)).HasForeignKey((Expression<Func<AuditTransactionEntityLog, object>>) (el => (object) el.AuditEntityId));
    builder.HasOne<AuditProcessLog>((Expression<Func<AuditTransactionEntityLog, AuditProcessLog>>) (el => el.AuditProcessLog)).WithMany((Expression<Func<AuditProcessLog, IEnumerable<AuditTransactionEntityLog>>>) (pl => pl.AuditTransactionEntityLogs)).HasForeignKey((Expression<Func<AuditTransactionEntityLog, object>>) (el => (object) el.AuditProcessLogId));
  }
}
