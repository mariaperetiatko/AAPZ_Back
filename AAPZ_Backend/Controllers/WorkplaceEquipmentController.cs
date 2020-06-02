using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AAPZ_Backend;
using AAPZ_Backend.Repositories;
using AAPZ_Backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace AAPZ_Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WorkplaceEquipmentController : Controller
    {
        WorkplaceEquipmentRepository WorkplaceEquipmentDB;

        public WorkplaceEquipmentController()
        {
            WorkplaceEquipmentDB = new WorkplaceEquipmentRepository();
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<WorkplaceEquipment>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceEquipmentsList")]
        public IEnumerable<WorkplaceEquipment> GetWorkplaceEquipmentsList()
        {
            return WorkplaceEquipmentDB.GetEntityList();
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<WorkplaceEquipment>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceEquipmentsList/{workplaceId}")]
        public IEnumerable<WorkplaceEquipment> GetWorkplaceEquipmentByWorkplace(int workplaceId)
        {
            return WorkplaceEquipmentDB.GetWorkplaceEquipmentByWorkplace(workplaceId);
        }

        [ProducesResponseType(typeof(IEnumerable<WorkplaceEquipment>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceEquipmentByWorkplaceWithEquipment/{workplaceId}")]
        public IEnumerable<WorkplaceEquipment> GetWorkplaceEquipmentByWorkplaceWithEquipment(int workplaceId)
        {
            return WorkplaceEquipmentDB.GetWorkplaceEquipmentByWorkplaceWithEquipment(workplaceId);
        }
        
        // GET api/<controller>/5
        [ProducesResponseType(typeof(WorkplaceEquipment), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceEquipmentById/{id}")]
        public IActionResult GetWorkplaceEquipmentById(int id)
        {
            WorkplaceEquipment WorkplaceEquipment = WorkplaceEquipmentDB.GetEntity(id);
            if (WorkplaceEquipment == null)
                return NotFound();
            return new ObjectResult(WorkplaceEquipment);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(WorkplaceEquipment), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("CreateWorkplaceEquipment")]
        public IActionResult CreateWorkplaceEquipment([FromBody]WorkplaceEquipment WorkplaceEquipment)
        {
            WorkplaceEquipmentDB.Create(WorkplaceEquipment);
            return Ok(WorkplaceEquipment);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(WorkplaceEquipment), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPut("UpdateWorkplaceEquipment")]
        public IActionResult UpdateWorkplaceEquipment([FromBody]WorkplaceEquipment WorkplaceEquipment)
        {
            if (WorkplaceEquipment == null)
            {
                return BadRequest();
            }
            WorkplaceEquipmentDB.Update(WorkplaceEquipment);
            WorkplaceEquipmentDB.Save();
            return Ok(WorkplaceEquipment);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(WorkplaceEquipment), StatusCodes.Status200OK)]
        [Authorize]
        [HttpDelete("DeleteWorkplaceEquipment/{id}")]
        public IActionResult DeleteWorkplaceEquipment(int id)
        {
            WorkplaceEquipment WorkplaceEquipment = WorkplaceEquipmentDB.GetEntity(id);
            if (WorkplaceEquipment == null)
            {
                return NotFound();
            }
            WorkplaceEquipmentDB.Delete(id);
            WorkplaceEquipmentDB.Save();
            return Ok(WorkplaceEquipment);
        }
    }
}
