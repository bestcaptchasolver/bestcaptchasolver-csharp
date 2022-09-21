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
        private const int TIMEOUT = 6000000;

        private string _access_token;
        private int _timeout;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="timeout"></param>
        public BestCaptchaSolverAPI(string access_token, int timeout = 6000000)
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
        /// <param name="opts"></param>
        /// <returns>captchaID</returns>
        public string submit_image_captcha(Dictionary<string, string> opts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/image", BASE_URL);
            dict.Add("access_token", this._access_token);
            var image = "";
            // if no b64 string was given, but image path instead
            if (File.Exists(opts["image"])) image = Utils.read_captcha_image(opts["image"]);
            else image = opts["image"];
            dict.Add("b64image", image);
            // optional parameters
            if (opts.ContainsKey("case_sensitive"))
            {
                if (opts["case_sensitive"].Equals("true")) dict.Add("is_case", "true");
            }
            if (opts.ContainsKey("is_case"))
            {
                if (opts["is_case"].Equals("true")) dict.Add("is_case", "true");
            }
            if (opts.ContainsKey("is_phrase"))
            {
                if (opts["is_phrase"].Equals("true")) dict.Add("is_phrase", "true");
            }
            if (opts.ContainsKey("is_math"))
            {
                if (opts["is_math"].Equals("true")) dict.Add("is_math", "true");
            }
            if (opts.ContainsKey("alphanumeric")) dict.Add("alphanumeric", opts["alphanumeric"]);
            if (opts.ContainsKey("minlength")) dict.Add("minlength", opts["minlength"]);
            if (opts.ContainsKey("maxlength")) dict.Add("maxlength", opts["maxlength"]);
            // affiliate ID
            if (opts.ContainsKey("affiliate_id")) dict.Add("affiliate_id", opts["affiliate_id"]);
            var data = JsonConvert.SerializeObject(dict);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }
        /// <summary>
        /// Submit reCAPTCHA
        /// </summary>
        /// <param name="opts"></param>
        /// <returns>captchaID</returns>
        public string submit_recaptcha(Dictionary<string, string> opts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/recaptcha", BASE_URL);
            dict.Add("access_token", this._access_token);
            dict.Add("page_url", opts["page_url"]);
            dict.Add("site_key", opts["site_key"]);
            if(opts.ContainsKey("proxy"))
            {
                dict.Add("proxy", opts["proxy"]);
                dict.Add("proxy_type", "HTTP");
            }

            // optional params
            if (opts.ContainsKey("type")) dict.Add("type", opts["type"]);
            if (opts.ContainsKey("v3_action")) dict.Add("v3_action", opts["v3_action"]);
            if (opts.ContainsKey("v3_min_score")) dict.Add("v3_min_score", opts["v3_min_score"]);
            if (opts.ContainsKey("data_s")) dict.Add("data_s", opts["data_s"]);
            if (opts.ContainsKey("cookie_input")) dict.Add("cookie_input", opts["cookie_input"]);
            if (opts.ContainsKey("user_agent")) dict.Add("user_agent", opts["user_agent"]);
            if (opts.ContainsKey("affiliate_id")) dict.Add("affiliate_id", opts["affiliate_id"]);

            var data = JsonConvert.SerializeObject(dict);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }

        /// <summary>
        /// Submit GeeTestV4
        /// </summary>
        /// <param name="opts"></param>
        /// <returns>captchaID</returns>
        public string submit_geetest_v4(Dictionary<string, string> opts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/geetestv4", BASE_URL);
            opts.Add("access_token", this._access_token);

            var data = JsonConvert.SerializeObject(opts);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }

        /// <summary>
        /// Submit GeeTest
        /// </summary>
        /// <param name="opts"></param>
        /// <returns>captchaID</returns>
        public string submit_geetest(Dictionary<string, string> opts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/geetest", BASE_URL);
            opts.Add("access_token", this._access_token);

            var data = JsonConvert.SerializeObject(opts);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }

        /// <summary>
        /// Submit Capy
        /// </summary>
        /// <param name="opts"></param>
        /// <returns>captchaID</returns>
        public string submit_capy(Dictionary<string, string> opts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/capy", BASE_URL);
            opts.Add("access_token", this._access_token);

            var data = JsonConvert.SerializeObject(opts);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }

        /// <summary>
        /// Submit FunCaptcha
        /// </summary>
        /// <param name="opts"></param>
        /// <returns>captchaID</returns>
        public string submit_funcaptcha(Dictionary<string, string> opts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/funcaptcha", BASE_URL);
            opts.Add("access_token", this._access_token);

            var data = JsonConvert.SerializeObject(opts);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }

        /// <summary>
        /// Submit hCaptcha
        /// </summary>
        /// <param name="opts"></param>
        /// <returns>captchaID</returns>
        public string submit_hcaptcha(Dictionary<string, string> opts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/hcaptcha", BASE_URL);
            opts.Add("access_token", this._access_token);

            var data = JsonConvert.SerializeObject(opts);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }

        /// <summary>
        /// Submit task
        /// </summary>
        /// <param name="opts"></param>
        /// <returns>captchaID</returns>
        public string submit_task(Dictionary<string, string> opts)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var url = string.Format("{0}/captcha/task", BASE_URL);
            opts.Add("access_token", this._access_token);

            var data = JsonConvert.SerializeObject(opts);
            var resp = Utils.POST(url, data, USER_AGENT, TIMEOUT);
            dynamic d = JObject.Parse(resp);
            return d.id.ToString();
        }

        /// <summary>
        /// Retrieve captcha text / gresponse using captcha ID
        /// </summary>
        /// <param name="captchaid"></param>
        /// <returns></returns>
        public Dictionary<string, string> retrieve(string captchaid)
        {
            var url = string.Format("{0}/captcha/{1}?access_token={2}", BASE_URL, captchaid, this._access_token);
            string resp = Utils.GET(url, USER_AGENT, TIMEOUT);
            JObject d = JObject.Parse(resp);
            // check if still in pending
            if (d.GetValue("status").ToString() == "pending")
            {
                var dd = new Dictionary<string, string>();
                dd.Add("gresponse", "");
                dd.Add("text", "");
                dd.Add("solution", "");
                return dd;
            }
            // we're good, create dict and return
            var dict = new Dictionary<string, string>();
            foreach(var e in d)
            {
                dict.Add(e.Key, e.Value.ToString());   
            }
            return dict;
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
