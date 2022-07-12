using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KnabTestSample.Pages.App_Code;

namespace KnabTestSample.Pages
{
    public class CryptoCurrencyModel : PageModel
    {
        public string cryptoCurrencyCode = "";
        public string currentQuote = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            cryptoCurrencyCode = Request.Form["txtCryptoCurrencyCode"];
            List<string> currencyCodes = new List<string>();
            currencyCodes.Add("USD");
            currencyCodes.Add("EUR");
            currencyCodes.Add("BRL");
            currencyCodes.Add("GBP");
            currencyCodes.Add("AUD");

            currentQuote = CryptoCurrencyLib.getCurrentQuote(cryptoCurrencyCode, currencyCodes);
        }
    }
}
