using System;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentFilter _businessware;
        
        public CommentsController(CommentFilter businessware)
        {
            _businessware = businessware;
        }

        [Route("getcomments/{reviewId}")]
        [HttpGet("reviewId")]
        public ActionResult<ResponseObject<object>> GetComments(string reviewId)
        {
            return _businessware.GetComments(reviewId);
        }

        [Route("addcomment/{reviewId}/{username}/{body}")]
        [HttpPost("addcomment/{reviewId}/{username}/{body}")]
        public ActionResult<ResponseObject<object>> AddComment(string reviewId, string username, string body)
        {
            return _businessware.AddComment(reviewId, username, body);
        }
    }
}