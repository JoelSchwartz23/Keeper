using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepRepository _vkrepo;

    public VaultKeepsController(VaultKeepRepository vkrepo)
    {
      _vkrepo = vkrepo;
    }


    [HttpGet("{id}")]
    public IEnumerable<Keep> Get(int id)
    {
      return _vkrepo.GetVaultKeepById(id);
    }

    [Authorize]
    [HttpPost]
    public ActionResult<VaultKeep> Post([FromBody] VaultKeep vk)
    {
      vk.UserId = HttpContext.User.Identity.Name;
      if (vk.UserId != null)
      {
        VaultKeep result = _vkrepo.NewVaultKeep(vk);
        return Created("Successfully created vaultkeep", result);
      }
      return BadRequest("cannot make vaultkeep");
    }

    [HttpPut]
    public ActionResult<string> DeleteVaultKeep([FromBody]VaultKeep vaultKeep)
    {
      var result = _vkrepo.DeleteVaultKeep(vaultKeep);
      if (result != false)
      {
        return Ok("deleted vaultkeep");
      }
      return BadRequest("no vaultkeep to delete");
    }
  }
}