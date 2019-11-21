using System;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly CommentFilter _businessware;
        
        public CommentsController(CommentFilter businessware)
        {
            _businessware = businessware;
        }

        [Route("getcomments/{reviewId}")]
        [HttpGet]
        public ActionResult<ResponseObject<object>> GetComments(string reviewId)
        {
            return _businessware.GetComments(reviewId);
        }

        [Route("addcomment")]
        [HttpPost]
        public ActionResult<ResponseObject<object>> AddComment([FromBody] Comment comment)
        {
            return _businessware.AddComment(comment);
        }
    }
}