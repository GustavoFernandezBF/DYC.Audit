// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Validation.Validators.AuditEntityValidator
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

public class AuditEntityValidator : AbstractValidator<AuditEntity>
{
  public AuditEntityValidator()
  {
    this.RuleFor<string>((Expression<Func<AuditEntity, string>>) (x => x.Name)).NotEmpty<AuditEntity, string>().MaximumLength<AuditEntity>(50).MinimumLength<AuditEntity>(3).Matches<AuditEntity>("^([A-Za-z0-9_-]+\\.*)+$").WithLocalizedMessage<AuditEntity, string>(typeof (Messages), "EntityNameInvalid");
    this.RuleFor<string>((Expression<Func<AuditEntity, string>>) (x => x.Module)).NotEmpty<AuditEntity, string>().MaximumLength<AuditEntity>(50).MinimumLength<AuditEntity>(3).Matches<AuditEntity>("^([\\sA-Za-z0-9_-]+\\.*)+$").WithLocalizedMessage<AuditEntity, string>(typeof (Messages), "ModuleInvalid");
    this.RuleFor<List<AuditTable>>((Expression<Func<AuditEntity, List<AuditTable>>>) (x => x.Tables)).Must<AuditEntity, List<AuditTable>>(new Func<List<AuditTable>, bool>(this.ValidateUniqueTableNames)).WithLocalizedMessage<AuditEntity, List<AuditTable>>(typeof (Messages), "TableNamesDuplicated");
    ((IRuleBuilder<AuditEntity, IEnumerable<AuditTable>>) this.RuleFor<List<AuditTable>>((Expression<Func<AuditEntity, List<AuditTable>>>) (x => x.Tables))).SetCollectionValidator<AuditEntity, AuditTable>((IValidator<AuditTable>) new AuditTableValidator());
  }

  private bool ValidateUniqueTableNames(List<AuditTable> auditTables)
  {
    return auditTables == null || auditTables.Select<AuditTable, string>((Func<AuditTable, string>) (x => x.Name)).Distinct<string>().Count<string>() == auditTables.Count<AuditTable>();
  }
}
