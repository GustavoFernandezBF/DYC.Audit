// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditTableLogValidator
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TSS.Audit.DTOs.Core;
using TSS.Audit.Resources;

#nullable disable
namespace TSS.Audit.WriteServices.Validation.Validators;

public class AuditTableLogValidator : AbstractValidator<AuditTableLog>
{
  public AuditTableLogValidator()
  {
    this.RuleFor<string>((Expression<Func<AuditTableLog, string>>) (x => x.TableName)).NotEmpty<AuditTableLog, string>().WithLocalizedMessage<AuditTableLog, string>(typeof (Messages), "TableNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditTableLog, string>>) (x => x.UpdateMask)).NotEmpty<AuditTableLog, string>();
    this.RuleFor<string>((Expression<Func<AuditTableLog, string>>) (x => x.IdColumnValue)).MaximumLength<AuditTableLog>(250);
    this.RuleFor<string>((Expression<Func<AuditTableLog, string>>) (x => x.KeyColumnValue)).MaximumLength<AuditTableLog>(250);
    this.RuleFor<DateTime>((Expression<Func<AuditTableLog, DateTime>>) (x => x.Timestamp)).NotNull<AuditTableLog, DateTime>().NotEqual<AuditTableLog, DateTime>(DateTime.MinValue).NotEqual<AuditTableLog, DateTime>(DateTime.MaxValue);
    this.RuleFor<string>((Expression<Func<AuditTableLog, string>>) (x => x.RowVersion)).MaximumLength<AuditTableLog>(250);
    this.RuleFor<List<AuditColumnLog>>((Expression<Func<AuditTableLog, List<AuditColumnLog>>>) (x => x.Columns)).NotEmpty<AuditTableLog, List<AuditColumnLog>>();
    ((IRuleBuilder<AuditTableLog, IEnumerable<AuditColumnLog>>) this.RuleFor<List<AuditColumnLog>>((Expression<Func<AuditTableLog, List<AuditColumnLog>>>) (x => x.Columns))).SetCollectionValidator<AuditTableLog, AuditColumnLog>((IValidator<AuditColumnLog>) new AuditColumnLogValidator());
  }
}
