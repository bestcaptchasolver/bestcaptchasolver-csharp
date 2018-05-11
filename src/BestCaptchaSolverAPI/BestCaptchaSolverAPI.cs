using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bestcaptchasolver
{
    public class BestCaptchaSolverAPI
    {
        private const string BASE_URL = "https://bcsapi.xyz/api";
        private const string USER_AGENT = "csharpClient1.0";
        private const int TIMEOUT = 30000;

        private string _access_token;
        private int _timeout;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="timeout"></param>
        public BestCaptchaSolverAPI(string access_token, int timeout = 30000)
        {
            this._access_token = access_token;
            this._timeout = timeout;
        }

        /// <summary>
        /// Get account's balance
        /// </summary>
        /// <returns></returns>
        public string account_balance()
        {
            var url = string.Format("{0}/user/balance?access_token={1}", BASE_URL, this._access_token);
            var resp = Utils.GET(url, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return string.Format("${0}", d.balance.ToString());
        }

        /// <summary>
        /// Submit image captcha
        /// </summary>
        /// <param name="image"></param>
        /// <param name="case_sensitive"></param>
        /// <returns>captchaID</returns>
        public string submit_image_captcha(string image, bool case_sensitive = false)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/image", BASE_URL);
            dict.Add("access_token", this._access_token);
            // if no b64 string was given, but image path instead
            if (File.Exists(image)) image = Utils.read_captcha_image(image);
            dict.Add("b64image", image);
            if (case_sensitive) dict.Add("case_sensitive", "1");
            var data = JsonConvert.SerializeObject(dict);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }
        /// <summary>
        /// Submit image captcha
        /// </summary>
        /// <param name="image"></param>
        /// <param name="case_sensitive"></param>
        /// <returns>captchaID</returns>
        public string submit_recaptcha(string page_url, string site_key, string proxy = "")
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/recaptcha", BASE_URL);
            dict.Add("access_token", this._access_token);
            dict.Add("page_url", page_url);
            dict.Add("site_key", site_key);
            if(!string.IsNullOrWhiteSpace(proxy))
            {
                dict.Add("proxy", proxy);
                dict.Add("proxy_type", "HTTP");
            }
            var data = JsonConvert.SerializeObject(dict);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }

        /// <summary>
        /// Retrieve captcha text / gresponse using captcha ID
        /// </summary>
        /// <param name="captchaid"></param>
        /// <returns></returns>
        public string retrieve(string captchaid)
        {
            var url = string.Format("{0}/captcha/{1}?access_token={2}", BASE_URL, captchaid, this._access_token);
            string resp = Utils.GET(url, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            if (d.status == "pending") return null;
            try
            {
                return d.gresponse.ToString();
            }
            catch
            {
                return d.text.ToString();
            }
        }

        /// <summary>
        /// Set captcha bad
        /// </summary>
        /// <param name="captchaid"></param>
        /// <returns></returns>
        public string set_captcha_bad(string captchaid)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/bad/{1}", BASE_URL, captchaid);
            dict.Add("access_token", this._access_token);
            var data = JsonConvert.SerializeObject(dict);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.status.ToString();
        }
    }
}
