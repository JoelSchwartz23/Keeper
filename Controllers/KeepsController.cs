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
  public class KeepsController : ControllerBase
  {

    private readonly KeepRepository _keepRepo;
    public KeepsController(KeepRepository keepRepo)
    {
      _keepRepo = keepRepo;
    }
    // GET api/Keeps
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> GetAll()
    {
      return Ok(_keepRepo.GetAll());
    }

    // GET api/Keeps/1
    [Authorize]
    [HttpGet("user")]
    public IEnumerable<Keep> Get()
    {
      string uid = HttpContext.User.Identity.Name;
      return _keepRepo.GetByUserId(uid);
    }

    // POST api/Keeps
    [Authorize]
    [HttpPost]
    public ActionResult<Keep> Post([FromBody] Keep keep)
    {
      keep.UserId = HttpContext.User.Identity.Name;
      if (keep.UserId != null)
      {
        Keep result = _keepRepo.NewKeep(keep);
        return Created("/api/keeps/" + result.Id, result);
      }
      return Unauthorized("Login to create keep");
    }

    // PUT api/Keeps/1
    [HttpPut("{keepid}")]
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
    [Authorize]
    [HttpDelete("{keepid}")]
    public ActionResult<Keep> Delete(string keepId)
    {
      string uid = HttpContext.User.Identity.Name;
      if (uid != null)
      {
        _keepRepo.DeleteKeep(keepId, uid);
        return Ok("Successfully deleted keep");
      }
      return BadRequest("No keep to delete");
    }
  }
}