BestCaptchaSolver API wrapper C#
=========================================
bestcaptchasolverapi is a super easy to use bypass captcha API wrapper for bestcaptchasolver.com captcha service

## Installation

    git clone https://github.com/bestcaptchasolver/bestcaptchasolver-csharp

## How to use?

Simply import the library, set the auth details and start using the captcha service:

``` csharp
using bestcaptchasolver;
```
Set access_token or username and password (legacy) for authentication

``` csharp
string access_token = "your_access_key";
var bcs = new BestCaptchaSolverAPI(access_token);
```

Once you've set your authentication details, you can start using the API

## Get balance

``` csharp
string balance = bcs.account_balance();
Console.WriteLine(string.Format("Balance: {0}", balance));
```

## Submit image captcha

``` csharp
var d = new Dictionary<string, string>();
d.Add("image", "captcha.jpg");	   // file on disk, or b64 encoded string
// d.Add("is_case", "true");       // case sensitive, default: false, optional
// d.Add("is_phrase", "true");     // contains at least one space, default: false, optional
// d.Add("is_math", "true");       // math calculation captcha, default: false, optional
// d.Add("alphanumeric", "2");     // 1 (digits only) or 2 (letters only), default: all characters
// d.Add("minlength", "3");        // minimum length of captcha text, default: any
// d.Add("maxlength", "4");        // maximum length of captcha text, default: any
// d.Add("affiliate_id", "get it from /account");      // affiliate ID
var id = bcs.submit_image_captcha(d);			// use ID to retrieve `text`
```


## Submit recaptcha details

For recaptcha submission there are two things that are required.
- page_url
- site_key
- type (optional, defaults to 1 if not given)
  - `1` - v2
  - `2` - invisible
  - `3` - v3
  - `4` - enterprise v2
  - `5` - enterprise v3
- v3_action (optional)
- v3_min_score (optional)
- data_s (optional)
- cookie_input (optional)
- user_agent (optional)
- affiliate_id (optional)
- proxy (optional)

For more details about the parameters check [/api](https://bestcaptchasolver.com/api) page

``` csharp
var rd = new Dictionary<string, string>();
rd.Add("page_url", page_url);
rd.Add("site_key", site_key);
// rd.Add("type", "1");        // 1 - regular, 2 - invisible, 3 - v3, default: 1
// rd.Add("v3_action", "home");    // action used when solving v3 reCaptcha
// rd.Add("v3_min_score", "0.3");  // min score to target when solving v3
// rd.Add("data_s", "recaptcha data-s parameter used in loading reCAPTCHA");
// rd.Add("proxy", "user:pass@191.123.43.34");     // proxy with/out authentication
// rd.Add("affiliate_id", "get it from /account");
var id = bcs.submit_recaptcha(rd);		// use ID to retrieve `gresponse`
```
Same as before, this returns an ID which is used to regulary check for completion


## Submit Geetest
- domain
- gt
- challenge
- api_server (optional)

```csharp
var rd = new Dictionary<string, string>();
rd.Add("domain", "DOMAIN_HERE");
rd.Add("gt", "GT_HERE");
rd.Add("challenge", "CHALLENGE_HERE");
// rd.Add("api_server", "GT_DOMAIN_HERE"); // optional
// rd.Add("affiliate_id", "get it from /account");

string id = bcs.submit_geetest(rd);		// use ID to get solution
```

## Submit GeetestV4

- domain
- captchaid

**Important:** This is not the captchaid that's in our system that you receive while submitting a captcha. Gather this from HTML source of page with geetestv4 captcha, inside the `<script>` tag you'll find a link that looks like this: https://i.imgur.com/XcZd47y.png

```csharp
var rd = new Dictionary<string, string>();
rd.Add("domain", "https://example.com");
rd.Add("captchaid", "647f5ed2ed8acb4be36784e01556bb71");
// rd.Add("affiliate_id", "get it from /account");

string id = bcs.submit_geetest_v4(rd);		// use ID to get solution
```

## Submit Capy

- page_url
- site_key

```csharp
var rd = new Dictionary<string, string>();
rd.Add("page_url", "PAGE_URL_HERE");
rd.Add("site_key", "SITE_KEY_HERE");
// rd.Add("affiliate_id", "get it from /account");

string id = bcs.submit_capy(rd);		// use ID to get solution
```

## Submit hCaptcha
- page_url
- site_key

```csharp
var rd = new Dictionary<string, string>();
rd.Add("page_url", "PAGE_URL_HERE");
rd.Add("site_key", "SITE_KEY_HERE");
// rd.Add("affiliate_id", "get it from /account");

string id = bcs.submit_hcaptcha(rd);		// use ID to get solution
```

## Submit FunCaptcha (Arkose Labs)
- page_url
- s_url
- site_key

```csharp
var rd = new Dictionary<string, string>();
rd.Add("page_url", "https://abc.com");
rd.Add("s_url", "https://api.arkoselabs.com");
rd.Add("site_key", "11111111-1111-1111-1111-111111111111");
// rd.Add("data", "{\"x\":\"y\"}");      // optional
// rd.Add("affiliate_id", "your_affiliate_id");      // optional, get it from /account

string id = bcs.submit_funcaptcha(rd);
```

## Submit Task
- template_name
- page_url
- variables

```csharp
var rd = new Dictionary<string, string>();
rd.Add("template_name", "Login test page");
rd.Add("page_url", "https://bestcaptchasolver.com/automation/login");
rd.Add("variables", "{\"username\": \"from C#\", \"password\": \"1234\"}");
// rd.Add("user_agent", "your user agent");
// rd.Add("proxy", "12.34.56.78:1234");
// rd.Add("affiliate_id", "your_affiliate_id");      // optional, get it from /account

string id = bcs.submit_task(rd);
```

#### Task pushVariables
Update task variables while it is being solved by the worker. Useful when dealing with data / variables, of which
value you don't know, only after a certain step or action of the task. For example, in websites that require 2 factor
authentication code.

When the task (while running on workers machine) is getting to an action defined in the template, that requires a variable, but variable was not
set with the task submission, it will wait until the variable is updated through push.

The `bcs.task_push_variables(captcha_id, push_variables)` method can be used as many times as it is needed.

```csharp
bcs.task_push_variables(id, "{\"tfa_code\": \"4612\"}");
```


## Retrieve (all captchas)

Use the retrieve method to retrieve text (image captcha), gresponse (reCAPTCHA) or solution (GeeTest and Capy)

```csharp
id = bcs.submit_recaptcha(page_url, site_key);
string gresponse = "";
while (gresponse == "")
{
     gresponse = bcs.retrieve(id)["gresponse"];		// can be also `text` or `solution`
     Thread.Sleep(5000);
}
```


## If reCAPTCHA is submitted with proxy, get proxy status

``` csharp
var proxy_status = bcs.retrieve(id)["proxy_status"];
```

## Set captcha bad

In case a captcha was wrongly completed, you can use the `set_captcha_bad(captchaID)` method like this
```csharp
bcs.set_captcha_bad("45");
```


## Examples
Compile and run the **example** project in solution

## Binary
If you don't want to compile your own library, you can check the binary folder for a compiled version.
**Keep in mind** that this might not be the latest version with every release

## License
API library is licensed under the MIT License

## More information
More details about the server-side API can be found [here](https://bestcaptchasolver.com)


<sup><sub>captcha, bypasscaptcha, decaptcher, decaptcha, 2captcha, deathbycaptcha, anticaptcha, 
bypassrecaptchav2, bypassnocaptcharecaptcha, bypassinvisiblerecaptcha, captchaservicesforrecaptchav2, 
recaptchav2captchasolver, googlerecaptchasolver, recaptchasolverpython, recaptchabypassscript, bestcaptchasolver</sup></sub>
