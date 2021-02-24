using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EApi;
using System.Configuration;
using RESTApiDelo.Helpers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using RESTApiDelo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTApiDelo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveFileController : ControllerBase
    {

        public Head head;
        private static Head CreateHead()
        {
            Head h = new Head();
            h.OpenWithParamsEx("10.10.6.70", "delec", "tver", "tver");
            return h;
        }

        [HttpPost]
        public void Post([FromForm] sFile _sFile, IFormFile file)
        {
            try
            {
                head = CreateHead();
                Startup._logger.Information("Создано подключение: Процедура SaveFile");
            }
            catch
            {
                Startup._logger.Error("Ошибка: Ошибка подключения. Процедура SaveFile");
            }

            try
            {

                string addrFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("PathForPDF")["path"];

                var filePath = Path.Combine(addrFile, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                     file.CopyTo(fileStream);
                }

                //byte[] bytes = Convert.FromBase64String(sFile);
                //FileStream stream =
                //new FileStream(addrFile, FileMode.CreateNew);
                //System.IO.BinaryWriter writer =
                //    new BinaryWriter(stream);
                //writer.Write(file., 0, file.Length);
                //writer.Close();
                Startup._logger.Information("Создан временный файл для РК №{0}", _sFile.aIsn);

                Procedures.save_file_wf(head, 1, null, _sFile.aIsn, 1, "Файл к РК №" + _sFile.aIsn.ToString(), null, null, null, null, filePath, 0);
                Startup._logger.Information("Выполнена процедура SaveFile для РК №{0}", _sFile.aIsn);

                System.IO.File.Delete(filePath);
                Startup._logger.Information("Удален временный файл для РК №{0}", _sFile.aIsn);

                Startup._logger.Information("Запись файла прошла успешно");

            }
            catch
            {
                Startup._logger.Error("Ошибка: Ошибка выполнения процедуры SaveFile. Входные данные ISN_DOC: {0}", _sFile.aIsn);
            }
        }
    }
}
