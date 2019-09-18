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
        public ActionResult<ResponseObject<object>> GetReviewsByFilmId(string id)
        {
            return _businessware.GetByFilmId(id);
        }

        [Route("addreview/{filmid}/{username}/{rating}/{reviewBody}")]
        [HttpPost("addreview/{filmid}/{username}/{rating}/{reviewBody}")]
        public ActionResult<ResponseObject<object>> SubmitReview(string filmid, string username, string rating, string reviewBody)
        {
            return _businessware.AddReview(filmid, username, rating, reviewBody);
            
        }
    }
}