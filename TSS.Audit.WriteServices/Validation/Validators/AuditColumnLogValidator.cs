// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditColumnLogValidator
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

public class AuditColumnLogValidator : AbstractValidator<AuditColumnLog>
{
  public AuditColumnLogValidator()
  {
    this.RuleFor<string>((Expression<Func<AuditColumnLog, string>>) (x => x.ColumnName)).NotEmpty<AuditColumnLog, string>().WithLocalizedMessage<AuditColumnLog, string>(typeof (Messages), "ColumnNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditColumnLog, string>>) (x => x.Previous)).MaximumLength<AuditColumnLog>(250);
    this.RuleFor<string>((Expression<Func<AuditColumnLog, string>>) (x => x.Current)).MaximumLength<AuditColumnLog>(250);
  }
}
