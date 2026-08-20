// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.Settings.WriteServiceSettings
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

#nullable disable
namespace TSS.Audit.WriteServices.Settings;

public class WriteServiceSettings
{
  public bool ValidateAuditEntityTablesExistence { get; set; }

  public bool ValidateAuditEntityTableColumnsExistence { get; set; }
}
