using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApiDelo.Helpers
{
    public class Procedures
    {
        public static void reserve_num(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            string aOper, //тип операции
            string aDueDocgroup, //код due группы документов
            int aYear, //год в счетчике номерообразования РК
            string card_id, //код due картотеки регистрации
            ref int? aOrderNum, //порядковый номер
            ref string aFreeNum, //регистрационный номер
            string aSessionId) //идентификатор сессии
        {
            dynamic proc = oHead.GetProc("reserve_num");

            proc.Parameters.Append(proc.CreateParameter("aOper", 200, 1, 2, aOper));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, aDueDocgroup));
            proc.Parameters.Append(proc.CreateParameter("aYear", 3, 1, 0, aYear));
            proc.Parameters.Append(proc.CreateParameter("card_id", 200, 1, 48, card_id));
            dynamic aOrderNumParam = proc.CreateParameter("aOrderNum", 3, 3, 0, (object)check_null(aOrderNum));
            proc.Parameters.Append(aOrderNumParam);
            dynamic aFreeNumParam = proc.CreateParameter("aFreeNum", 200, 3, 64, check_null(aFreeNum));
            proc.Parameters.Append(aFreeNumParam);
            proc.Parameters.Append(proc.CreateParameter("aSessionId", 200, 1, 255, aSessionId));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aOrderNum = null;
                aFreeNum = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aOrderNum = (int?)aOrderNumParam.Value;
                aFreeNum = (string)aFreeNumParam.Value;
            }
        }

        public static void add_rc(
            dynamic oHead, //головной объект системы, поддерживающий интерфейс IHead
            ref int? aIsn, //Isn зарегистрированной РК   
            string aDueCard, //код due картотеки регистрации
            int aIsnCab, //Isn кабинета регистрации
            string aDueDocgroup, //код due группы документов
            int aOrderNum, //порядковый  номер
            string aFreeNum, //регистрационный номер
            DateTime aDocDate, //дата регистрации РК
            int aSecurlevel, //Isn грифа доступа
            string aConsists, //состав
            string aSpecimen, //номера  экземпляров
            DateTime? aPlanDate, //плановая  дата исполнения документа
            DateTime? aFactDate, //фактическая дата исполнения документа
            int? aControlState, //флаг контрольности
            string aAnnotat, //содержание
            string aNote, //примечание
            string aDuePersonWhos, //коды due должностных лиц, кому адресован входящий документ
            int? aIsnDelivery, //Isn вида доставки входящего документа
            string aDueOrganiz, //код due организации - корреспондента входящего документа
            int? aIsnContact, //Isn контакта корреспондента входящего документа для входящих документов
            string aCorrespNum, //исходящий номер входящего документа
            DateTime? aCorrespDate, //исходящая дата входящего документа
            string aCorrespSign, //лицо, подписавшее  входящий документ
            int? aIsnCitizen, //Isn гражданина – корреспондента письма
            int? aIsCollective, //признак  коллективного письма
            int? aIsAnonim, //признак  анонимного письма
            string aSigns, //список подписавших исходящий документ (коды due)
            string aDuePersonExe, //код due исполнителя исходящего документа
            int? aIsnNomenc, //Isn дела в номенклатуе дел
            int? aNothardcopy, //флаг "без досылки бумажного экземпляра"
            int? aCito, //флаг "срочно"
            int? aIsnLinkingDoc, //Isn связанной РК
            int? aIsnLinkingPrj, //Isn регистрируемого РКПД (в случае  регистрации связанной РК из проекта)
            int? aIsnClLink, //Isn типа связки
            string aCopyShablon, //маска для копирования реквизитов
            string aVisas, //cписок лиц, завизировавших документ (коды due)
            int? aEDocument,
            string aSends,
            string askipcopy_ref_file_isns,
            int? aIsnLinkTranparent,
            int? aIsnLinkTranparentPare,
            string aTelNum) //флаг "Оригинал в электронном виде"
        {
            dynamic proc = oHead.GetProc("add_rc");
            string maskDate = "yyyyMMdd";


            dynamic aIsnParam = proc.CreateParameter("aIsn", 3, 3, 0, (object)aIsn);
            proc.Parameters.Append(aIsnParam);
            proc.Parameters.Append(proc.CreateParameter("aDueCard", 200, 1, 48, aDueCard));
            proc.Parameters.Append(proc.CreateParameter("aIsnCab", 3, 1, 0, aIsnCab));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, aDueDocgroup));
            proc.Parameters.Append(proc.CreateParameter("aOrderNum", 3, 1, 0, aOrderNum));
            proc.Parameters.Append(proc.CreateParameter("aFreeNum", 200, 1, 64, check_null(aFreeNum)));
            proc.Parameters.Append(proc.CreateParameter("aDocDate", 200, 1, 20, check_null(aDocDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aSecurlevel", 3, 1, 0, aSecurlevel));
            proc.Parameters.Append(proc.CreateParameter("aConsists", 200, 1, 255, check_null(aConsists)));
            proc.Parameters.Append(proc.CreateParameter("aSpecimen", 200, 1, 64, aSpecimen));
            proc.Parameters.Append(proc.CreateParameter("aPlanDate", 200, 1, 20, check_null(aPlanDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aFactDate", 200, 1, 20, check_null(aFactDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aControlState", 3, 1, 0, (object)check_null(aControlState)));
            proc.Parameters.Append(proc.CreateParameter("aAnnotat", 200, 1, 2000, check_null(aAnnotat)));
            proc.Parameters.Append(proc.CreateParameter("aNote", 200, 1, 2000, check_null(aNote)));
            proc.Parameters.Append(proc.CreateParameter("aDuePersonWhos", 200, 1, 8000, check_null(aDuePersonWhos)));
            proc.Parameters.Append(proc.CreateParameter("aIsnDelivery", 3, 1, 0, (object)check_null(aIsnDelivery)));
            proc.Parameters.Append(proc.CreateParameter("aDueOrganiz", 200, 1, 48, check_null(aDueOrganiz)));
            proc.Parameters.Append(proc.CreateParameter("aIsnContact", 3, 1, 0, (object)check_null(aIsnContact)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespNum", 200, 1, 64, check_null(aCorrespNum)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespDate", 200, 1, 20, check_null(aCorrespDate, maskDate)));
            proc.Parameters.Append(proc.CreateParameter("aCorrespSign", 200, 1, 255, check_null(aCorrespSign)));
            proc.Parameters.Append(proc.CreateParameter("aIsnCitizen", 3, 1, 0, (object)check_null(aIsnCitizen)));
            proc.Parameters.Append(proc.CreateParameter("aIsCollective", 3, 1, 0, (object)check_null(aIsCollective)));
            proc.Parameters.Append(proc.CreateParameter("aIsAnonim", 3, 1, 0, (object)check_null(aIsAnonim)));
            proc.Parameters.Append(proc.CreateParameter("aSigns", 200, 1, 8000, check_null(aSigns)));
            proc.Parameters.Append(proc.CreateParameter("aDuePersonExe", 200, 1, 8000, check_null(aDuePersonExe)));
            proc.Parameters.Append(proc.CreateParameter("aIsnNomenc", 3, 1, 0, (object)check_null(aIsnNomenc)));
            proc.Parameters.Append(proc.CreateParameter("aNothardcopy", 3, 1, 0, (object)check_null(aNothardcopy)));
            proc.Parameters.Append(proc.CreateParameter("aCito", 3, 1, 0, (object)check_null(aCito)));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkingDoc", 3, 1, 0, (object)check_null(aIsnLinkingDoc)));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkingPrj", 3, 1, 0, (object)check_null(aIsnLinkingPrj)));
            proc.Parameters.Append(proc.CreateParameter("aIsnClLink", 3, 1, 0, (object)check_null(aIsnClLink)));
            proc.Parameters.Append(proc.CreateParameter("aCopyShablon", 200, 1, 20, check_null(aCopyShablon)));
            proc.Parameters.Append(proc.CreateParameter("aVisas", 200, 1, 8000, aVisas));
            proc.Parameters.Append(proc.CreateParameter("aEDocument", 3, 1, 0, (object)aEDocument));
            proc.Parameters.Append(proc.CreateParameter("aSends", 200, 1, 8000, aSends));
            proc.Parameters.Append(proc.CreateParameter("askipcopy_ref_file_isns", 200, 1, 8000, askipcopy_ref_file_isns));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkTranparent", 3, 1, 0, (object)aIsnLinkTranparent));
            proc.Parameters.Append(proc.CreateParameter("aIsnLinkTranparentPare", 3, 1, 0, (object)aIsnLinkTranparent));
            proc.Parameters.Append(proc.CreateParameter("aTelNum", 200, 1, 64, aTelNum));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                aIsn = null;
                throw new Exception(oHead.ErrText);
            }
            else
            {
                aIsn = (int?)aIsnParam.Value;
            }
        }


        public static void return_num(
            dynamic oHead,
            string aOper,
            string aDueDocgroup,
            int aYear,
            ref int? aOrderNum)
        {
            dynamic proc = oHead.GetProc("return_num");

            proc.Parameters.Append(proc.CreateParameter("aOper", 200, 1, 2, aOper));
            proc.Parameters.Append(proc.CreateParameter("aDueDocgroup", 200, 1, 48, aDueDocgroup));
            proc.Parameters.Append(proc.CreateParameter("aYear", 3, 1, 0, aYear));
            proc.Parameters.Append(proc.CreateParameter("aOrderNum", 200, 1, 64, (object)check_null(aOrderNum)));
            proc.Parameters.Append(proc.CreateParameter("aFreeNum", 200, 1, 64, ""));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
            {
                throw new Exception(oHead.ErrText);
            }
        }

        public static void save_file_wf(
            dynamic oHead,
            int action,
            int? aISN_REF_FILE,
            int? aISN_RC,
            int? aKind_Doc,
            string aDescription,
            string aCategory,
            int? aSecur,
            int? aLockFlag,
            string aFileAccessDues,
            string aFileName,
            int aDontDelFlag)
        {
            dynamic proc = oHead.GetProc("SAVE_FILE_WF");

            proc.Parameters.Append(proc.CreateParameter("action", 3, 1, 0, action));
            proc.Parameters.Append(proc.CreateParameter("aISN_REF_FILE", 3, 1, 0, check_null(aISN_REF_FILE)));
            proc.Parameters.Append(proc.CreateParameter("aISN_RC", 3, 1, 0, check_null(aISN_RC)));
            proc.Parameters.Append(proc.CreateParameter("aKind_Doc", 3, 1, 0, check_null(aKind_Doc)));
            proc.Parameters.Append(proc.CreateParameter("aDescription", 200, 1, 255, check_null(aDescription)));
            proc.Parameters.Append(proc.CreateParameter("aCategory", 200, 1, 255, check_null(aCategory)));
            proc.Parameters.Append(proc.CreateParameter("aSecur", 3, 1, 0, check_null(aSecur)));
            proc.Parameters.Append(proc.CreateParameter("aLockFlag", 3, 1, 0, check_null(aLockFlag)));
            proc.Parameters.Append(proc.CreateParameter("aFileAccessDues", 200, 1, 255, check_null(aFileAccessDues)));
            proc.Parameters.Append(proc.CreateParameter("aFileName", 200, 1, 255, check_null(aFileName)));
            proc.Parameters.Append(proc.CreateParameter("aDontDelFlag", 3, 1, 0, check_null(aDontDelFlag)));
            proc.Parameters.Append(proc.CreateParameter("aIs_hidden", 3, 1, 0, 0));
            proc.Parameters.Append(proc.CreateParameter("aApply_eds", 3, 1, 0, 0));
            proc.Parameters.Append(proc.CreateParameter("aSend_enabled", 3, 1, 0, 0));
            proc.Parameters.Append(proc.CreateParameter("aSaveSourceFile", 3, 1, 0, 1));

            oHead.ExecuteProc(proc);

            if (oHead.ErrCode < 0)
                throw new Exception(oHead.ErrText);
        }

        static dynamic check_null(dynamic prop)
        {
            return check_null(prop, string.Empty);
        }

        static dynamic check_null(dynamic prop, string date_mask)
        {
            dynamic result = Convert.DBNull;

            if (prop != null)
            {
                string typeName = prop.GetType().ToString();
                switch (typeName)
                {
                    case ("System.String"):
                        {
                            string strProp = prop;
                            result = string.IsNullOrEmpty(strProp) ? string.Empty : strProp;
                            break;
                        }
                    case ("System.DateTime"):
                        {
                            DateTime? propDate = prop;
                            if (!string.IsNullOrEmpty(date_mask))
                                result = propDate.HasValue ? propDate.Value.ToString(date_mask) : string.Empty;
                            else result = string.Empty;
                            break;
                        }
                    case ("System.DateTime[]"):
                        {
                            DateTime[] propDate = prop;
                            string outStrDate = "";
                            if (!string.IsNullOrEmpty(date_mask))
                            {
                                for (int i = 0; i < propDate.Length; i++)
                                {
                                    if (i != propDate.Length - 1)
                                    {
                                        if (propDate[i] != DateTime.MinValue)
                                            outStrDate += propDate[i].ToString(date_mask) + "|";
                                        else
                                            outStrDate += Convert.DBNull + "|";
                                    }
                                    else if (propDate[i] != DateTime.MinValue)
                                        outStrDate += propDate[i].ToString(date_mask);
                                }
                                result = outStrDate;
                                break;
                            }
                            result = string.Empty;
                            break;
                        }
                    case ("System.Int32"):
                        {
                            int? propInt = prop;
                            result = propInt.HasValue ? propInt.Value : Convert.DBNull;
                            break;
                        }
                    default:
                        {
                            result = Convert.DBNull;
                            break;
                        }
                }
            }
            return result;
        }
    }
}
