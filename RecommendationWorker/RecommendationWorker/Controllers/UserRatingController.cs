using Microsoft.AspNetCore.Mvc;
using RecommendationWorker.Models;
using RecommendationWorker.Serivces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRatingController : ControllerBase
    {
        private readonly IUserRatingService _userRatingService;

        public UserRatingController(IUserRatingService userRatingService)
        {
            _userRatingService = userRatingService;
        }

        [HttpGet("{id}")]
        public ActionResult<List<UserRating>> GetUserRatingsById(string id)
        {
            if (id != null || id != "")
            {
                return _userRatingService.GetUserRatingById(id);
            }
            return NotFound();
        }
    }
}
