// Decompiled with JetBrains decompiler
// Type: TSS.Audit.Persistence.NPoco.Mapping.AuditProcessDescriptionMapping
// Assembly: TSS.Audit.Persistence.NPoco, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F1D411E7-D536-4883-86F6-699D5668BAB4
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.Persistence.NPoco.dll

using NPoco.FluentMappings;
using System;
using System.Linq.Expressions;
using TSS.Audit.QueryModel;

#nullable disable
namespace TSS.Audit.Persistence.NPoco.Mapping;

public class AuditProcessDescriptionMapping : Map<AuditProcessDescription>
{
  public AuditProcessDescriptionMapping()
  {
    this.TableName("AuditProcessDescription");
    this.Columns((Action<ColumnConfigurationBuilder<AuditProcessDescription>>) (x =>
    {
      x.Column<int>((Expression<Func<AuditProcessDescription, int>>) (y => y.AuditProcessDescriptionId));
      x.Column<string>((Expression<Func<AuditProcessDescription, string>>) (y => y.ApplicationCode));
      x.Column<string>((Expression<Func<AuditProcessDescription, string>>) (y => y.Name));
      x.Column<string>((Expression<Func<AuditProcessDescription, string>>) (y => y.Description));
      x.Column<string>((Expression<Func<AuditProcessDescription, string>>) (y => y.Module));
      x.Column<bool>((Expression<Func<AuditProcessDescription, bool>>) (y => y.Enabled));
    }));
  }
}
