using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewsController : Controller
    {

        private readonly ReviewFilter _businessware;

        public ReviewsController(ReviewFilter businessware)
        {
            _businessware = businessware;
        }

        [Route("getreviews/{id}")]
        [HttpGet]
        public ActionResult<ResponseObject<object>> GetReviewsByFilmId(string id)
        {
            return _businessware.GetByFilmId(id);
        }

        [Route("addreview")]
        [HttpPost]
        public ActionResult<ResponseObject<object>> SubmitReview([FromBody] Review review)
        {
            return _businessware.AddReview(review);
            
        }
    }
}