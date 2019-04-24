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
    [Route("api/Landlord")]
    public class LandlordController : Controller
    {
        IDBActions<Landlord> LandlordDB;

        public LandlordController()
        {
            LandlordDB = new LandlordRepository();
        }

        // GET: api/<controller>
        [Authorize]
        [HttpGet("GetLandlordsList")]
        public IEnumerable<Landlord> GetLandlordsList()
        {
            return LandlordDB.GetEntityList();
        }

        // GET api/<controller>/5
        [Authorize]
        [HttpGet("GetLandlordById/{id}")]
        public IActionResult GetLandlordById(int id)
        {
            Landlord Landlord = LandlordDB.GetEntity(id);
            if (Landlord == null)
                return NotFound();
            return new ObjectResult(Landlord);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin")]
        [HttpPost("CreateLandlord")]
        public IActionResult CreateLandlord([FromBody]Landlord Landlord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            LandlordDB.Create(Landlord);
            LandlordDB.Save();
            return Ok(Landlord);
        }

        // PUT api/<controller>
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateLandlord")]
        public IActionResult UpdateLandlord([FromBody]Landlord Landlord)
        {
            if (Landlord == null)
            {
                return BadRequest();
            }
            LandlordDB.Update(Landlord);
            LandlordDB.Save();
            return Ok(Landlord);
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteLandlord/{id}")]
        public IActionResult DeleteLandlord(int id)
        {
            Landlord Landlord = LandlordDB.GetEntity(id);
            if (Landlord == null)
            {
                return NotFound();
            }
            LandlordDB.Delete(id);
            LandlordDB.Save();
            return Ok(Landlord);
        }
    }
}
