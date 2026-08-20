// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ExternalServicesClients.Security.Settings.SecurityServiceSettings
// Assembly: TSS.Audit.ExternalServicesClients, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0467B7C2-D14D-4069-AC97-E0E900EF0DDE
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ExternalServicesClients.dll

#nullable disable
namespace TSS.Audit.ExternalServicesClients.Security.Settings;

public class SecurityServiceSettings
{
  public string SecurityAccessTokenEndpoint { get; set; }

  public string ClientId { get; set; }

  public string ClientSecret { get; set; }

  public string AllowedScopes { get; set; }

  public string SecurityServiceHostUrl { get; set; }
}
