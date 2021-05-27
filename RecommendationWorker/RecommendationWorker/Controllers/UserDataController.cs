using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using RecommendationWorker.Models;
using RecommendationWorker.Serivces;
using RecommendationWorker.Serivces.Interfaces;

namespace RecommendationWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataService _userDataService;

        public UserDataController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet]
        public ActionResult<List<DataLayer>> Get()
        {
            try
            {
                List<DataLayer> datalayer = _userDataService.GetDataLayer();
                return Ok(datalayer);
            }
            catch (Exception e)
            {
                return StatusCode(500, e); 
            }
        }

        [HttpGet("{id}")]
        public ActionResult<List<DataLayer>> GetByUserId(string id)
        {
            if (id != null || id != "")
            {
                return _userDataService.GetDataLayerByUserId(id);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<string> Create([FromBody]DataLayer dataLayer)
        {
            return _userDataService.InsertDataLayer(dataLayer).Id;
        }
    }
}
