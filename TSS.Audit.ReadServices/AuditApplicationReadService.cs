// Decompiled with JetBrains decompiler
// Type: TSS.Audit.ReadServices.AuditApplicationReadService
// Assembly: TSS.Audit.ReadServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A10258C3-6446-4BF1-813B-45DC267811FE
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.ReadServices.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSS.Audit.DTOs.Core;
using TSS.Audit.ExternalServicesClients.Abstractions.Security.Contracts;
using TSS.Audit.ExternalServicesClients.Abstractions.Security.Services;
using TSS.Audit.Mapping;
using TSS.Audit.ReadServices.Settings;
using TSS.Audit.Resources;

#nullable disable
namespace TSS.Audit.ReadServices;

public class AuditApplicationReadService : IDisposable
{
  private readonly IAuditMapper _mapper;
  private readonly ISecurityService _securityService;
  private readonly ReadServiceSettings _readServiceSettings;

  public AuditApplicationReadService(
    ISecurityService securityService,
    IAuditMapper mapper,
    ReadServiceSettings readServiceSettings)
  {
    this._mapper = mapper;
    this._securityService = securityService;
    this._readServiceSettings = readServiceSettings;
  }

  public async Task<ReadOperationResponse<AuditApp>> GetAuditApplicationAsync(string appCode)
  {
    ReadOperationResponse<AuditApp> getApplication = this.ValidateParametersFormatToGetApplication<AuditApp>(appCode);
    if (!getApplication.IsCorrect)
      return getApplication;
    ResponseDto<ApplicationDto> securityServiceAsync = await this.GetApplicationFromSecurityServiceAsync(appCode);
    ReadOperationResponse<AuditApp> applicationResponseDto = this.ValidateGetApplicationResponseDto(securityServiceAsync);
    return applicationResponseDto.IsCorrect ? ReadOperationBuilder.Correct<AuditApp>(this._mapper.Map<ApplicationDto, AuditApp>(securityServiceAsync.Data)) : applicationResponseDto;
  }

  public async Task<ReadOperationResponse<List<AuditApp>>> GetAuditApplicationByUserAsync(
    string username)
  {
    ReadOperationResponse<List<AuditApp>> applicationByUser = this.ValidateParametersFormatToGetApplicationByUser<List<AuditApp>>(username);
    if (!applicationByUser.IsCorrect)
      return applicationByUser;
    ResponseDto<List<ApplicationDto>> securityServiceAsync = await this.GetApplicationListFromSecurityServiceAsync(username);
    ReadOperationResponse<List<AuditApp>> byUserResponseDto = this.ValidateGetApplicationByUserResponseDto(securityServiceAsync);
    return byUserResponseDto.IsCorrect ? (securityServiceAsync.Data != null ? ReadOperationBuilder.Correct<List<AuditApp>>(this._mapper.Map<List<ApplicationDto>, List<AuditApp>>(securityServiceAsync.Data)) : ReadOperationBuilder.Correct<List<AuditApp>>(new List<AuditApp>())) : byUserResponseDto;
  }

  private ReadOperationResponse<T> ValidateParametersFormatToGetApplication<T>(string appCode)
  {
    return !string.IsNullOrWhiteSpace(appCode) ? ReadOperationBuilder.CreateReadOperationResponse<T>() : ReadOperationBuilder.CreateReadOperationResponse<T>(Messages.AppInvalid, OperationStatus.Invalid);
  }

  private Task<ResponseDto<ApplicationDto>> GetApplicationFromSecurityServiceAsync(string appCode)
  {
    return this._securityService.GetApplicationAsync(appCode);
  }

  private ReadOperationResponse<AuditApp> ValidateGetApplicationResponseDto(
    ResponseDto<ApplicationDto> responseDto)
  {
    if (responseDto == null)
      return ReadOperationBuilder.WithMessage<AuditApp>(Messages.NoExternalServiceResponseReceived, OperationStatus.ServerError);
    if (!responseDto.IsValid)
      return ReadOperationBuilder.CreateReadOperationResponse<AuditApp>(responseDto.Messages.Select<ApplicationMessage, string>((Func<ApplicationMessage, string>) (x => x.Message)).ToList<string>(), OperationStatus.BadGateway);
    return responseDto.Data == null ? ReadOperationBuilder.WithMessage<AuditApp>(Messages.AppNotExists, OperationStatus.NotFound) : ReadOperationBuilder.Correct<AuditApp>();
  }

  private ReadOperationResponse<T> ValidateParametersFormatToGetApplicationByUser<T>(string username)
  {
    return !string.IsNullOrWhiteSpace(username) ? ReadOperationBuilder.CreateReadOperationResponse<T>() : ReadOperationBuilder.CreateReadOperationResponse<T>(Messages.UsernameInvalid, OperationStatus.Invalid);
  }

  private Task<ResponseDto<List<ApplicationDto>>> GetApplicationListFromSecurityServiceAsync(
    string username)
  {
    return this._securityService.GetApplicationByUserAsync(username, this._readServiceSettings.ExternalApplicationAuditRoleCode);
  }

  private ReadOperationResponse<List<AuditApp>> ValidateGetApplicationByUserResponseDto(
    ResponseDto<List<ApplicationDto>> responseDto)
  {
    if (responseDto == null)
      return ReadOperationBuilder.WithMessage<List<AuditApp>>(Messages.NoExternalServiceResponseReceived, OperationStatus.ServerError);
    return !responseDto.IsValid ? ReadOperationBuilder.CreateReadOperationResponse<List<AuditApp>>(responseDto.Messages.Select<ApplicationMessage, string>((Func<ApplicationMessage, string>) (x => x.Message)).ToList<string>(), OperationStatus.BadGateway) : ReadOperationBuilder.Correct<List<AuditApp>>();
  }

  public void Dispose() => this._securityService?.Dispose();
}
