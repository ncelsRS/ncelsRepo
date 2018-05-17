using Microsoft.AspNetCore.Mvc;
using RSC.FileStorageLogic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Download([FromRoute] string fileId)
        {
            var result = await _logic.DownloadAsStream(fileId);
            return File(result.Item1, result.Item2, result.Item3);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return Json(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetFiles([FromQuery] string entityType, [FromQuery] string entityId, [FromQuery] string fileType)
        {
            return Json(await _logic.GetFiles(entityType, entityId, fileType));
        }
    }
}
