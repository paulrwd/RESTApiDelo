using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EApi;
using RESTApiDelo.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTApiDelo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveNumController : ControllerBase
    {
        public Head head;

        [HttpGet]
        public string Get([FromQuery]string aOper)
        {

            try
            {
                head = CreateHead();
                Startup._logger.Information("Создано подключение: Процедура ReserveNum");


            }
            catch
            {
                Startup._logger.Error("Ошибка: Ошибка подключения. Процедура ReserveNum");

            }

            int? aOrderNum = 0;
            string aFreeNum = String.Empty;

            if (aOper == "N")
            {


                Procedures.reserve_num(head, aOper, "0.2EZ47.2EZ49.", 2021, "0.", ref aOrderNum, ref aFreeNum, null);
                Startup._logger.Information("Зарегистрирован номер: {0}", aOrderNum);
                return string.Format("aFreeNum: {0} , aOrderNum: {1}", aFreeNum, aOrderNum);


            }
            else
            {
                Startup._logger.Error("Ошибка выполнения процедуры ReserveNum: Некорректная операция");

                return "Error";
            }
        }

        private static Head CreateHead()
        {
            Head h = new Head();
            h.OpenWithParamsEx("10.10.6.70", "delec", "tver", "tver");
            return h;
        }
    }
}
