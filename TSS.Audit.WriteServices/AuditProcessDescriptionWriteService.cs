// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WriteServices.AuditProcessDescriptionWriteService
// Assembly: TSS.Audit.WriteServices, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DA5016C0-9FEC-4A45-98E8-F75EBF083899
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WriteServices.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TSS.Audit.Domain;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Request;
using TSS.Audit.DTOs.Response;
using TSS.Audit.Persistence;
using TSS.Audit.Persistence.Commands;
using TSS.Audit.Resources;
using TSS.Audit.WriteServices.Validation;

#nullable disable
namespace TSS.Audit.WriteServices;

public class AuditProcessDescriptionWriteService : IDisposable
{
  private readonly IAuditUnitOfWork _unitOfWork;
  private readonly IAuditRepository<AuditProcessDescription> _auditProcessRepository;
  private readonly IValidationService _validationService;

  public AuditProcessDescriptionWriteService(
    IAuditUnitOfWork unitOfWork,
    IAuditRepository<AuditProcessDescription> auditProcessRepository,
    IValidationService validationService)
  {
    this._unitOfWork = unitOfWork;
    this._auditProcessRepository = auditProcessRepository;
    this._validationService = validationService;
  }

  public async Task<WriteOperationResponse> RegisterAsync(string appCode, AuditProcess newProcess)
  {
    WriteOperationResponse registerNewProcessAsync = await this.ValidateDataToRegisterNewProcessAsync(appCode, newProcess);
    if (!registerNewProcessAsync.IsCorrect)
      return registerNewProcessAsync;
    await this.AddNewProcessAsync(appCode, newProcess);
    await this.CommitTransactionAsync();
    return WriteOperationBuilder.CreateWriteOperationResponse();
  }

  public async Task<WriteOperationResponse> DeleteAsync(
    string appCode,
    AuditProcessDelete auditProcess)
  {
    WriteOperationResponse deleteProcessAsync = await this.ValidateDataToDeleteProcessAsync(appCode, auditProcess);
    if (!deleteProcessAsync.IsCorrect)
      return deleteProcessAsync;
    await this.DeleteProcessAsync(appCode, auditProcess);
    await this.CommitTransactionAsync();
    return WriteOperationBuilder.CreateWriteOperationResponse();
  }

  public void Dispose() => this._unitOfWork?.Dispose();

  private async Task<WriteOperationResponse> ValidateDataToRegisterNewProcessAsync(
    string appCode,
    AuditProcess newProcess)
  {
    WriteOperationResponse registerNewProcess = this.ValidateParametersFormatToRegisterNewProcess(appCode, newProcess);
    return !registerNewProcess.IsCorrect ? registerNewProcess : await this.ValidateDataIntegrityToRegisterNewProcessAsync(appCode, newProcess);
  }

  private WriteOperationResponse ValidateParametersFormatToRegisterNewProcess(
    string appCode,
    AuditProcess newProcess)
  {
    if (newProcess == null || string.IsNullOrWhiteSpace(appCode))
      return WriteOperationBuilder.CreateWriteOperationResponse(Messages.BadParameter, OperationStatus.Invalid);
    OperationResponse operationResponse = this._validationService.Validate<AuditProcess>(newProcess);
    return operationResponse.IsCorrect ? WriteOperationBuilder.CreateWriteOperationResponse() : operationResponse.WithStatus(OperationStatus.Invalid);
  }

  private async Task<WriteOperationResponse> ValidateDataIntegrityToRegisterNewProcessAsync(
    string appCode,
    AuditProcess newProcess)
  {
    return await this.ValidateAuditProcessExistenceAsync(appCode, newProcess.Name, newProcess.Module) ? WriteOperationBuilder.CreateWriteOperationResponse(Messages.ProcessAlreadyExists, OperationStatus.Conflict) : WriteOperationBuilder.CreateWriteOperationResponse();
  }

  private Task<bool> ValidateAuditProcessExistenceAsync(
    string appCode,
    string processName,
    string module)
  {
    return (Task<bool>) this._auditProcessRepository.AnyAsync((Expression<Func<AuditProcessDescription, bool>>) (x => string.Equals(x.ApplicationCode, appCode, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Name, processName, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Module, module, StringComparison.OrdinalIgnoreCase)));
  }

  private async Task AddNewProcessAsync(string appCode, AuditProcess newProcess)
  {
    if (string.IsNullOrWhiteSpace(appCode) || newProcess == null)
      return;
    if (await (Task<bool>) this._auditProcessRepository.AnyAsync((Expression<Func<AuditProcessDescription, bool>>) (x => string.Equals(x.ApplicationCode, appCode, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Name, newProcess.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Module, newProcess.Module, StringComparison.OrdinalIgnoreCase))))
      return;
    await (Task) this._auditProcessRepository.AddAsync(new AuditProcessDescription(appCode, newProcess.Name, newProcess.Module)
    {
      Description = newProcess.Description
    });
  }

  private Task CommitTransactionAsync() => (Task) this._unitOfWork.CommitAsync();

  private async Task<WriteOperationResponse> ValidateDataToDeleteProcessAsync(
    string appCode,
    AuditProcessDelete auditProcess)
  {
    WriteOperationResponse delete = this.ValidateParametersFormatToDelete(appCode, auditProcess);
    return !delete.IsCorrect ? delete : await this.ValidateDataIntegrityToDeleteAsync(appCode, auditProcess);
  }

  private WriteOperationResponse ValidateParametersFormatToDelete(
    string appCode,
    AuditProcessDelete auditProcess)
  {
    if (string.IsNullOrWhiteSpace(appCode))
      return WriteOperationBuilder.CreateWriteOperationResponse(Messages.BadParameter, OperationStatus.Invalid);
    OperationResponse operationResponse = this._validationService.Validate<AuditProcessDelete>(auditProcess);
    return operationResponse.IsCorrect ? WriteOperationBuilder.CreateWriteOperationResponse() : operationResponse.WithStatus(OperationStatus.Invalid);
  }

  private async Task<WriteOperationResponse> ValidateDataIntegrityToDeleteAsync(
    string appCode,
    AuditProcessDelete auditProcess)
  {
    return !await this.ValidateAuditProcessExistenceAsync(appCode, auditProcess.Name, auditProcess.Module) ? WriteOperationBuilder.CreateWriteOperationResponse(Messages.ProcessNotExists, OperationStatus.NotFound) : WriteOperationBuilder.CreateWriteOperationResponse();
  }

  private async Task DeleteProcessAsync(string appCode, AuditProcessDelete auditProcess)
  {
    AuditProcessDescription descriptionAsync = await this.GetAuditProcessDescriptionAsync(appCode, auditProcess.Name, auditProcess.Module, new string[1]
    {
      "AuditProcessLogs"
    });
    if (descriptionAsync == null)
      return;
    if (this.ValidateExistenceOfAuditDataLinkedToProcess(descriptionAsync))
      this.UpdateAuditEntityTableColumnEnabledProperty(descriptionAsync, false);
    else
      await this.DeleteProcessPhysicallyAsync(descriptionAsync);
  }

  private Task<AuditProcessDescription> GetAuditProcessDescriptionAsync(
    string appCode,
    string processName,
    string module,
    string[] includes = null)
  {
    return this._auditProcessRepository.FirstOrDefaultAsync((Expression<Func<AuditProcessDescription, bool>>) (x => string.Equals(x.ApplicationCode, appCode, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Name, processName, StringComparison.OrdinalIgnoreCase) && string.Equals(x.Module, module, StringComparison.OrdinalIgnoreCase)), includes: includes);
  }

  private bool ValidateExistenceOfAuditDataLinkedToProcess(
    AuditProcessDescription auditProcessDescription)
  {
    return auditProcessDescription.AuditProcessLogs.Any<AuditProcessLog>();
  }

  private void UpdateAuditEntityTableColumnEnabledProperty(
    AuditProcessDescription auditProcessDescription,
    bool enabled)
  {
    if (auditProcessDescription == null)
      return;
    auditProcessDescription.Enabled = enabled;
  }

  private Task DeleteProcessPhysicallyAsync(AuditProcessDescription auditProcessDescription)
  {
    return auditProcessDescription != null ? (Task) this._auditProcessRepository.DeleteAsync(auditProcessDescription) : Task.CompletedTask;
  }
}
