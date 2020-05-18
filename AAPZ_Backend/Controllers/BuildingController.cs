using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAPZ_Backend.Repositories;
using AAPZ_Backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace AAPZ_Backend.Controllers
{
    public class FilteredPagedBuildingsResult
    {
        public IEnumerable<Building> Buildings { get; set; }
        public int TotalCount { get; set; }
    }

    [Produces("application/json")]
    [Route("api/Building")]
    public class BuildingController : Controller
    {
        BuildingRepository BuildingDB;
        LandlordRepository LandlordDB;

        public BuildingController()
        {
            BuildingDB = new BuildingRepository();
            LandlordDB = new LandlordRepository();
        }

        [Authorize]
        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<Building>), StatusCodes.Status200OK)]
        [HttpGet("GetBuildingsList")]
        public IEnumerable<Building> GetBuildingsList()
        {
            return BuildingDB.GetEntityList();
        }

        [Authorize]
        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<Building>), StatusCodes.Status200OK)]
        [HttpGet("GetBuildingsByLandlord")]
        public IEnumerable<Building> GetBuildingsByLandlord()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Landlord landlord = LandlordDB.GetCurrentLandlord(userJWTId);
            if (landlord == null)
            {
                return null;
            }

            //int take = 10;
            //int skip = (pageNumber - 1) * take;
            //int totalCount = BuildingDB.GetBuildingsCountByLandlord(landlord.Id);

            return BuildingDB.GetBuildingsByLandlord(landlord.Id);
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetBuildingById/{id}")]
        public IActionResult GetBuildingById(int id)
        {
            Building Building = BuildingDB.GetEntity(id);
            if (Building == null)
                return NotFound();
            return new ObjectResult(Building);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("CreateBuilding")]
        [Authorize]
        public IActionResult CreateBuilding([FromBody]Building Building)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Landlord landlord = LandlordDB.GetCurrentLandlord(userJWTId);
            if (landlord == null)
            {
                return NotFound();
            }

            Building.LandlordId = landlord.Id;
            BuildingDB.Create(Building);
            return Ok(Building);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPut("UpdateBuilding")]
        public IActionResult UpdateBuilding([FromBody]Building Building)
        {
            if (Building == null)
            {
                return BadRequest();
            }
            BuildingDB.Update(Building);
            return Ok(Building);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(Building), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteBuilding/{id}")]
        public IActionResult DeleteBuilding(int id)
        {
            Building Building = BuildingDB.GetEntity(id);
            if (Building == null)
            {
                return NotFound();
            }
            BuildingDB.Delete(id);
            return Ok(Building);
        }
    }
}
