using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAPZ_Backend.Repositories;
using AAPZ_Backend.BusinessLogic.Ordering;
using AAPZ_Backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace AAPZ_Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/WorkplaceOrder")]
    public class WorkplaceOrderController : Controller
    {
        WorkplaceOrderRepository WorkplaceOrderDB;
        OrderWorkplace OrderWorkplace;
        ClientRepository clientDB;

        public WorkplaceOrderController(ClientRepository clientRepository)
        {
            WorkplaceOrderDB = new WorkplaceOrderRepository();
            OrderWorkplace = new OrderWorkplace(clientRepository);
            clientDB = clientRepository;            
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<WorkplaceOrder>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceOrdersList")]
        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersList()
        {

            return WorkplaceOrderDB.GetEntityList();
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<WorkplaceOrder>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceOrdersListByClient/{clientId}")]
        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersListByClient(int clientId)
        {
            //string userJWTId = User.FindFirst("id")?.Value;
            //Client client = clientDB.GetCurrentClient(userJWTId);
           // if(client != null)
                //return WorkplaceOrderDB.GetEntityList().Where(x => x.ClientId == client.Id);
           // if(clientId != null)
                return WorkplaceOrderDB.GetEntityListByClientId(clientId);
           // else return null;
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<WorkplaceOrder>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceOrdersByWorkplaceId/{workplaceId}")]
        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersByWorkplaceId(int workplaceId)
        {
            //string userJWTId = User.FindFirst("id")?.Value;
            //Client client = clientDB.GetCurrentClient(userJWTId);
            // if(client != null)
            //return WorkplaceOrderDB.GetEntityList().Where(x => x.ClientId == client.Id);
            // if(clientId != null)
            return WorkplaceOrderDB.GetWorkplaceOrdersByWorkplaceId(workplaceId);
          // else return null;
        }

        [ProducesResponseType(typeof(IEnumerable<WorkplaceOrder>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceOrdersByWorkplaceAndDate/{workplaceId}/{date}")]
        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersByWorkplaceAndDate(int workplaceId, DateTime date)
        {
            //string userJWTId = User.FindFirst("id")?.Value;
            //Client client = clientDB.GetCurrentClient(userJWTId);
            // if(client != null)
            //return WorkplaceOrderDB.GetEntityList().Where(x => x.ClientId == client.Id);
            // if(clientId != null)
            return WorkplaceOrderDB.GetWorkplaceOrdersByWorkplaceAndDate(workplaceId, date);
            // else return null;
        }
        // GET api/<controller>/5
        [ProducesResponseType(typeof(WorkplaceOrder), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceOrderById/{id}")]
        public IActionResult GetWorkplaceOrderById(int id)
        {
            WorkplaceOrder WorkplaceOrder = WorkplaceOrderDB.GetEntity(id);
            if (WorkplaceOrder == null)
                return NotFound();
            return new ObjectResult(WorkplaceOrder);
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(WorkplaceOrder), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("CreateWorkplaceOrder")]
        public IActionResult CreateWorkplaceOrder([FromBody]WorkplaceOrder WorkplaceOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            
            if(OrderWorkplace.isFree(WorkplaceOrder.ClientId, WorkplaceOrder.WorkplaceId,
                WorkplaceOrder.StartTime, WorkplaceOrder.FinishTime))
            {
                WorkplaceOrder.SumToPay = (int)OrderWorkplace.CreateOrder(WorkplaceOrder.ClientId, WorkplaceOrder.WorkplaceId,
                WorkplaceOrder.StartTime, WorkplaceOrder.FinishTime);

                WorkplaceOrderDB.Create(WorkplaceOrder);
                WorkplaceOrderDB.Save();
                WorkplaceOrder.Client =null;
                WorkplaceOrder.Workplace = null;

                return Ok(WorkplaceOrder);

            }
            return Ok("Busy");
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(WorkplaceOrder), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPut("UpdateWorkplaceOrder")]
        public IActionResult UpdateWorkplaceOrder([FromBody]WorkplaceOrder WorkplaceOrder)
        {
            if (WorkplaceOrder == null)
            {
                return BadRequest();
            }
            WorkplaceOrderDB.Update(WorkplaceOrder);
            WorkplaceOrderDB.Save();
            return Ok(WorkplaceOrder);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(WorkplaceOrder), StatusCodes.Status200OK)]
        [Authorize]
        [HttpDelete("DeleteWorkplaceOrder/{id}")]
        public IActionResult DeleteWorkplaceOrder(int id)
        {
            WorkplaceOrder WorkplaceOrder = WorkplaceOrderDB.GetEntity(id);
            if (WorkplaceOrder == null)
            {
                return NotFound();
            }
            WorkplaceOrderDB.Delete(id);
            WorkplaceOrderDB.Save();
            return Ok(WorkplaceOrder);
        }
    }
}
