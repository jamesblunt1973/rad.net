using rad.net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rad.net.CommonClasses
{
    public class Sms
    {
        public Sms(IRepository repository)
        {
            exception = null;
            repo = repository;
        }

        string Signature
        {
            get
            {
                return repo.Setting("PhoneSign").Result;
            }
        }
        string FromNumber
        {
            get
            {
                return repo.Setting("PhoneNumber").Result;
            }
        }

        string[] _retstr;
        private readonly IRepository repo;

        string[] retstr
        {
            get { return _retstr; }
            set
            {
                _retstr = value;
                if (value.Length == 1)
                {
                    string[] splited = value[0].Split(new char[] { ';' });
                    if (splited.Length == 3)
                    {
                        sendnumber = splited[0];
                        status = "";
                        int temp = -1;
                        int.TryParse(splited[1], out temp);
                        switch (temp)
                        {
                            case 0:
                                status = "موفق";
                                break;
                            case 1:
                                status = "ناموفق";
                                break;
                            case 2:
                                status = "خطا";
                                break;
                            case 3:
                                status = "بلک لیست";
                                break;
                            default:
                                status = "خطای ارسال";
                                break;
                        }
                        RecID = splited[2];
                    }
                }
            }
        }
        public int success { get; set; }
        int _ReturnValue { get; set; }
        int ReturnValue
        {
            get { return _ReturnValue; }
            set
            {
                _ReturnValue = value;
                switch (value)
                {
                    case -1:
                        returnMsg = "امضا معتبر نیست";
                        break;
                    case 0:
                        returnMsg = "ارسال نشد";
                        break;
                    case 1:
                        returnMsg = "ارسال با موفقیت انجام شد .";
                        break;
                    case 2:
                        returnMsg = "پیامک معتبر نیست";
                        break;
                    case 3:
                        returnMsg = " محدودیت حد اقل درخواست - هیچ شماره موبایلی موجود نیست";
                        break;
                    case 4:
                        returnMsg = "فیلتر می باشد.";
                        break;
                    case 5:
                        returnMsg = " اپراتور قطع است.";
                        break;
                    case 6:
                        returnMsg = "ارسال مجاز نیست.";
                        break;
                    case 7:
                        returnMsg = "حساب کاربری شما فعال نیست.";
                        break;
                    case 8:
                        returnMsg = "اعتبار کافی موجود نیست .";
                        break;
                    case 9:
                        returnMsg = "محدودیت در تعداد درخواست";
                        break;
                    case 10:
                        returnMsg = "محدودیت ارسال روزانه";
                        break;
                    case 11:
                        returnMsg = "شماره پیامک معتبر نیست";
                        break;
                    case 12:
                        returnMsg = "خطا";
                        break;
                    default:
                        returnMsg = "خطا در ارسال";
                        break;
                }
            }
        }

        public string status { get; set; }
        public string sendnumber { get; set; }
        public string RecID { get; set; }
        public string returnMsg { get; set; }
        public Exception exception { get; set; }


        public void send(string msg, string[] numbers)
        {
            try
            {
                exception = null;
                int _successCount = 0;
                string[] _ReturnStr = null;

                PARSGREEN.API_SendSMS.SendSMS sms = new PARSGREEN.API_SendSMS.SendSMS();
                //ReturnValue = sms.SendGroupSMS(Signature, FromNumber, numbers, msg, false, string.Empty, ref _successCount, ref _ReturnStr);

                success = _successCount;
                if (_ReturnStr == null) _ReturnStr = new string[] { string.Empty };
                retstr = _ReturnStr;
            }
            catch (Exception ex)
            {
                exception = ex;
                ReturnValue = -2;
                success = 0;
                retstr = new string[] { string.Empty };
            }
        }
    }


    enum StatusCode
    {
        NoOperation = 0,
        OnQueue = 1,
        Done = 2

    }
}
