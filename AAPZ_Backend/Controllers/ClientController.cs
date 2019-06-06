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
    [Route("api/Client")]
    public class ClientController : Controller
    {
        ClientRepository clientDB;

        public ClientController(ClientRepository repository)
        {
            clientDB = repository;
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        //[Authorize]
        [HttpGet("GetClientsList")]
        public IEnumerable<Client> GetClientsList()
        {
            return clientDB.GetEntityList();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetClientById/{id}")]
        public IActionResult GetClientById(int id)
        {
           // string userJWTId = User.FindFirst("id")?.Value;
            //Client client = clientDB.GetCurrentClient(id);
            //if (client == null)
            //{
               Client client = clientDB.GetEntity(id);
                if (client == null)
                    return NotFound();
            //}
            return new ObjectResult(client);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [Authorize(Roles = "Member")]
        [HttpPost("CreateClient")]
        public IActionResult CreateClient([FromBody]Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            clientDB.Create(client);
            return Ok(client);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPut/*("UpdateClient")*/]
        public IActionResult UpdateClient([FromBody]Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            //string userJWTId = User.FindFirst("id")?.Value;
            //Client currentClient = clientDB.GetCurrentClient(userJWTId);
            //if (currentClient != null)
            //{
            //    client.Id = currentClient.Id;
            //   // client.IdentityId = currentClient.IdentityId;                 
            //}

            clientDB.Update(client);
            return Ok(client);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [Authorize]
        [HttpDelete("DeleteClient/{id?}")]
        public IActionResult DeleteClient(int id)
        {
            //string userJWTId = User.FindFirst("id")?.Value;
            //Client client = clientDB.GetCurrentClient(userJWTId);
            //if (client == null)
            //{
               Client client = clientDB.GetEntity(id);
                if (client == null)
                {
                    return NotFound();
                }
            //}
            
            clientDB.Delete(client.Id);
            return Ok(client);
        }
    }
}
