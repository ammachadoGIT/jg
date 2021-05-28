﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        static string _url = "";

        public JsonFeed() { }
        public JsonFeed(string endpoint, int results)
        {
            _url = endpoint;
        }

        public static string[] GetRandomJokes(string firstname, string lastname, string category)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            var url = "jokes/random";
            if (category != null)
            {
                if (url.Contains('?'))
                    url += "&";
                else url += "?";
                url += "category=";
                url += category;
            }

            var joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

            if (firstname != null && lastname != null)
            {
                var index = joke.IndexOf("Chuck Norris");
                var firstPart = joke.Substring(0, index);
                var secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
                joke = firstPart + " " + firstname + " " + lastname + secondPart;
            }

            return new string[] { JsonConvert.DeserializeObject<dynamic>(joke).value };
        }

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static dynamic Getnames()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            var result = client.GetStringAsync("").Result;
            return JsonConvert.DeserializeObject<dynamic>(result);
        }

        public static string[] GetCategories()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_url);

            return new string[] { Task.FromResult(client.GetStringAsync("categories").Result).Result };
        }
    }
}
