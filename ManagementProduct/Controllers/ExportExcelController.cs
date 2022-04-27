using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PM.Data.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportExcelController : ControllerBase
    {
        private readonly AppDbContext _app;
        public ExportExcelController(AppDbContext app)
        {
            _app = app;
        }
        [HttpGet("export-list-user")]
        public IActionResult ExportUser()
        {
            var data = _app.Users.ToList();
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("User");
                //đổ data vào
                sheet.Cells.LoadFromCollection(data, true);
                //save
                package.Save();
            }
            stream.Position = 0;
            var fileName = $"User_{DateTime.Now.ToShortDateString()}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        [HttpGet("export-list-product")]
        public IActionResult ExportProduct()
        {
            var data = _app.Products.ToList();
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets.Add("Product");
                //đổ data vào
                sheet.Cells.LoadFromCollection(data, true);
                //save
                package.Save();
            }
            stream.Position = 0;
            var fileName = $"Loai_{DateTime.Now.ToShortDateString()}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

    }
}
