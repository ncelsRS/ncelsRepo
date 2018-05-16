using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            return Json(await _logic.UploadFromStream(HttpContext));
        }

        [HttpGet]
        [Route("{fileId}")]
        public async Task<IActionResult> Get([FromRoute] string fileId)
        {
            var result = await _logic.DownloadAsStream(fileId);
            return File(result.Item1, result.Item2, result.Item3);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Json(null);
        }
    }
}
