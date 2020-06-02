using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAPZ_Backend.Repositories;
using AAPZ_Backend.BusinessLogic.Ordering;
using AAPZ_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing.Constraints;

namespace AAPZ_Backend.Controllers
{
    public class DateFilter
    {
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public string Like { get; set; }
    }

    public class FilteredPagedResult
    {
        public IEnumerable<WorkplaceOrder> WorkplaceOrders { get; set; }
        public int TotalCount { get; set; }
    }

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
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return null;
            return WorkplaceOrderDB.GetEntityListByClientId(client.Id);
        }

        [ProducesResponseType(typeof(FilteredPagedResult), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetFilteredWorkplaceOrdersListByClient/{pageNumber}")]
        public FilteredPagedResult GetFilteredWorkplaceOrdersListByClient([FromQuery]DateFilter filter, int pageNumber)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return null;

            int take = 3;
            int skip = (pageNumber - 1) * take;

            IEnumerable<WorkplaceOrder> workplaceOrders;
            int totalCount = 0;
            string likeString = (String.IsNullOrEmpty(filter.Like)) ? null : filter.Like;
            if (filter == null || (filter.StartTime == null && filter.FinishTime == null))
            {
                workplaceOrders = WorkplaceOrderDB.GetCurrentWorkplaceOrdersByClient(client.Id, skip, take);
                totalCount = WorkplaceOrderDB.GetCurrentWorkplaceOrdersByClientCount(client.Id);
            }
            else if (filter.StartTime == null)
            {
                workplaceOrders = WorkplaceOrderDB.GetPreviousWorkplaceOrdersByClient
                    ((DateTime) filter.FinishTime, client.Id, skip, take);
                totalCount =
                    WorkplaceOrderDB.GetPreviousWorkplaceOrdersByClientCount((DateTime) filter.FinishTime, client.Id);
            }
            else if (filter.FinishTime == null)
            {
                
                workplaceOrders = WorkplaceOrderDB.GetFutureWorkplaceOrdersByClient
                    ((DateTime)filter.StartTime, client.Id, skip, take, likeString);
                totalCount =
                    WorkplaceOrderDB.GetFutureWorkplaceOrdersByClientCount((DateTime) filter.StartTime, client.Id, likeString);
            }
            else 
            {
                workplaceOrders = WorkplaceOrderDB.GetFilteredWorkplaceOrdersByClient
                    ((DateTime)filter.StartTime, (DateTime)filter.FinishTime, client.Id, skip, take, likeString);
                totalCount = WorkplaceOrderDB.GetFilteredWorkplaceOrdersByClientCount
                    ((DateTime) filter.StartTime, (DateTime) filter.FinishTime, client.Id, likeString);
            }

            double pageDecimal = (double) totalCount / take;
            int pageCount = totalCount / take;

            if (pageDecimal - (double) pageCount != 0.0)
                pageCount++;
               
            return new FilteredPagedResult
            {
                WorkplaceOrders = workplaceOrders,
                TotalCount = pageCount
            };

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
        [HttpGet("GetPreviousWorkplaceOrdersByWorkplace/{workplaceId}")]
        public IEnumerable<WorkplaceOrder> GetPreviousWorkplaceOrdersByWorkplace(int workplaceId)
        {           
            return WorkplaceOrderDB.GetPreviousWorkplaceOrdersByWorkplace(workplaceId);
        }

        [ProducesResponseType(typeof(IEnumerable<WorkplaceOrder>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetFutureWorkplaceOrdersByWorkplace/{workplaceId}")]
        public IEnumerable<WorkplaceOrder> GetFutureWorkplaceOrdersByWorkplace(int workplaceId)
        {
            return WorkplaceOrderDB.GetFutureWorkplaceOrdersByWorkplace(workplaceId);
        }

        [ProducesResponseType(typeof(IEnumerable<WorkplaceOrder>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetWorkplaceOrdersByWorkplaceAndDate/{workplaceId}/{date}")]
        public IEnumerable<WorkplaceOrder> GetWorkplaceOrdersByWorkplaceAndDate(int workplaceId, DateTime date)
        {
            return WorkplaceOrderDB.GetWorkplaceOrdersByWorkplaceAndDate(workplaceId, date);
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
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return NotFound();

            WorkplaceOrder.ClientId = client.Id;
            if (OrderWorkplace.isFree(WorkplaceOrder.ClientId, WorkplaceOrder.WorkplaceId,
                WorkplaceOrder.StartTime, WorkplaceOrder.FinishTime))
            {
                WorkplaceOrder.SumToPay = OrderWorkplace.CreateOrder(WorkplaceOrder.ClientId, WorkplaceOrder.WorkplaceId,
                WorkplaceOrder.StartTime, WorkplaceOrder.FinishTime);

                WorkplaceOrderDB.Create(WorkplaceOrder);
                WorkplaceOrderDB.Save();
                WorkplaceOrder.Client =null;
                WorkplaceOrder.Workplace = null;

                return Ok(WorkplaceOrder);

            }
            return BadRequest();
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
