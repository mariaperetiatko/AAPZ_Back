using System;
using System.Collections.Generic;
using System.Linq;
using AAPZ_Backend.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAPZ_Backend;
using AAPZ_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AAPZ_Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/Workplace")]
    public class WorkplaceController : Controller
    {
        IDBActions<Workplace> WorkplaceDB;

        public WorkplaceController()
        {
            WorkplaceDB = new WorkplaceRepository();
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<Workplace>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplacesList")]
        public IEnumerable<Workplace> GetWorkplacesList()
        {
            return WorkplaceDB.GetEntityList();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(Workplace), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceById/{id}")]
        public IActionResult GetWorkplaceById(int id)
        {
            Workplace Workplace = WorkplaceDB.GetEntity(id);
            if (Workplace == null)
                return NotFound();
            return new ObjectResult(Workplace);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(Workplace), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateWorkplace")]
        public IActionResult CreateWorkplace([FromBody]Workplace Workplace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            WorkplaceDB.Create(Workplace);
            WorkplaceDB.Save();
            return Ok(Workplace);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(Workplace), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateWorkplace")]
        public IActionResult UpdateWorkplace([FromBody]Workplace Workplace)
        {
            if (Workplace == null)
            {
                return BadRequest();
            }
            WorkplaceDB.Update(Workplace);
            WorkplaceDB.Save();
            return Ok(Workplace);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(Workplace), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteWorkplace/{id}")]
        public IActionResult DeleteWorkplace(int id)
        {
            Workplace Workplace = WorkplaceDB.GetEntity(id);
            if (Workplace == null)
            {
                return NotFound();
            }
            WorkplaceDB.Delete(id);
            WorkplaceDB.Save();
            return Ok(Workplace);
        }
    }
}
