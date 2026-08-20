// Decompiled with JetBrains decompiler
// Type: TSS.Audit.WebApi.Controllers.MasterDataController
// Assembly: TSS.Audit.WebApi, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9CA87E92-3DDD-413F-AD9B-DAF8395B6F1D
// Assembly location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.dll
// XML documentation location: D:\Baufest\GIT\TSS.Audit.WebApi\TSS.Audit.WebApi.xml

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TSS.Audit.Common;
using TSS.Audit.DTOs.Response;

#nullable disable
namespace TSS.Audit.WebApi.Controllers;

[Authorize]
[Route("api/master-data")]
public class MasterDataController : BaseController
{
  /// <summary>Get actions available.</summary>
  /// <returns></returns>
  [HttpGet]
  [Route("actions")]
  [ProducesResponseType(typeof (IList<TSS.Audit.DTOs.Response.TableOperation>), 200)]
  [ProducesResponseType(typeof (OperationResponse), 400)]
  [ProducesResponseType(typeof (OperationResponse), 404)]
  [ProducesResponseType(typeof (OperationResponse), 500)]
  public IActionResult GetActions()
  {
    return (IActionResult) this.Ok((object) new List<TSS.Audit.DTOs.Response.TableOperation>()
    {
      new TSS.Audit.DTOs.Response.TableOperation(Constants.TableOperation.Delete),
      new TSS.Audit.DTOs.Response.TableOperation(Constants.TableOperation.Insert),
      new TSS.Audit.DTOs.Response.TableOperation(Constants.TableOperation.Update)
    }.OrderBy<TSS.Audit.DTOs.Response.TableOperation, string>((Func<TSS.Audit.DTOs.Response.TableOperation, string>) (x => x.Name)));
  }
}
