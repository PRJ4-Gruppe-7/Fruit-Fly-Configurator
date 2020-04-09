using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FFC.Models;

namespace FFC.Services
{
    public class RestApiService : IRestApiService
    {
        HttpClient _client;
        public static string BaseAddress = "https://fruitflyapi.azurewebsites.net";
        public static string AuthKey = "829320-adajdasd-12vasdas-baslk3";
        public static string ReferenceUrl = BaseAddress + "/api/Referencepoint?api_key=" + AuthKey;
        
        public List<Reference> Items { get; private set; }

        public RestApiService()
        {
            _client = new HttpClient();
        }

        public async Task PostReferenceAsync(Reference item)
        {
            var uri = new Uri(string.Format(ReferenceUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\Reference Point successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<Reference>> RefreshDataAsync()
        {
            Items = new List<Reference>();

            var uri = new Uri(string.Format(ReferenceUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Reference>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }
    }
}
