using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using EApi;
using RESTApiDelo.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTApiDelo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddRCController : ControllerBase
    {
        public bool IfRepeat = false;
        public bool CanBeUsed = true;
        //public Head head;

        //private static Head CreateHead()
        //{
        //    Head h = new Head();
        //    h.OpenWithParamsEx("10.10.6.70", "delec", "tver", "tver");
        //    return h;
        //}


        [HttpGet]
        public string Get([FromQuery]string aFreeNum, [FromQuery] int aOrderNum)
        {
            int? aIsn = 0;
            DateTime aDocDate = DateTime.Now.Date;
            DateTime aCorrespDate = DateTime.Now.Date;
            DateTime dateTimeEmpty = DateTime.MinValue.Date;
            try
            {
                try
                {
                    //head = CreateHead();
                    Startup._logger.Information("Создано подключение: Процедура AddRC");
                }
                catch
                {
                    Startup._logger.Error("Ошибка: Ошибка подключения. Процедура AddRC");
                }

                //IfRepeat = Helper.CheckRepeat(aOrderNum, head);
                //CanBeUsed = Helper.CheckCanBeUsed(aOrderNum, head);

                Random rand1 = new Random();
                Random rand2 = new Random();
                Random rand3 = new Random();
                int r1 = rand1.Next(0, 3);
                int r2 = rand2.Next(0, 3);
                aIsn = rand3.Next(5000, 7000);
                IfRepeat = Convert.ToBoolean(r1);
                CanBeUsed = Convert.ToBoolean(r2);

                if (!IfRepeat && CanBeUsed && aOrderNum != 0)
                {
                   // Procedures.add_rc(head, ref aIsn, "0.", 0, "0.2EZ47.2EZ49.", aOrderNum, aFreeNum, aDocDate,
                   //1, "null", "1", null, null, null, "Заявление на лицензирование тест api",
                   //"Заявление на лицензирование тест api", "", 2, "0.2EZ3X.", null, "56",
                   //aCorrespDate, "", null, null, null, "", "", null, null, null, null, null,
                   //null, "", "", null, "", "", null, null, "");

                    int? ref_aOrderNum = aOrderNum;

                    if (aIsn != 0)
                    {
                        Startup._logger.Information("Записана РК: ISN_DOC: {0}, ORDER_NUM: {1}", aIsn, aOrderNum);
                        return string.Format(aIsn.ToString());
                        //
                    }
                    else
                    {
                        //Procedures.return_num(head, "R", "0.2EZ47.2EZ49.", DateTime.Now.Year, ref ref_aOrderNum);
                        Startup._logger.Error("Ошибка: Зарегистрированный номер {0} отменен", aOrderNum);
                        return "Error";
                        
                    }
                }
                else if (IfRepeat)
                {
                    Startup._logger.Error("Ошибка: Попытка повторного использования ORDER_NUM: {0}", aOrderNum);
                    return "Error: Repeat";
                }
                else if (!CanBeUsed)
                {
                    Startup._logger.Error("Ошибка: Попытка использования незарегистрированного ORDER_NUM: {0}", aOrderNum);
                    return "Error: Number is not reserved";
                }
                else
                {
                    Startup._logger.Error("Ошибка: Ошибка проверки. Входные данные ORDER_NUM: {0}, FREE_NUM: {1}", aOrderNum, aFreeNum);
                    return "Error";
                }
            }
            catch
            {
                Startup._logger.Error("Ошибка: Ошибка выполнения процедуры AddRC. Входные данные ORDER_NUM: {0}, FREE_NUM: {1}", aOrderNum, aFreeNum);
                return "Error";
            }
        }
    }
}
