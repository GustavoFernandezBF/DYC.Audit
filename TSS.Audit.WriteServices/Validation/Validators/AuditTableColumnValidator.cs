// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditTableColumnValidator
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using FluentValidation;
using System;
using System.Linq.Expressions;
using TSS.Audit.DTOs.Core;
using TSS.Audit.Resources;

#nullable disable
namespace TSS.Audit.WriteServices.Validation.Validators;

public class AuditTableColumnValidator : AbstractValidator<AuditTableColumn>
{
  public AuditTableColumnValidator()
  {
    this.RuleFor<string>((Expression<Func<AuditTableColumn, string>>) (x => x.Name)).NotEmpty<AuditTableColumn, string>().MaximumLength<AuditTableColumn>(50).MinimumLength<AuditTableColumn>(3).Matches<AuditTableColumn>("^([A-Za-z0-9_-]+\\.*)+$").WithLocalizedMessage<AuditTableColumn, string>(typeof (Messages), "ColumnNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditTableColumn, string>>) (x => x.NetType)).MaximumLength<AuditTableColumn>(250);
    this.RuleFor<string>((Expression<Func<AuditTableColumn, string>>) (x => x.SqlType)).MaximumLength<AuditTableColumn>(250);
    this.RuleFor<string>((Expression<Func<AuditTableColumn, string>>) (x => x.Label)).MaximumLength<AuditTableColumn>(50);
    this.RuleFor<string>((Expression<Func<AuditTableColumn, string>>) (x => x.MasterTableName)).MaximumLength<AuditTableColumn>(50);
    this.RuleFor<string>((Expression<Func<AuditTableColumn, string>>) (x => x.MasterTablePKName)).MaximumLength<AuditTableColumn>(50);
    this.RuleFor<string>((Expression<Func<AuditTableColumn, string>>) (x => x.MasterTableDescColumnName)).MaximumLength<AuditTableColumn>(50);
  }
}
