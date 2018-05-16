using OpenSSL.Core;
using OpenSSL.X509;
using OpenSSL.XMLsec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PW.Ncels.Helpers
{
    public static class DigitalSignHelper
    {
        private const string caCertUrl = "http://root.gov.kz/root_cer/pki_rsa.crt";
        private const string ocspUrl = "http://ocsp.pki.gov.kz";
        public static int GetCertificateState(string xml)
        {
            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData(caCertUrl);
            BIO bio = new BIO(bytes);
            X509Certificate certca = X509Certificate.FromDER(bio);

            XMLClass xmlEx = new XMLClass();
            byte[] bytesCert = UTF8Encoding.UTF8.GetBytes(xml);
            string sCert = xmlEx.getX509FromXMLbytes(bytesCert);
            BIO bCert = new BIO(Convert.FromBase64String(sCert));
            X509Certificate cert = X509Certificate.FromDER(bCert);

            int result = cert.CheckOCSP(certca, ocspUrl);

            return result;
        }

        public static string GetMessage(int result)
        {
            string message;
            if (result < 0)
            {
                message = "Статус проверки ЭЦП неизвестен";
            }
            else if (result == 0)
            {
                message = "ЭЦП действителен";
            }
            else
            {
                message = "Ваш ЭЦП был отозван";
            }
            return message;
        }
    }
}