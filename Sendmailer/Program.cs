using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sendmailer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                cMailIslemleri mailser = new cMailIslemleri();
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+"/mailing.html"))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.AppendLine(line);
                    }
                }
                string htmlText = sb.ToString();
                mailser.SendMail(new cMAIL() { BASLIK = "title", ICERIK = htmlText, GONDERILECEK_ADRESLER = new List<string> { "tahsin.sevinc@plasenta.com.tr" } });

            }
            catch
            {
                Console.WriteLine("Hata");
            }
            Console.ReadLine();
        }
    }
}
