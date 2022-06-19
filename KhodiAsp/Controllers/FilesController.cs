using KhodiAsp.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KhodiAsp.Controllers
{
    [Route("api/fileUpload")]
    public class FilesController : Controller
    {

        FileOps fileOps = new FileOps();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]        
        public async Task<ActionResult> FileUpload()
        {
            var result = "";

            if(Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files.Get(0);
                
                result = await fileOps.uploadFile(file);
            }

            return Content(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteFile()
        {
            var param = Request.Form.GetValues("fileName");
            var x = param.First();           

            var results = await fileOps.deleteFile(x);

            return Json(results);
        }
    }
}