using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SzkolnikMobileApp.Code
{
    public static class HTTPRequest
    {
        public static string api;

        public static string Get(string uri)
        {
            if (api == string.Empty)
                throw new Exception("API link is not set");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api + uri);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static async void Post(string uri,object postJson)
        {
            var json = JsonConvert.SerializeObject(postJson);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = api+uri;
            var client = new HttpClient();

            await client.PostAsync(url, data);
        }
    }
}
