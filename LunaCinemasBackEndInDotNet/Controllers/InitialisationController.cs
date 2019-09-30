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
        private InitialisationHandler Businessware { get; }

        public InitialisationController(InitialisationHandler businessware)
        {
            Businessware = businessware;
        }
        public ActionResult<ResponseObject<object>> Initialise()
        {
            return Businessware.InitialiseData();
        }
    }
}