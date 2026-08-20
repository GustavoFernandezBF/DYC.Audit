// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ExternalServicesClients.Abstractions.Security.Contracts.ResponseDto`1
// Assembly: TSS.Audit.ExternalServicesClients.Abstractions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F43BDB0A-852F-4F0E-9964-9C6DD884D0AA
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ExternalServicesClients.Abstractions.dll

#nullable disable
namespace TSS.Audit.ExternalServicesClients.Abstractions.Security.Contracts;

public class ResponseDto<T> : ResponseDto
{
  public T Data { get; set; }
}
