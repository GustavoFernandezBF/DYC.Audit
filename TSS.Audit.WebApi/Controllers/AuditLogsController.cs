// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Controllers.AuditLogsController
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TSS.Audit.Common;
using TSS.Audit.DTOs;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Request;
using TSS.Audit.DTOs.Response;
using TSS.Audit.ReadServices;
using TSS.Audit.Resources;
using TSS.Audit.WebApi.Extensions;
using TSS.Audit.WebApi.Helpers;
using TSS.Audit.WriteServices;

#nullable disable
namespace TSS.Audit.WebApi.Controllers;

[Authorize]
[Route("api/apps")]
public class AuditLogsController : BaseController
{
  private readonly AuditLogWriteService _auditLogWriteService;
  private readonly AuditLogReadService _auditLogReadService;
  private readonly TSS.Audit.Common.Helpers.IdsHasher.IdsHasher _idsHasher;
  private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

  public AuditLogsController(
    AuditLogWriteService auditLogWriteService,
    AuditLogReadService auditLogReadService,
    TSS.Audit.Common.Helpers.IdsHasher.IdsHasher idsHasher)
  {
    this._auditLogWriteService = auditLogWriteService;
    this._auditLogReadService = auditLogReadService;
    this._idsHasher = idsHasher;
  }

  /// <summary>Get audit log data</summary>
  /// <param name="appCode"></param>
  /// <param name="from"></param>
  /// <param name="to"></param>
  /// <param name="process"></param>
  /// <param name="processModule"></param>
  /// <param name="entity"></param>
  /// <param name="entityModule"></param>
  /// <param name="entityKeyValue"></param>
  /// <param name="by"></param>
  /// <param name="actions"></param>
  /// <param name="page"></param>
  /// <param name="pageSize"></param>
  /// <param name="sortFieldName"></param>
  /// <param name="ascending"></param>
  /// <returns></returns>
  [HttpGet]
  [Route("{appCode}/log")]
  [ProducesResponseType(typeof (PaginatedResponse<AuditLogResponse>), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Search(
    string appCode,
    string from = null,
    string to = null,
    string process = null,
    string processModule = null,
    string entity = null,
    string entityModule = null,
    string entityKeyValue = null,
    string by = null,
    Constants.TableOperation? actions = null,
    int? page = null,
    int? pageSize = null,
    string sortFieldName = null,
    bool? ascending = null)
  {
    Guid? authenticatedUserTenantId = this.GetAuthenticatedUserTenantId();
    DateTime? dateTimeNullableFrom = new DateTime?();
    DateTime? dateTimeNullableTo = new DateTime?();
    BadRequestObjectResult requestObjectResult = this.ValidateFormatParametersGet(authenticatedUserTenantId, appCode, from, to, out dateTimeNullableFrom, out dateTimeNullableTo);
    if (requestObjectResult != null)
      return (IActionResult) requestObjectResult;
    AuditLogSearch auditLogSearch = new AuditLogSearch((DateTime?) dateTimeNullableFrom, (DateTime?) dateTimeNullableTo, process, processModule, entity, entityModule, entityKeyValue, by, (Constants.TableOperation?) actions, (int?) page, (int?) pageSize);
    return (IActionResult) this.Ok((object) await (Task<PaginatedResponse<AuditLogResponse>>) this._auditLogReadService.FindAuditDataLogAsync((Guid) authenticatedUserTenantId.Value, appCode, auditLogSearch, this.GetSortDescriptor(sortFieldName, ascending)));
  }

  /// <summary>Register an audit log.</summary>
  /// <param name="appCode"></param>
  /// <param name="appLog"></param>
  /// <returns></returns>
  [HttpPost]
  [Route("{appCode}/log")]
  [ProducesResponseType(typeof (OperationResponse), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Post(string appCode, [FromBody] AuditLog appLog)
  {
    return (await (Task<WriteOperationResponse>) this._auditLogWriteService.RegisterAsync(appCode, appLog)).ToActionResult();
  }

  /// <summary>Get audit log data advanced</summary>
  /// <param name="appCode"></param>
  /// <param name="auditLogSearch"></param>
  /// <param name="sortFieldName"></param>
  /// <param name="ascending"></param>
  /// <returns></returns>
  [HttpPost]
  [Route("{appCode}/log/advanced-search")]
  [ProducesResponseType(typeof (PaginatedResponse<AuditLogResponse>), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> SearchAdvanced(
    string appCode,
    [FromBody] AuditLogSearch auditLogSearch,
    string sortFieldName = null,
    bool? ascending = null)
  {
    Guid? authenticatedUserTenantId = this.GetAuthenticatedUserTenantId();
    if (!authenticatedUserTenantId.HasValue)
      return (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.TenantInvalid));
    return string.IsNullOrWhiteSpace(appCode) ? (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.AppInvalid)) : (IActionResult) this.Ok((object) await (Task<PaginatedResponse<AuditLogResponse>>) this._auditLogReadService.FindAuditDataLogAsync((Guid) authenticatedUserTenantId.Value, appCode, auditLogSearch, this.GetSortDescriptor(sortFieldName, ascending)));
  }

  /// <summary>Get audit logs data result file</summary>
  /// <param name="appCode"></param>
  /// <param name="auditLogSearch"></param>
  /// <returns></returns>
  [HttpPost]
  [Route("{appCode}/log/export-search")]
  [ProducesResponseType(typeof (FileStreamResult), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> ExportAuditLogsData(
    string appCode,
    [FromBody] AuditLogSearch auditLogSearch)
  {
    Guid? authenticatedUserTenantId = this.GetAuthenticatedUserTenantId();
    if (!authenticatedUserTenantId.HasValue)
      return (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.TenantInvalid));
    if (string.IsNullOrWhiteSpace(appCode))
      return (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.AppInvalid));
    MemoryStream logDataExcelFile = ExcelExportHelper.CreateAuditLogDataExcelFile((await (Task<IList<AuditLogDataExport>>) this._auditLogReadService.GetAuditLogDataToExportAsync((Guid) authenticatedUserTenantId.Value, appCode, auditLogSearch)).ToList<AuditLogDataExport>());
    this.SetResponseHeaderForExportProcess();
    return (IActionResult) this.File((Stream) logDataExcelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{appCode}.xlsx");
  }

  /// <summary>Get audit log data result file</summary>
  /// <param name="appCode"></param>
  /// <param name="transactionCode"></param>
  /// <returns></returns>
  [HttpGet]
  [Route("{appCode}/log/export-search/{transactionCode}")]
  [ProducesResponseType(typeof (FileStreamResult), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> ExportAuditLogData(string appCode, string transactionCode)
  {
    if (!this.GetAuthenticatedUserTenantId().HasValue)
      return (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.TenantInvalid));
    if (string.IsNullOrWhiteSpace(appCode) || string.IsNullOrWhiteSpace(transactionCode))
      return (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.AppInvalid));
    MemoryStream logDataExcelFile = ExcelExportHelper.CreateAuditLogDataExcelFile((await (Task<IList<AuditLogDataExport>>) this._auditLogReadService.GetAuditLogDataToExportAsync((long?) this._idsHasher.DecodeLongId(transactionCode) ?? 0L)).ToList<AuditLogDataExport>());
    this.SetResponseHeaderForExportProcess();
    return (IActionResult) this.File((Stream) logDataExcelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{appCode}-{transactionCode}.xlsx");
  }

  /// <summary>
  /// Get the table column values stored in the audit system
  /// </summary>
  /// <param name="appCode"></param>
  /// <param name="auditTableLogSnapshotRequest"></param>
  /// <returns></returns>
  [HttpPost]
  [Route("{appCode}/log/get-table-snapshot")]
  [ProducesResponseType(typeof (AuditTableLogSnapshotResponse), 200)]
  [ProducesResponseType(204)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> GetAuditTableLogSnapshotAsync(
    string appCode,
    [FromBody] AuditTableLogSnapshotRequest auditTableLogSnapshotRequest)
  {
    return (IActionResult) this.Ok((object) await (Task<AuditTableLogSnapshotResponse>) this._auditLogReadService.GetLastAuditTableLogSnapshotAsync(appCode, auditTableLogSnapshotRequest));
  }

  private BadRequestObjectResult ValidateFormatParametersGet(
    Guid? tenantId,
    string appCode,
    string from,
    string to,
    out DateTime? dateTimeNullableFrom,
    out DateTime? dateTimeNullableTo)
  {
    dateTimeNullableFrom = new DateTime?();
    dateTimeNullableTo = new DateTime?();
    if (!tenantId.HasValue)
      return this.BadRequest((object) OperationBuilder.WithMessage(Messages.TenantInvalid));
    if (string.IsNullOrWhiteSpace(appCode))
      return this.BadRequest((object) OperationBuilder.WithMessage(Messages.AppInvalid));
    DateTime result1 = new DateTime();
    DateTime result2 = new DateTime();
    string format = "yyyyMMddHHmmss";
    if (!string.IsNullOrWhiteSpace(from))
    {
      if (!DateTime.TryParseExact(from.Trim(), format, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result1))
        return this.BadRequest((object) OperationBuilder.WithMessage(Messages.FromDateInvalid));
      dateTimeNullableFrom = new DateTime?(result1);
    }
    if (!string.IsNullOrWhiteSpace(to))
    {
      if (!DateTime.TryParseExact(to.Trim(), format, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result2))
        return this.BadRequest((object) OperationBuilder.WithMessage(Messages.ToDateInvalid));
      dateTimeNullableTo = new DateTime?(result2);
    }
    return (BadRequestObjectResult) null;
  }

  private void SetResponseHeaderForExportProcess()
  {
    ((IDictionary<string, StringValues>) this.Response.Headers).Add("Access-Control-Expose-Headers", (StringValues) "Content-Disposition");
  }

  private SortDescriptor GetSortDescriptor(string columnName, bool? ascending)
  {
    SortDescriptor sortDescriptor = (SortDescriptor) null;
    if (!string.IsNullOrWhiteSpace(columnName))
      sortDescriptor = new SortDescriptor()
      {
        FieldName = columnName,
        Ascending = ascending ?? false
      };
    return sortDescriptor;
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (!disposing)
      return;
    this._auditLogWriteService.Dispose();
  }
}
