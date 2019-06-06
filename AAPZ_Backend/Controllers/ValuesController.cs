using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;using AAPZ_Backend.Models;

namespace AAPZ_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private SheringDBContext sheringDBContext;

        public ValuesController()
        {
            sheringDBContext = new SheringDBContext();
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Distance dist = sheringDBContext.Diastance.Last();
            return new string[] { dist.DistanceValue.ToString(), "value2" };
        }

        // GET api/values
        [HttpGet("h")]
        public ActionResult<string> h()
        {
            return "Backend works";
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(double id)
        {
            Distance distance = new Distance()
            {
                DistanceValue = id,
                Date = DateTime.Now
            };
            sheringDBContext.Diastance.Add(distance);
            sheringDBContext.SaveChanges();
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
