using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSC.FileStorageLogic;
using Teme.SharedApi.Controllers;

namespace RSC.FileStorage.Controllers
{
    [Route("[controller]")]
    public class FilesController : Controller
    {
        private readonly IFileLogic _logic;
        public FilesController(IFileLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            return null;
        }
    }
}
