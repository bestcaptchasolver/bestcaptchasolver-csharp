using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;

namespace bestcaptchasolver
{
    static class Utils
    {
        /// <summary>
        /// GET request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GET(string url, string user_agent, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = user_agent;
            request.Timeout = timeout;
            request.ReadWriteTimeout = timeout;
            request.Accept = "*/*";
            request.ContentType = "application/json";
            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
               delegate { return true; }
            );
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        /// <summary>
        /// POST request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="post_data"></param>
        /// <param name="user_agent"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static string POST(string url, string post_data, string user_agent, int timeout)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var data = Encoding.ASCII.GetBytes(post_data);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            ServicePointManager.ServerCertificateValidationCallback = new
            RemoteCertificateValidationCallback
            (
               delegate { return true; }
            );

            // set user agent and timeout
            request.UserAgent = user_agent;
            request.Timeout = timeout;
            request.ReadWriteTimeout = timeout;
            request.Accept = "*/*";

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse();
            string s = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return s;
        }
        /// <summary>
        /// Read captcha image
        /// </summary>
        /// <param name="captcha_path"></param>
        /// <returns></returns>
        public static string read_captcha_image(string captcha_path)
        {
            Byte[] bytes = File.ReadAllBytes(captcha_path);
            string file_data = Convert.ToBase64String(bytes);
            return file_data;
        }
    }
}
