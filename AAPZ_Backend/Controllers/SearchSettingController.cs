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
    public class SearchSettingController : Controller
    {
        IDBActions<SearchSetting> SearchSettingDB;
        ClientRepository clientDB;


        public SearchSettingController(ClientRepository clientRepository)
        {
            SearchSettingDB = new SearchSettingRepository();
            clientDB = clientRepository;
        }

        // GET: api/<controller>
        [ProducesResponseType(typeof(IEnumerable<SearchSetting>), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetSearchSettingList")]
        public IEnumerable<SearchSetting> GetSearchSettingList()
        {
            return SearchSettingDB.GetEntityList();
        }

        // GET api/<controller>/5
        [ProducesResponseType(typeof(SearchSetting), StatusCodes.Status200OK)]
        [Authorize]
        [HttpGet("GetSearchSetting")]
        public IActionResult GetSearchSetting()
        {
            string userJWTId = User.FindFirst("id")?.Value;
            Client client = clientDB.GetCurrentClient(userJWTId);
            if (client != null)
            {
                SearchSetting searchSetting = SearchSettingDB.GetEntity(client.Id);

                if (searchSetting != null)
                    return Ok(searchSetting);

                SearchSetting defaultSearchSetting = new SearchSetting
                {
                    SearchSettingId = client.Id,
                    Radius = 500,
                    WantedCost = 100
                };

                SearchSettingDB.Create(defaultSearchSetting);

                return Ok(defaultSearchSetting);
            }

            return NotFound();
        }

        // POST api/<controller>
        [ProducesResponseType(typeof(SearchSetting), StatusCodes.Status200OK)]
        // [Authorize(Roles = "Admin")]
        [HttpPost("CreateSearchSetting")]
        public IActionResult CreateEquipment([FromBody]SearchSetting searchSetting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            SearchSettingDB.Create(searchSetting);
            SearchSettingDB.Save();
            return Ok(searchSetting);
        }

        // PUT api/<controller>
        [ProducesResponseType(typeof(SearchSetting), StatusCodes.Status200OK)]
        [Authorize]
        [HttpPut("UpdateSearchSetting")]
        public IActionResult UpdateSearchSetting([FromBody]SearchSetting searchSetting)
        {
            if (searchSetting == null)
            {
                return BadRequest();
            }
            SearchSettingDB.Update(searchSetting);
            return Ok(searchSetting);
        }

        // DELETE api/<controller>/5
        [ProducesResponseType(typeof(SearchSetting), StatusCodes.Status200OK)]
        [Authorize]
        [HttpDelete("DeleteSearchSetting/{id}")]
        public IActionResult DeleteEquipment(int id)
        {
            SearchSetting searchSetting = SearchSettingDB.GetEntity(id);
            if (searchSetting == null)
            {
                return NotFound();
            }
            SearchSettingDB.Delete(id);
            return Ok(searchSetting);
        }
    }
}
