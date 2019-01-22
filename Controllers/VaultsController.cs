using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VaultsController : ControllerBase
  {

    private readonly VaultRepository _vaultRepo;
    public VaultsController(VaultRepository vaultRepo)
    {
      _vaultRepo = vaultRepo;
    }
    // GET api/Vaults
    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      return Ok(_vaultRepo.GetAll());
    }

    // GET api/Vaults/1
    [HttpGet("{vaultid}")]
    public ActionResult<Vault> Get(int id)
    {
      Vault result = _vaultRepo.GetVaultById(id);
      if (result != null)
      {
        return Ok(result);
      }
      return BadRequest();
    }

    // POST api/Vaults
    [Authorize]
    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault vault)
    {
      vault.UserId = HttpContext.User.Identity.Name;
      if (vault.UserId != null)
      {
        Vault result = _vaultRepo.NewVault(vault);
        return Created("/api/vaults/" + result.Id, result);
      }
      return Unauthorized("Login to create vault");
    }

    // PUT api/Vaults/1
    [HttpPut("{vaultid}")]
    public ActionResult<Vault> Put(int id, [FromBody] Vault vault)
    {
      if (vault.Id == 0)
      {
        vault.Id = id;
      }
      Vault result = _vaultRepo.EditVault(id, vault);
      if (result != null)
      {
        return result;
      }
      return NotFound();
    }

    // DELETE api/Vaults/1
    [Authorize]
    [HttpDelete("{vaultid}")]
    public ActionResult<string> Delete(string vaultId)
    {
      string uid = HttpContext.User.Identity.Name;
      if (uid != null)
      {
        _vaultRepo.DeleteVault(vaultId, uid);
        return Ok("Successfully deleted vault");
      }
      return BadRequest("No vault to delete");
    }
  }
}