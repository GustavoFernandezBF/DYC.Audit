// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Controllers.EntitiesController
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
public class EntitiesController : BaseController
{
  private readonly AuditEntityWriteService _auditEntityWriteService;
  private readonly AuditEntityReadService _auditEntityReadService;

  public EntitiesController(
    AuditEntityWriteService auditEntityWriteService,
    AuditEntityReadService auditEntityReadService)
  {
    this._auditEntityWriteService = auditEntityWriteService;
    this._auditEntityReadService = auditEntityReadService;
  }

  /// <summary>Get entities of an application.</summary>
  /// <param name="appCode"></param>
  /// <returns></returns>
  [HttpGet]
  [Route("{appCode}/entities")]
  [ProducesResponseType(typeof (IList<AuditEntity>), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Get(string appCode)
  {
    return string.IsNullOrWhiteSpace(appCode) ? (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.AppInvalid)) : (IActionResult) this.Ok((object) await (Task<List<AuditEntity>>) this._auditEntityReadService.FindByApplicationAsync(appCode, true));
  }

  /// <summary>Register an entity.</summary>
  /// <param name="appCode"></param>
  /// <param name="newEntity"></param>
  /// <returns></returns>
  [HttpPost]
  [Route("{appCode}/entities")]
  [ProducesResponseType(typeof (OperationResponse), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 409)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Post(string appCode, [FromBody] AuditEntity newEntity)
  {
    return (await (Task<WriteOperationResponse>) this._auditEntityWriteService.RegisterAsync(appCode, newEntity)).ToActionResult();
  }

  /// <summary>Update an entity.</summary>
  /// <param name="appCode"></param>
  /// <param name="newEntity"></param>
  /// <returns></returns>
  [HttpPut]
  [Route("{appCode}/entities")]
  [ProducesResponseType(typeof (OperationResponse), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Put(string appCode, [FromBody] AuditEntity newEntity)
  {
    return (await (Task<WriteOperationResponse>) this._auditEntityWriteService.UpdateAsync(appCode, newEntity)).ToActionResult();
  }

  /// <summary>Unregister an entity.</summary>
  /// <param name="appCode"></param>
  /// <param name="auditEntityDelete"></param>
  /// <returns></returns>
  [HttpDelete]
  [Route("{appCode}/entities")]
  [ProducesResponseType(typeof (OperationResponse), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public async Task<IActionResult> Delete(string appCode, [FromBody] AuditEntityDelete auditEntityDelete)
  {
    return (await (Task<WriteOperationResponse>) this._auditEntityWriteService.DeleteAsync(appCode, auditEntityDelete)).ToActionResult();
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (!disposing)
      return;
    this._auditEntityWriteService.Dispose();
  }
}
