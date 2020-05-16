using System;
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
    public class MonitoringController : Controller
    {
        private MonitoringRepository db;
        ClientRepository clientDB;

        public MonitoringController(ClientRepository clientRepository)
        {
            db = new MonitoringRepository();
            clientDB = clientRepository;
        }

        [ProducesResponseType(typeof(Monitoring), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetMonitoringByDate/{date}")]
        public IActionResult GetMonitoringByDate(DateTime date)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return NotFound();
            db.GenerateMonitoring(client.Id);

            return Ok(db.GetByDate(date, client.Id));
        }

        [ProducesResponseType(typeof(IEnumerable<Monitoring>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetMonitoringList")]
        public IActionResult GetMonitoringList()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return NotFound();
            db.GenerateMonitoring(client.Id);

            return Ok(db.GetList(client.Id));
        }
    }
}
