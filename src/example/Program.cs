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
            string page_url = "recaptcha_page_url";
            string site_key = "recaptcha_site_key";

            // initialize api object
            var bcs = new BestCaptchaSolverAPI(access_token);
           
            // account balance
            string balance = bcs.account_balance();
            Console.WriteLine(string.Format("Balance: {0}", balance));

            // solve image (classic) captcha
            Console.WriteLine("Solving image captcha ...");
            var id = bcs.submit_image_captcha("captcha.jpg");
            string image_text = null;
            while(image_text == null)
            {
                image_text = bcs.retrieve(id);
                Thread.Sleep(2000);
            }
            Console.WriteLine(string.Format("Captcha text: {0}", image_text));

            // solve recaptcha
            Console.WriteLine("Solving recaptcha ...");
            id = bcs.submit_recaptcha(page_url, site_key);
            string gresponse = null;
            while (gresponse == null)
            {
                gresponse = bcs.retrieve(id);
                Thread.Sleep(5000);
            }
            Console.WriteLine(string.Format("Gresponse: {0}", gresponse));

            // bcs.submit_image_captcha("captcha.jpg", true);      // case sensitive
            // bcs.submit_recaptcha(page_url, site_key, "127.0.0.1:8080");     // HTTP proxy
            // bcs.submit_recaptcha(page_url, site_key, "user:password@127.0.0.1:8080");     // HTTP proxy with auth
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
