using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

namespace Sendmailer
{
    public class cMAIL
    {
        internal string MAIL_SERVER = ConfigurationManager.AppSettings["MAIL_SERVER"];
        internal int MAIL_PORT = ConfigurationManager.AppSettings["MAIL_PORT"].ToInt();
        internal string GONDEREN_ADRES = ConfigurationManager.AppSettings["GONDEREN_ADRES"];
        internal string GONDEREN_SIFRE = ConfigurationManager.AppSettings["GONDEREN_SIFRE"];
        internal string GORUNEN_AD = ConfigurationManager.AppSettings["GORUNEN_AD"];
        internal bool ENABLE_SSL = Convert.ToBoolean(ConfigurationManager.AppSettings["ENABLE_SSL"].ToShort());
        public string BASLIK { get; set; }
        public string ICERIK { get; set; }
        //public Attachment dosya { get; set; }
        public List<string> GONDERILECEK_ADRESLER { get; set; }
    }
    public class cMailIslemleri
    {
        public bool SendMail(cMAIL pMail)
        {
            try
            {
                MailMessage mmEPosta = new MailMessage();
                //mmEPosta.Attachments.Add(pMail.dosya);
                mmEPosta.Subject = pMail.BASLIK;
                mmEPosta.Body = pMail.ICERIK;
                mmEPosta.IsBodyHtml = true;
                foreach (string gonAdres in pMail.GONDERILECEK_ADRESLER)
                    mmEPosta.To.Add(gonAdres);
                mmEPosta.From = new MailAddress(pMail.GONDEREN_ADRES, pMail.GORUNEN_AD, System.Text.Encoding.UTF8);
                System.Net.NetworkCredential ncCredential;
                ncCredential = new System.Net.NetworkCredential(pMail.GONDEREN_ADRES, pMail.GONDEREN_SIFRE);

                SmtpClient SMTP = new SmtpClient();
                SMTP.Port = pMail.MAIL_PORT;
                SMTP.Host = pMail.MAIL_SERVER;
                SMTP.UseDefaultCredentials = true;
                SMTP.EnableSsl = pMail.ENABLE_SSL;
                SMTP.Credentials = ncCredential;
                SMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
                SMTP.Send(mmEPosta);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
