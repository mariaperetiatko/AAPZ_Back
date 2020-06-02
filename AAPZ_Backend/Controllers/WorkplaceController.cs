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
    public class WorkplacePagedResult
    {
        public IEnumerable<Workplace> Workplaces { get; set; }
        public int TotalCount { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Workplace")]
    public class WorkplaceController : Controller
    {
        WorkplaceRepository WorkplaceDB;

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


        [ProducesResponseType(typeof(WorkplacePagedResult), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetPagedWorkplacesByBuildingId/{buildingId}/{pageNumber}")]
        public WorkplacePagedResult GetPagedWorkplacesByBuildingId(int buildingId, int pageNumber)
        {
            int take = 3;
            int skip = (pageNumber - 1) * take;

            IEnumerable<Workplace> workplaces = WorkplaceDB.GetPagedWorkplacesByBuildingId(buildingId, skip, take);
            int totalCount = WorkplaceDB.GetWorkplacesByBuildingIdCount(buildingId);
            

            double pageDecimal = (double)totalCount / take;
            int pageCount = totalCount / take;

            if (pageDecimal - (double)pageCount != 0.0)
                pageCount++;

            return new WorkplacePagedResult
            {
                Workplaces = workplaces,
                TotalCount = pageCount
            };

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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
