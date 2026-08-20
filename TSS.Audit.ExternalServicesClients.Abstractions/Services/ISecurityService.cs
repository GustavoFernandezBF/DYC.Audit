// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ExternalServicesClients.Abstractions.Security.Services.ISecurityService
// Assembly: TSS.Audit.ExternalServicesClients.Abstractions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F43BDB0A-852F-4F0E-9964-9C6DD884D0AA
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ExternalServicesClients.Abstractions.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSS.Audit.ExternalServicesClients.Abstractions.Security.Contracts;

#nullable disable
namespace TSS.Audit.ExternalServicesClients.Abstractions.Security.Services;

public interface ISecurityService : IDisposable
{
  Task<ResponseDto<ApplicationDto>> GetApplicationAsync(string appCode);

  Task<ResponseDto<List<ApplicationDto>>> GetApplicationByUserAsync(
    string username,
    string applicationRoleCode);
}
