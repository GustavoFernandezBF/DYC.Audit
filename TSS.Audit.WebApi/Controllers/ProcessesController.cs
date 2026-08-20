// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Controllers.ProcessesController
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Request;
using TSS.Audit.DTOs.Response;
using TSS.Audit.ReadServices;
using TSS.Audit.Resources;
using TSS.Audit.WebApi.Extensions;
using TSS.Audit.WriteServices;

#nullable disable
namespace TSS.Audit.WebApi.Controllers;

[Authorize]
[Route("api/apps")]
public class ProcessesController : BaseController
{
  private readonly AuditProcessDescriptionWriteService _auditProcessWriteService;
  private readonly AuditProcessDescriptionReadService _auditProcessDescriptionReadService;

  public ProcessesController(
    AuditProcessDescriptionWriteService auditProcessWriteService,
    AuditProcessDescriptionReadService auditProcessDescriptionReadService)
  {
    this._auditProcessWriteService = auditProcessWriteService;
    this._auditProcessDescriptionReadService = auditProcessDescriptionReadService;
  }

  /// <summary>
  /// List all processes available for the application specified.
  /// </summary>
  /// <param name="appCode"></param>
  /// <returns></returns>
  [HttpGet]
  [Route("{appCode}/processes")]
  [ProducesResponseType(typeof (IList<AuditProcess>), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Get(string appCode)
  {
    return string.IsNullOrWhiteSpace(appCode) ? (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.AppInvalid)) : (IActionResult) this.Ok((object) await (Task<List<AuditProcess>>) this._auditProcessDescriptionReadService.FindByApplicationAsync(appCode));
  }

  /// <summary>Register a process application.</summary>
  /// <param name="appCode"></param>
  /// <param name="registerAuditProcessDescription"></param>
  /// <returns></returns>
  [HttpPost]
  [Route("{appCode}/processes")]
  [ProducesResponseType(typeof (OperationResponse), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 409)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Post(
    string appCode,
    [FromBody] AuditProcess registerAuditProcessDescription)
  {
    return (await (Task<WriteOperationResponse>) this._auditProcessWriteService.RegisterAsync(appCode, registerAuditProcessDescription)).ToActionResult();
  }

  /// <summary>Unregister a process application.</summary>
  /// <param name="appCode"></param>
  /// <param name="auditProcessDelete"></param>
  /// <returns></returns>
  [HttpDelete]
  [Route("{appCode}/processes")]
  [ProducesResponseType(typeof (OperationResponse), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Delete(string appCode, [FromBody] AuditProcessDelete auditProcessDelete)
  {
    return (await (Task<WriteOperationResponse>) this._auditProcessWriteService.DeleteAsync(appCode, auditProcessDelete)).ToActionResult();
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (!disposing)
      return;
    this._auditProcessWriteService.Dispose();
  }
}
