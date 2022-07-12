using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KnabTestSample.Pages.App_Code
{
    public static class CryptoCurrencyLib
    {
        public static string getCurrentQuote(string cryptoCurrencyCode, List<string> currencyCodes)
        {
            string result = "";
            if (!string.IsNullOrEmpty(cryptoCurrencyCode))
            {
                if (cryptoCurrencyCode.Length == 3)
                {
                    result = cryptoCurrencyCode + ": 1\n";
                    foreach (string tempCurrency in currencyCodes)
                    {
                        try
                        {
                            string urlForRequest = @"https://api.apilayer.com/exchangerates_data/convert?to=" + tempCurrency + "&from=" + cryptoCurrencyCode + "&amount=1";
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlForRequest);
                            request.ContentType = "application/json";
                            request.Method = "GET";
                            request.Headers.Add("apikey", "r8nApmDZTWxP8P4ZDFi2soMMRSbYPNPJ"); //Add a valid API Key

                            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                            {
                                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                {
                                    string tempResult = reader.ReadToEnd();

                                    result = result + tempCurrency + ": " + getRateOfCurrency(tempResult) + "\n";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            result = "Error: The CryptoCurrency Code Not Found! Please Enter a Valid Code Like BTC";
                            break;
                        }
                    }
                }
                else
                {
                    result = "Error: You Should Enter a VALID CryptoCurrency Code with 3 Letters";
                }
            }
            else
            {
                result = "Error: You Should Enter a CryptoCurrency Code";
            }
            return result;
        }

        private static string getRateOfCurrency(string raw)
        {
            string result = "0";
            var data = (JObject)JsonConvert.DeserializeObject(raw);
            result = data.SelectToken(
               "info.rate").Value<string>();
            return result;
        }
    }
}
