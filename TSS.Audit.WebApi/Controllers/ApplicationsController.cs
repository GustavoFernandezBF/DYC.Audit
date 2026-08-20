// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Controllers.ApplicationsController
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSS.Audit.DTOs.Core;
using TSS.Audit.DTOs.Response;
using TSS.Audit.ReadServices;
using TSS.Audit.Resources;
using TSS.Audit.WebApi.Extensions;

#nullable disable
namespace TSS.Audit.WebApi.Controllers;

[Authorize]
[Route("api/apps")]
public class ApplicationsController : BaseController
{
  private readonly AuditApplicationReadService _auditApplicationReadService;

  public ApplicationsController(
    AuditApplicationReadService auditApplicationReadService)
  {
    this._auditApplicationReadService = auditApplicationReadService;
  }

  /// <summary>Get application details.</summary>
  /// <param name="code"></param>
  /// <returns></returns>
  [HttpGet]
  [Route("{appCode}")]
  [ProducesResponseType(typeof (AuditApp), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 401)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  [ProducesResponseType(typeof (OperationResponse), 502)]
  public async Task<IActionResult> Get(string appCode)
  {
    string authenticatedUserName = this.GetAuthenticatedUserName();
    if (string.IsNullOrWhiteSpace(appCode))
      return (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.AppInvalid));
    if (string.IsNullOrWhiteSpace(authenticatedUserName))
      return (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.UsernameInvalid));
    Task<ReadOperationResponse<AuditApp>> appTask = (Task<ReadOperationResponse<AuditApp>>) this._auditApplicationReadService.GetAuditApplicationAsync(appCode);
    Task<ReadOperationResponse<List<AuditApp>>> userAppTask = (Task<ReadOperationResponse<List<AuditApp>>>) this._auditApplicationReadService.GetAuditApplicationByUserAsync(authenticatedUserName);
    await Task.WhenAll((Task) appTask, (Task) userAppTask);
    if (!appTask.Result.IsCorrect)
      return appTask.Result.ToActionResult();
    if (appTask.Result.Data == null)
      return (IActionResult) this.NotFound((object) OperationBuilder.WithMessage(Messages.AppNotExists));
    if (!userAppTask.Result.IsCorrect)
      return userAppTask.Result.ToActionResult();
    List<AuditApp> data = userAppTask.Result.Data;
    AuditApp auditApp = data != null ? data.FirstOrDefault<AuditApp>((Func<AuditApp, bool>) (x => string.Equals(x.Code, appCode))) : (AuditApp) null;
    if (auditApp != null)
      return (IActionResult) this.Ok((object) auditApp);
    return (IActionResult) new ObjectResult((object) OperationBuilder.WithMessage(Messages.UserNotHavePermissions))
    {
      StatusCode = (int?) new int?(401)
    };
  }

  /// <summary>Get application list for the user authenticated.</summary>
  /// <returns></returns>
  [HttpGet]
  [Route("user")]
  [ProducesResponseType(typeof (List<AuditApp>), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  [ProducesResponseType(typeof (OperationResponse), 502)]
  public async Task<IActionResult> GetByUser()
  {
    string authenticatedUserName = this.GetAuthenticatedUserName();
    if (string.IsNullOrWhiteSpace(authenticatedUserName))
      return (IActionResult) this.BadRequest((object) OperationBuilder.WithMessage(Messages.UsernameInvalid));
    ReadOperationResponse<List<AuditApp>> applicationByUserAsync = await (Task<ReadOperationResponse<List<AuditApp>>>) this._auditApplicationReadService.GetAuditApplicationByUserAsync(authenticatedUserName);
    return applicationByUserAsync.IsCorrect ? (IActionResult) this.Ok((object) applicationByUserAsync.Data) : applicationByUserAsync.ToActionResult();
  }

  protected override void Dispose(bool disposing)
  {
    base.Dispose(disposing);
    if (!disposing)
      return;
    this._auditApplicationReadService?.Dispose();
  }
}
