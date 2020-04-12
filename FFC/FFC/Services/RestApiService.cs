﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FFC.Models;
using System.Json;
using System.IO;

namespace FFC.Services
{
    public class RestApiService : IRestApiService
    {
        HttpClient _client;
        //Base address for database
        public static string BaseAddress = "https://fruitflyapi.azurewebsites.net";
        //API key for authentication
        public static string ApiKey = "829320-adajdasd-12vasdas-baslk3";
        //ReferenceURL used as the combined url
        public static string ReferenceUrl = BaseAddress + "/api/Referencepoint";
        
        public ObservableCollection<Reference> Items { get; private set; }
        public Reference Item { get; private set; }

        public RestApiService()
        {
            _client = new HttpClient();
            //Applying the API Key to the Header
            _client.DefaultRequestHeaders.Add("ApiKey", ApiKey);
        }

        //Posts a reference point
        public async Task PostReferenceAsync(Reference item)
        {
            string jsonString = "{\"referencepointId\":0,\"category\":0,\"rssI1\":0," +
                                "\"rssI2\":0,\"rssI3\":0,\"x\":0,\"y\":0,\"heatmapId\":0," +
                                "\"heatmap\":{\"heatmapID\":0,\"strength\":0,\"referencepointId\":0}}";

            //Converting the jsonString to a Json object to modify values
            var details = JsonObject.Parse(jsonString);

            details["x"] = item.x;
            details["y"] = item.y;
            details["rssI1"] = item.rssI1;

            try
            {

                var content = new StringContent(details.ToString(), Encoding.UTF8, "application/json");

                //Posting
                HttpResponseMessage response = await _client.PostAsync(ReferenceUrl, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception message: {0} ", ex.Message);
            }
        }

        //Retrieves all prior dialed reference points 
        public async Task<ObservableCollection<Reference>> RefreshDataAsync()
        {
            Items = new ObservableCollection<Reference>();

            try
            {
                //Getting
                var response = await _client.GetAsync(ReferenceUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<Reference>>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception message: {0} ", ex.Message);
            }

            return Items;
        }

        //Deletes reference point with given ReferencePointID (Unique)
        public async Task DeleteReferenceAsync(string id)
        {
            try
            {
                //Deleting
                var response = await _client.DeleteAsync(ReferenceUrl + "/" + id);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception message: {0} ", ex.Message);
            }
        }

        public async Task<Reference> GetSpecificRefID(string id)
        {
            Item = new Reference();
            try
            {
                var response = await _client.GetAsync(ReferenceUrl + "/" + id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Item = JsonConvert.DeserializeObject<Reference>(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception message: {0} ", ex.Message);
            }

            return Item;
        }
            
    }
}
