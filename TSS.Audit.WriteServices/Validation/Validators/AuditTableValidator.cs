// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditTableValidator
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TSS.Audit.DTOs.Core;
using TSS.Audit.Resources;

#nullable disable
namespace TSS.Audit.WriteServices.Validation.Validators;

public class AuditTableValidator : AbstractValidator<AuditTable>
{
  public AuditTableValidator()
  {
    this.RuleFor<string>((Expression<Func<AuditTable, string>>) (x => x.Name)).NotEmpty<AuditTable, string>().MaximumLength<AuditTable>(50).MinimumLength<AuditTable>(3).Matches<AuditTable>("^([A-Za-z0-9_-]+\\.*)+$").WithLocalizedMessage<AuditTable, string>(typeof (Messages), "EntityTableNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditTable, string>>) (x => x.DescriptionFormat)).MaximumLength<AuditTable>(250);
    this.RuleFor<string>((Expression<Func<AuditTable, string>>) (x => x.IdColumnName)).MaximumLength<AuditTable>(50);
    this.RuleFor<string>((Expression<Func<AuditTable, string>>) (x => x.KeyFieldName)).MaximumLength<AuditTable>(50);
    this.RuleFor<string>((Expression<Func<AuditTable, string>>) (x => x.AuditByFieldName)).MaximumLength<AuditTable>(50);
    this.RuleFor<string>((Expression<Func<AuditTable, string>>) (x => x.AuditDateFieldName)).MaximumLength<AuditTable>(50);
    this.RuleFor<List<AuditTableColumn>>((Expression<Func<AuditTable, List<AuditTableColumn>>>) (x => x.Columns)).Must<AuditTable, List<AuditTableColumn>>(new Func<List<AuditTableColumn>, bool>(this.ValidateUniqueColumnNames)).WithLocalizedMessage<AuditTable, List<AuditTableColumn>>(typeof (Messages), "ColumnNamesDuplicated");
    ((IRuleBuilder<AuditTable, IEnumerable<AuditTableColumn>>) this.RuleFor<List<AuditTableColumn>>((Expression<Func<AuditTable, List<AuditTableColumn>>>) (x => x.Columns))).SetCollectionValidator<AuditTable, AuditTableColumn>((IValidator<AuditTableColumn>) new AuditTableColumnValidator());
  }

  private bool ValidateUniqueColumnNames(List<AuditTableColumn> auditColumns)
  {
    return auditColumns == null || auditColumns.Select<AuditTableColumn, string>((Func<AuditTableColumn, string>) (x => x.Name)).Distinct<string>().Count<string>() == auditColumns.Count<AuditTableColumn>();
  }
}
