using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using bestcaptchasolver;

namespace example
{
    class Program
    {
        // get acces token from https://bestcaptchasolver.com/account 
        public static string ACCESS_TOKEN = "ACCESS_TOKEN_HERE";
        public static BestCaptchaSolverAPI bcs = new BestCaptchaSolverAPI(ACCESS_TOKEN);

        /// <summary>
        /// Image captcha
        /// </summary>
        static void test_image()
        {
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            Console.WriteLine("Solving image captcha ...");
            var d = new Dictionary<string, string>();
            d.Add("image", "captcha.jpg");
            // d.Add("is_case", "true");       // case sensitive, default: false, optional
            // d.Add("is_phrase", "true");     // contains at least one space, default: false, optional
            // d.Add("is_math", "true");       // math calculation captcha, default: false, optional
            // d.Add("alphanumeric", "2");     // 1 (digits only) or 2 (letters only), default: all characters
            // d.Add("minlength", "3");        // minimum length of captcha text, default: any
            // d.Add("maxlength", "4");        // maximum length of captcha text, default: any
            // d.Add("affiliate_id", "get it from /account");      // affiliate ID
            var id = bcs.submit_image_captcha(d);
            string image_text = "";
            while(image_text == "")
            {
                image_text = bcs.retrieve(id)["text"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Captcha text: {0}", image_text));
            // bcs.set_captcha_bad(id);      // set captcha as bad
        }

        /// <summary>
        /// reCAPTCHA solving
        /// </summary>
        static void test_recaptcha()
        {
            string page_url = "PAGE_URL_HERE";
            string site_key = "SITE_KEY_HERE";
            
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            Console.WriteLine("Solving recaptcha ...");
            var rd = new Dictionary<string, string>();
            rd.Add("page_url", page_url);
            rd.Add("site_key", site_key);

            // other parameters
            // ----------------------------------------------------------------------
            // reCAPTCHA type(s) - optional, defaults to 1
            // ---------------------------------------------
            // 1 - v2
            // 2 - invisible
            // 3 - v3
            // 4 - enterprise v2
            // 5 - enterprise v3
            //
            // rd.Add("type", "1");
            //
            // rd.Add("v3_action", "home");    // action used when solving v3 reCaptcha
            // rd.Add("v3_min_score", "0.3");  // min score to target when solving v3
            // rd.Add("data_s", "recaptcha data-s parameter used in loading reCAPTCHA");  // optional
            // rd.Add("cookie_input", "a=b;c=d");  // used in solving reCAPTCHA, optional
            // rd.put("user_agent", "your user agent");        // UA used in solving captcha
            // rd.Add("proxy", "user:pass@191.123.43.34:301");     // proxy with/out authentication
            // rd.Add("affiliate_id", "get it from /account");

            string id = bcs.submit_recaptcha(rd);
            string gresponse = "";
            while (gresponse == "")
            {
                gresponse = bcs.retrieve(id)["gresponse"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Gresponse: {0}", gresponse));
            // var proxy_status = bcs.retrieve(id)["proxy_status"];
            // bcs.set_captcha_bad(id);      // set captcha as bad
        }

        /// <summary>
        /// GeeTest solving
        /// </summary>
        static void test_geetest()
        {
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            Console.WriteLine("Solving geetest ...");
            var rd = new Dictionary<string, string>();
            rd.Add("domain", "DOMAIN_HERE");
            rd.Add("gt", "GT_HERE");
            rd.Add("challenge", "CHALLENGE_HERE");
            // rd.Add("api_server", "GT_DOMAIN_HERE");           // optional
            // rd.put("user_agent", "your user agent");          // UA used in solving captcha, optional
            // rd.Add("proxy", "user:pass@191.123.43.34:301");   // proxy with/out authentication, optional
            // rd.Add("affiliate_id", "get it from /account");   // optional

            string id = bcs.submit_geetest(rd);
            string solution = "";
            while (solution == "")
            {
                solution = bcs.retrieve(id)["solution"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Solution: {0}", solution));
            // bcs.set_captcha_bad(id);      // set captcha as bad
        }

        /// <summary>
        /// GeeTestV4 solving
        /// </summary>
        static void test_geetestv4()
        {
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            Console.WriteLine("Solving geetestv4 ...");
            var rd = new Dictionary<string, string>();
            rd.Add("domain", "https://example.com");
            rd.Add("captchaid", "647f5ed2ed8acb4be36784e01556bb71");
            // rd.put("user_agent", "your user agent");          // UA used in solving captcha, optional
            // rd.Add("proxy", "user:pass@191.123.43.34:301");   // proxy with/out authentication, optional
            // rd.Add("affiliate_id", "get it from /account");   // optional

            string id = bcs.submit_geetest_v4(rd);
            string solution = "";
            while (solution == "")
            {
                solution = bcs.retrieve(id)["solution"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Solution: {0}", solution));
            // bcs.set_captcha_bad(id);      // set captcha as bad
        }

        /// <summary>
        /// Capy solving
        /// </summary>
        static void test_capy()
        {
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            Console.WriteLine("Solving capy ...");
            var rd = new Dictionary<string, string>();
            rd.Add("page_url", "PAGE_URL_HERE");
            rd.Add("site_key", "SITE_KEY_HERE");
            // rd.put("user_agent", "your user agent");          // UA used in solving captcha, optional
            // rd.Add("proxy", "user:pass@191.123.43.34:301");   // proxy with/out authentication, optional
            // rd.Add("affiliate_id", "get it from /account");   // optional

            string id = bcs.submit_capy(rd);
            string solution = "";
            while (solution == "")
            {
                solution = bcs.retrieve(id)["solution"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Solution: {0}", solution));
            // bcs.set_captcha_bad(id);      // set captcha as bad
        }

        /// <summary>
        /// Capy solving
        /// </summary>
        static void test_hcaptcha()
        {
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            Console.WriteLine("Solving hCaptcha ...");
            var rd = new Dictionary<string, string>();
            rd.Add("page_url", "PAGE_URL_HERE");
            rd.Add("site_key", "SITE_KEY_HERE");
            // rd.Add("invisible", "1");
            // rd.Add("payload", "{\"rqdata\": \"from web requests\"}");
            // rd.Add("user_agent", "your user agent");
            // rd.Add("proxy", "12.34.56.78:1234");
            // rd.Add("affiliate_id", "get it from /account");

            string id = bcs.submit_hcaptcha(rd);
            string solution = "";
            while (solution == "")
            {
                solution = bcs.retrieve(id)["solution"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Solution: {0}", solution));
            // bcs.set_captcha_bad(id);      // set captcha as bad
        }

        /// <summary>
        /// Funcaptcha solving
        /// </summary>
        static void test_funcaptcha()
        {
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            Console.WriteLine("Solving FunCaptcha ...");
            var rd = new Dictionary<string, string>();
            rd.Add("page_url", "PAGE_URL_HERE");
            rd.Add("s_url", "SITE_KEY_HERE");
            rd.Add("site_key", "11111111-1111-1111-1111-111111111111");
            // rd.Add("data", "{\"x\":\"y\"}");      // optional
            // rd.put("user_agent", "your user agent");          // UA used in solving captcha, optional
            // rd.Add("proxy", "user:pass@191.123.43.34:301");   // proxy with/out authentication, optional
            // rd.Add("affiliate_id", "get it from /account");   // optional

            string id = bcs.submit_funcaptcha(rd);
            string solution = "";
            while (solution == "")
            {
                solution = bcs.retrieve(id)["solution"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Solution: {0}", solution));
            // bcs.set_captcha_bad(id);      // set captcha as bad
        }

        /// <summary>
        /// Task solving
        /// </summary>
        static void test_task()
        {
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            Console.WriteLine("Solving task ...");
            var rd = new Dictionary<string, string>();
            rd.Add("template_name", "Login test page");
            rd.Add("page_url", "https://bestcaptchasolver.com/automation/login");
            rd.Add("variables", "{\"username\": \"from C#\", \"password\": \"1234\"}");
            // rd.Add("user_agent", "your user agent");
            // rd.Add("proxy", "12.34.56.78:1234");
            // rd.Add("affiliate_id", "your_affiliate_id");      // optional, get it from /account

            string id = bcs.submit_task(rd);

            // submit pushVariables while task is being solved by the worker
            // very helpful, for e.g. in cases of 2FA authentication
            // bcs.task_push_variables(id, "{\"tfa_code\": \"4612\"}");

            string solution = "";
            while (solution == "")
            {
                solution = bcs.retrieve(id)["solution"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Solution: {0}", solution));
            // bcs.set_captcha_bad(id);      // set captcha as bad
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                // test_image();
                test_recaptcha();
                // test_geetest();
                // test_geetestv4();
                // test_capy();
                // test_hcaptcha();
                // test_funcaptcha();
                // test_task();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error occurred: {0}", ex.ToString()));
            }
            finally
            {
                // disabled for command-line mode
                Console.WriteLine("FINISHED ! Press ENTER to close window ...");
                Console.ReadLine();
            }
        }
    }
}
