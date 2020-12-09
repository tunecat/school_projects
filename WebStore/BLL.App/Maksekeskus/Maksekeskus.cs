using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BLL.App.Maksekeskus
{
    public class Maksekeskus
    {
        private string ApiUrl { get; set; }
        private string ShopId { get; set; }
        private string PublishableKey { get; set; }
        private string SecretKey { get; set; }


        public Maksekeskus()
        {
            ApiUrl = "https://api-test.maksekeskus.ee";
            ShopId = "f7741ab2-7445-45f9-9af4-0d0408ef1e4c";
            PublishableKey = "zPA6jCTIvGKYqrXxlgkXLzv3F82Mjv2E";
            SecretKey = "pfOsGD9oPaFEILwqFLHEHkPf7vZz4j3t36nAcufP1abqT9l99koyuC1IWAOcBeqt";
        }

        public async Task<string> CreateTransaction(Dictionary<string, Dictionary<string, dynamic>> body)
        {
            var response = await MakeApiRequest("POST", "/v1/transactions", null, body);

            if (response != null)
            {
                var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(await response.ReadAsStringAsync());
                foreach (var method in  jsonResponse.Where(m => m.Key == "id").AsQueryable().Select(m => m.Value))
                {
                    return method;
                }
                throw new Exception("CUSTOM EXCEPTION - something wrong up there");
            }
            throw new Exception("transaction is null");
        }
        

        public async Task<Dictionary<string,Dictionary<string, string>>> GetPaymentMethods(Dictionary<string, string>? parameters = null )
        {
            var response = await MakeApiRequest("GET", "/v1/methods", parameters);
            if (response != null)
            {
                var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(await response.ReadAsStringAsync());
                var methods = new Dictionary<string,Dictionary<string, string>>();
                foreach (var method in  jsonResponse.Where(m => m.Key == "banklinks")
                    .AsQueryable().Select(m => m.Value))
                {
                    foreach (JObject meth in method.Children<JObject>())
                    {
                        if (meth.GetValue("country").ToString() == "ee")
                        {
                            var dict = new Dictionary<string, string>();
                            dict["logo_url"] = meth.GetValue("logo_url").ToString();
                            dict["url"] = meth.GetValue("url").ToString();
                            methods[meth.GetValue("name").ToString()] = dict;
                        } 
                    }
                }
                return methods;
            }
            return new Dictionary<string, Dictionary<string, string>>();
        } 
        

        public async Task<HttpContent?> MakeApiRequest(string method, string endPoint,
            Dictionary<string, string>? parameters = null,
            Dictionary<string, Dictionary<string, dynamic>>? body = null)
        {
            var uri = ApiUrl + endPoint;

            if (parameters != null && parameters.Count > 0)
            {
                string query = "";
                foreach (var key in parameters.Keys)
                {
                    query += $"{key}={parameters[key]}&";
                }

                uri += "?" + query;
            }

            var authUser = ShopId;
            var authPass = SecretKey;

            var client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes($"{authUser}:{authPass}");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            
            if (method == "GET")
            {
                return (await client.GetAsync(uri)).Content;
            }
            if (method == "POST")
            {
                var k = (await client.PostAsJsonAsync(uri, body));
                return k.Content;
            }
            if (method == "PUT")
            {
                return (await client.PutAsJsonAsync(uri, body)).Content;
            }

            return null;
        }
    }
}