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
  public class KeepsController : ControllerBase
  {

    private readonly KeepRepository _keepRepo;
    public KeepsController(KeepRepository keepRepo)
    {
      _keepRepo = keepRepo;
    }
    // GET api/Keeps
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      return Ok(_keepRepo.GetAll());
    }

    // GET api/Keeps/1
    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      Keep result = _keepRepo.GetKeepById(id);
      if (result != null)
      {
        return Ok(result);
      }
      return BadRequest();
    }

    // POST api/Keeps
    [HttpPost]
    public ActionResult<Keep> Post([FromBody] Keep keep)
    {
      Keep result = _keepRepo.AddKeep(keep);
      return Created("/api/keeps/" + result.Id, result);
    }

    // PUT api/Keeps/1
    [HttpPut("{id}")]
    public ActionResult<Keep> Put(int id, [FromBody] Keep keep)
    {
      if (keep.Id == 0)
      {
        keep.Id = id;
      }
      Keep result = _keepRepo.EditKeep(id, keep);
      if (result != null)
      {
        return result;
      }
      return NotFound();
    }

    // DELETE api/Keeps/1
    [HttpDelete("{id}")]
    public ActionResult<Keep> Delete(int id)
    {
      if (_keepRepo.DeleteKeep(id))
      {
        return Ok("Successfully deleted keep");
      }
      return BadRequest("No  to delete");
    }
  }
}