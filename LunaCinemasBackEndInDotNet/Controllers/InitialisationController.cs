using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace LunaCinemasBackEndInDotNet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InitialisationController : Controller
    {
        private readonly InitialisationHandler _businessware;

        public InitialisationController(InitialisationHandler businessware)
        {
            _businessware = businessware;
        }
        public ActionResult<ResponseObject<object>> Initialise()
        {
            return _businessware.initialiseData();
        }
    }
}