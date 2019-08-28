using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;

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
        public ActionResult<ResponseObject<Object>> GetComments(string reviewId)
        {
            return _businessware.GetComments(reviewId);
        }
    }
}