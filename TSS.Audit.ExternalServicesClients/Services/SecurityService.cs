// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ExternalServicesClients.Security.Services.SecurityService
// Assembly: TSS.Audit.ExternalServicesClients, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0467B7C2-D14D-4069-AC97-E0E900EF0DDE
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ExternalServicesClients.dll

using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TSS.Audit.ExternalServicesClients.Abstractions.Security.Contracts;
using TSS.Audit.ExternalServicesClients.Abstractions.Security.Services;
using TSS.Audit.ExternalServicesClients.Security.Settings;
using TSS.Audit.Resources;

#nullable disable
namespace TSS.Audit.ExternalServicesClients.Security.Services;

public class SecurityService : ISecurityService, IDisposable
{
  private readonly SecurityServiceSettings _securityServiceSettings;
  private readonly Uri _securityServiceBaseUri;
  private TokenClient _securityTokenClient;
  private TokenResponse _securityTokenResponse;

  public SecurityService(SecurityServiceSettings securityServiceSettings)
  {
    this._securityServiceSettings = securityServiceSettings ?? throw new ArgumentNullException(Messages.MissingExternalSecurityServiceSettings);
    this._securityServiceBaseUri = new Uri(this._securityServiceSettings.SecurityServiceHostUrl);
  }

  public async Task<ResponseDto<ApplicationDto>> GetApplicationAsync(string appCode)
  {
    ResponseDto<ApplicationDto> applicationAsync = (ResponseDto<ApplicationDto>) null;
    await this.InitializeSecurityConnectionObjectsAsync();
    using (HttpClient httpClient = new HttpClient())
    {
      this.SetHttpRequestHeaders(httpClient);
      applicationAsync = await this.GetApplicationByCodeAsync(httpClient, appCode);
    }
    return applicationAsync;
  }

  public async Task<ResponseDto<List<ApplicationDto>>> GetApplicationByUserAsync(
    string username,
    string applicationRoleCode)
  {
    ResponseDto<List<ApplicationDto>> applicationByUserAsync = (ResponseDto<List<ApplicationDto>>) null;
    await this.InitializeSecurityConnectionObjectsAsync();
    using (HttpClient httpClient = new HttpClient())
    {
      this.SetHttpRequestHeaders(httpClient);
      applicationByUserAsync = await this.GetApplicationByUserAsync(httpClient, username, applicationRoleCode);
    }
    return applicationByUserAsync;
  }

  public void Dispose() => this._securityTokenClient?.Dispose();

  private async Task InitializeSecurityConnectionObjectsAsync()
  {
    if (this._securityTokenResponse != null)
      return;
    await this.GetTokenClientAndResponseAsync();
  }

  private async Task GetTokenClientAndResponseAsync()
  {
    this._securityTokenClient = new TokenClient(this._securityServiceSettings.SecurityAccessTokenEndpoint, this._securityServiceSettings.ClientId, this._securityServiceSettings.ClientSecret);
    this._securityTokenResponse = await this._securityTokenClient.RequestClientCredentialsAsync(this._securityServiceSettings.AllowedScopes);
    if (this._securityTokenResponse.IsError)
      throw new Exception(this._securityTokenResponse.Error, this._securityTokenResponse.Exception);
  }

  private void SetHttpRequestHeaders(HttpClient httpClient)
  {
    httpClient.SetBearerToken(this._securityTokenResponse.AccessToken);
  }

  private async Task<ResponseDto<ApplicationDto>> GetApplicationByCodeAsync(
    HttpClient httpClient,
    string applicationCode)
  {
    HttpResponseMessage async = await httpClient.GetAsync(new Uri(this._securityServiceBaseUri, $"api/application/{applicationCode}"));
    async.EnsureSuccessStatusCode();
    return JsonConvert.DeserializeObject<ResponseDto<ApplicationDto>>(await async.Content.ReadAsStringAsync());
  }

  private async Task<ResponseDto<List<ApplicationDto>>> GetApplicationByUserAsync(
    HttpClient httpClient,
    string username,
    string applicationRoleCode)
  {
    HttpResponseMessage async = await httpClient.GetAsync(new Uri(this._securityServiceBaseUri, $"api/application/user/{username}/{applicationRoleCode}"));
    async.EnsureSuccessStatusCode();
    return JsonConvert.DeserializeObject<ResponseDto<List<ApplicationDto>>>(await async.Content.ReadAsStringAsync());
  }
}
