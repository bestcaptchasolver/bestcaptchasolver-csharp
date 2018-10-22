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
        /// <summary>
        /// Test API
        /// </summary>
        static void test_api()
        {
            // get acces token from https://bestcaptchasolver.com/account 
            string access_token = "your_access_token";
            string page_url = "page_url_here";
            string site_key = "site_key_here";

            // initialize api object
            var bcs = new BestCaptchaSolverAPI(access_token);
           
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            // solve image (classic) captcha
            var d = new Dictionary<string, string>();
            Console.WriteLine("Solving image captcha ...");
            d.Add("image", "captcha.jpg");
            //d.Add("case_sensitive", "true");        // sets solving to case sensitive
            //d.Add("affiliate_id", "get it from /account");      // affiliate ID
            var id = bcs.submit_image_captcha(d);
            string image_text = "";
            while(image_text == "")
            {
                image_text = bcs.retrieve(id)["text"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Captcha text: {0}", image_text));

            // solve recaptcha
            Console.WriteLine("Solving recaptcha ...");
            var rd = new Dictionary<string, string>();
            rd.Add("page_url", page_url);
            rd.Add("site_key", site_key);

            // optional parameters
            //rd.Add("type", "1");        // 1 - regular, 2 - invisible, 3 - v3, default: 1
            //rd.Add("v3_action", "home");    // action used when solving v3 reCaptcha
            //rd.Add("v3_min_score", "0.3");  // min score to target when solving v3
            //rd.Add("proxy", "user:pass@191.123.43.34");     // proxy with/out authentication
            //rd.Add("affiliate_id", "get it from /account");

            id = bcs.submit_recaptcha(rd);
            string gresponse = "";
            while (gresponse == "")
            {
                gresponse = bcs.retrieve(id)["gresponse"];
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Gresponse: {0}", gresponse));
            //var proxy_status = bcs.retrieve(id)["proxy_status"];
            // bcs.set_captcha_bad("45");      // set captcha with ID 45 bad
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                Program.test_api();          // test API
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Error occured: {0}", ex.ToString()));
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
