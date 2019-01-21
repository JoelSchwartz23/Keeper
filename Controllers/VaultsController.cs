using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
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
    [HttpGet("{id}")]
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
    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault vault)
    {
      Vault result = _vaultRepo.AddVault(vault);
      return Created("/api/vaults/" + result.Id, result);
    }

    // PUT api/Vaults/1
    [HttpPut("{id}")]
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
    [HttpDelete("{id}")]
    public ActionResult<Vault> Delete(int id)
    {
      if (_vaultRepo.DeleteVault(id))
      {
        return Ok("Successfully deleted vault");
      }
      return BadRequest("No vault to delete");
    }
  }
}