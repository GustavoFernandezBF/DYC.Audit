// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.EFCore.Mapping.AuditProcessLogMapping
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

public class AuditProcessLogMapping : IEntityTypeConfiguration<AuditProcessLog>
{
  public void Configure(EntityTypeBuilder<AuditProcessLog> builder)
  {
    builder.ToTable<AuditProcessLog>("AuditProcessLog");
    builder.HasKey((Expression<Func<AuditProcessLog, object>>) (x => (object) x.AuditProcessLogId));
    builder.Property<Guid>((Expression<Func<AuditProcessLog, Guid>>) (e => e.TenantId)).IsRequired(true).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditProcessLog, string>>) (e => e.AuditUserDescription)).HasMaxLength(250).IsUnicode(false);
    builder.Property<string>((Expression<Func<AuditProcessLog, string>>) (e => e.AuditUserIdentifier)).HasMaxLength(250).IsUnicode(false);
    builder.Property<DateTime?>((Expression<Func<AuditProcessLog, DateTime?>>) (e => e.BeginProcessTimestamp)).IsRequired(false);
    builder.Property<DateTime>((Expression<Func<AuditProcessLog, DateTime>>) (e => e.EndProcessTimestamp)).IsRequired(true);
    builder.HasOne<AuditProcessDescription>((Expression<Func<AuditProcessLog, AuditProcessDescription>>) (pl => pl.AuditProcessDescription)).WithMany((Expression<Func<AuditProcessDescription, IEnumerable<AuditProcessLog>>>) (pd => pd.AuditProcessLogs)).HasForeignKey((Expression<Func<AuditProcessLog, object>>) (pl => (object) pl.AuditProcessDescriptionId));
  }
}
