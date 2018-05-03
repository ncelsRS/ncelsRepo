using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Teme.ContractCoz.Api.Controllers
{
    public class ActionsController 
    {
        [HttpGet]
        [Route("")]
        public void Index()
        {
        }
    }
}