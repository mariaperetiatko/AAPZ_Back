using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAPZ_Backend.Models;
using AAPZ_Backend.BusinessLogic.Statistics;
using AAPZ_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AAPZ_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        WorkplaceStatistics workplaceStatistics;
        private ClientsWorkplaceStatistic clientsWorkplaceStatistic;
        ClientRepository clientDB;


        public StatisticsController(ClientRepository clientRepository)
        {
            workplaceStatistics = new WorkplaceStatistics();
            clientsWorkplaceStatistic = new ClientsWorkplaceStatistic();
            clientDB = clientRepository;
        }

        [ProducesResponseType(typeof(Dictionary<int, double>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetStatisticsByYear/{year}, {buildingId}")]
        public IActionResult GetStatisticsByYear(int year, int buildingId)
        {
            return new ObjectResult(workplaceStatistics.GetStatisticsByYear(year, buildingId));
        }

        [ProducesResponseType(typeof(Dictionary<int, double>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetStatisticsByMonth/{year}, {month}, {buildingId}")]
        public IActionResult GetStatisticsByMonth(int year, int month, int buildingId)
        {
            return new ObjectResult(workplaceStatistics.GetStatisticsByMonth(year, month, buildingId));
        }

        [ProducesResponseType(typeof(Dictionary<int, double>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetAverageStatisticsByWeek/{buildingId}")]
        public IActionResult GetAverageStatisticsByWeek(int buildingId)
        {
            return new ObjectResult(workplaceStatistics.GetAverageStatisticsByWeek(buildingId));
        }

        [ProducesResponseType(typeof(Dictionary<int, double>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetClientStatisticsByYear")]
        public IActionResult GetClientStatisticsByYear()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return null;

            return new ObjectResult(clientsWorkplaceStatistic.GetStatisticsByYear(client.Id));
        }

        [ProducesResponseType(typeof(Dictionary<int, double>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetClientStatisticsByMonth")]
        public IActionResult GetClientStatisticsByMonth()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return null;

            return new ObjectResult(clientsWorkplaceStatistic.GetStatisticsByMonth(client.Id));
        }

        [ProducesResponseType(typeof(Dictionary<int, double>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetClientStatisticsByWeek")]
        public IActionResult GetClientStatisticsByWeek()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
                return null;

            return new ObjectResult(clientsWorkplaceStatistic.GetStatisticsByWeek(client.Id));
        }
    }
}