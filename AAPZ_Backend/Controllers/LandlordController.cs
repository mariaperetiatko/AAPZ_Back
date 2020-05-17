using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAPZ_Backend.Repositories;
using AAPZ_Backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace AAPZ_Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/Landlord")]
    public class LandlordController : Controller
    {
        LandlordRepository LandlordDB;

        public LandlordController()
        {
            LandlordDB = new LandlordRepository();
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<Landlord>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetLandlordsList")]
        public IEnumerable<Landlord> GetLandlordsList()
        {
            return LandlordDB.GetEntityList();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(Landlord), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetLandlordById/{id}")]
        public IActionResult GetLandlordById(int id)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Landlord landlord = LandlordDB.GetCurrentLandlord(userJWTId);
            if (landlord == null)
            {
                //Client client = clientDB.GetEntity(id);
                //if (client == null)
                return NotFound();
            }
          
            return new ObjectResult(landlord);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(Landlord), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(Landlord), StatusCodes.Status200OK)]
        [Authorize]
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
        [ProducesResponseType(typeof(Landlord), StatusCodes.Status200OK)]
        [Authorize]
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
