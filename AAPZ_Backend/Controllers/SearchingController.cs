using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAPZ_Backend.Models;
using AAPZ_Backend.BusinessLogic.Searching;
using AAPZ_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AAPZ_Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchingController : ControllerBase
    {
        ClientRepository clientDB;

        public SearchingController(ClientRepository clientRepository)
        {
            clientDB = clientRepository;
        }

        [ProducesResponseType(typeof(List<BuildingSearchingResult>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetBuildingSearchingResults/{latitude}/{longitude}")]
        public IActionResult GetBuildingSearchingResults(double latitude, double longitude)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
            {
                return NotFound();
            }

            SearchWorkplaces sw = new SearchWorkplaces(latitude, longitude, client.Id);
            return new ObjectResult(sw.GetSearchingResult());
        }

        [ProducesResponseType(typeof(BuildingSearchingResult), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetAppropriationByBuildingResults/{buildingId}")]
        public IActionResult GetAppropriationByBuildingResults(long buildingId)
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client == null)
            {
                return NotFound();
            }

            SearchWorkplaces sw = new SearchWorkplaces(client.Id);
            return new ObjectResult(sw.GetAppropriationByBuildingResults(buildingId));
        }

        /*  [ProducesResponseType(typeof(FindedWorkplace), StatusCodes.Status200OK)]
          [Authorize]
          [HttpPost("GetAppropriationPercentage")]
          public IActionResult GetAppropriationPercentage([FromBody] SearchingViewModel searchingViewModel, Workplace workplace)
          {
              //List<FindedWorkplace> findedWorkplaces = searchWorkplaces.PerformSearching(searchingViewModel);

              return new ObjectResult(searchWorkplaces.GetAppropriationPercentage(searchingViewModel.SearchingModel,
                  workplace, searchingViewModel.WantedCost));
          }*/

    }
}