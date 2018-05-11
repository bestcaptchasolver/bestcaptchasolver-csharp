BestCaptchaSolver API wrapper C#
=========================================
bestcaptchasolverapi is a super easy to use bypass captcha API wrapper for bestcaptchasolver.com captcha service

## Installation
    Install-Package bestcaptchasolver

or
    
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
var id = bcs.submit_image_captcha("captcha.jpg");
```
Takes a 2nd argument, **case_sensitive** which is a bool
``` csharp
var id = bcs.submit_image_captcha("captcha.jpg", true);
```

## Retrieve image captcha text

Once you have the captchaID, you can check for it's completion
```
var id = bcs.submit_image_captcha("captcha.jpg");  // submit it and get id
string image_text = null;
while(image_text == null)
{
    image_text = bcs.retrieve(id);
    Thread.Sleep(2000);
}
```

## Submit recaptcha details

For recaptcha submission there are two things that are required.
- page_url
- site_key
- proxy (optional), works in this format `12.34.56.78:1234` or `user:password@12.34.56.78:1234`

``` csharp
id = bcs.submit_recaptcha(page_url, site_key);
```
Same as before, this returns an ID which is used to regulary check for completion

## Retrieve captcha response

```csharp
id = bcs.submit_recaptcha(page_url, site_key);
string gresponse = null;
while (gresponse == null)
{
     gresponse = bcs.retrieve(id);
     Thread.Sleep(5000);
}
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
