// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ExternalServicesClients.Abstractions.Security.Contracts.ApplicationDto
// Assembly: TSS.Audit.ExternalServicesClients.Abstractions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F43BDB0A-852F-4F0E-9964-9C6DD884D0AA
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ExternalServicesClients.Abstractions.dll

#nullable disable
namespace TSS.Audit.ExternalServicesClients.Abstractions.Security.Contracts;

public class ApplicationDto : AuditEntityDto
{
  public string Code { get; set; }

  public string Name { get; set; }

  public bool Enabled { get; set; }

  public string LogoUri { get; set; }
}
