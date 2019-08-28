using System;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly ReviewFilter _businessware;

        public ReviewsController(ReviewFilter businessware)
        {
            _businessware = businessware;
        }

        [Route("getreviews/{id}")]
        [HttpGet("id")]
        public ActionResult<ResponseObject<Object>> GetReviewsByFilmId(string id)
        {
            return _businessware.GetByFilmId(id);
        }
    }
}