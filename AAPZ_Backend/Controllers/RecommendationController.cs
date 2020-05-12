using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AAPZ_Backend.BusinessLogic.Recomendations;
using AAPZ_Backend.Models;
using AAPZ_Backend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAPZ_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        ClientRepository clientDB;

        public RecommendationController(ClientRepository repository)
        {
            clientDB = repository;
        }

        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("CalculateRecommendedTableHeight/{clientId}")]
        public IActionResult CalculateRecommendedTableHeight(int clientId)
        {
            //Client client = clientDB.GetEntity(clientId);
            //if (client == null)
                //return NotFound();
            return new ObjectResult
                (
                HealthCareRecomendations.CalculateRecommendedTableHeight((double)clientId)
                );
        }

        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPost("CalculateRecommendedChairHeight/{clientId}")]
        public IActionResult CalculateRecommendedChairHeight(int clientId)
        {
            //Client client = clientDB.GetEntity(clientId);
            //if (client == null)
                //return NotFound();
            return new ObjectResult
                (
                HealthCareRecomendations.CalculateRecommendedChairHeight((double)clientId)
                );
        }
    }
}