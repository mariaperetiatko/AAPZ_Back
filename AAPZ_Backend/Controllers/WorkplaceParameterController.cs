using System.Collections.Generic;
using AAPZ_Backend.Models;
using AAPZ_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAPZ_Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WorkplaceParameterController : Controller
    {
        IDBActions<WorkplaceParameter> WorkplaceParameterDB;
        ClientRepository clientDB;

        public WorkplaceParameterController(ClientRepository clientRepository)
        {
            WorkplaceParameterDB = new WorkplaceParameterRepository();
            clientDB = clientRepository;
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<WorkplaceParameter>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetClientsWorkplaceParameters")]
        public IEnumerable<WorkplaceParameter> GetClientsWorkplaceParameters()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if(client != null)
                return WorkplaceParameterDB.GetEntityListByClientId(client.Id); ;
            return null;
        }

        [ProducesResponseType(typeof(IEnumerable<WorkplaceParameter>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("CreateWorkplaceParameter")]
        public IActionResult CreateWorkplaceParameter([FromBody]WorkplaceParameter workplaceParameter)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client != null)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                workplaceParameter.ClientId = client.Id;
              
                WorkplaceParameterDB.Create(workplaceParameter);

                return Ok(workplaceParameter);
            }

            return NotFound();
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(WorkplaceParameter), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPut("UpdateWorkplaceParameter")]
        public IActionResult UpdateWorkplaceParameter([FromBody]WorkplaceParameter workplaceParameter)
        {
            if (workplaceParameter == null)
            {
                return BadRequest();
            }
            WorkplaceParameterDB.Update(workplaceParameter);
            WorkplaceParameterDB.Save();
            return Ok(workplaceParameter);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(WorkplaceParameter), StatusCodes.Status200OK)]
        [Authorize]
        [HttpDelete("DeleteWorkplaceParameter/{id}")]
        public IActionResult DeleteWorkplaceParameter(int id)
        {
            WorkplaceParameter workplaceParameter = WorkplaceParameterDB.GetEntity(id);
            if (workplaceParameter == null)
            {
                return NotFound();
            }
            WorkplaceParameterDB.Delete(id);
            WorkplaceParameterDB.Save();
            return Ok(workplaceParameter);
        }
    }
}
