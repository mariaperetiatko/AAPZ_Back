using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AAPZ_Backend.Models;
using AAPZ_Backend.BusinessLogic.Classes;
using AAPZ_Backend.BusinessLogic.Searching;
using Microsoft.AspNetCore.Authorization;

namespace AAPZ_Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchingController : ControllerBase
    {
        SearchWorkplaces searchWorkplaces;

        public SearchingController()
        {
            searchWorkplaces = new SearchWorkplaces();
        }

        [ProducesResponseType(typeof(List<FindedWorkplace>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("SearhcForWorcplaces")]
        public IActionResult SearcForWorcplaces([FromBody] SearchingViewModel searchingViewModel)
        {
            List<FindedWorkplace> findedWorkplaces = searchWorkplaces.PerformSearching(searchingViewModel);

            return new ObjectResult(findedWorkplaces);
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