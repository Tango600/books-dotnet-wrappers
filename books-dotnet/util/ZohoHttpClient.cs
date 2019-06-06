﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using zohobooks.exceptions;
using zohobooks.model;
using zohobooks.parser;

namespace zohobooks.util
{
    /// <summary>
    /// Class ZohoHttpClient.
    /// </summary>
    class ZohoHttpClient
    {
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <returns>HttpClient object.</returns>
        static HttpClient getClient()
        {
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 60);
            client.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
            client.DefaultRequestHeaders.Add("User-Agent", "ZohoBooks-dotnet-Wrappers/1.0");
            return client;
        }
        /// <summary>
        /// Make a query string from the given parameters.
        /// </summary>
        /// <param name="url">Service URL passed by the user.</param>
        /// <param name="parameters">The parameters contains the query string parameters in the form of key, value pair.</param>
        /// <returns> Returns the Query String</returns>
        static string getQueryString(string url, Dictionary<object, object> parameters)
        {
            var ub = new UriBuilder(url);
            var param = HttpUtility.ParseQueryString(ub.Query);
            foreach (var parameter in parameters.Where(f => f.Key.ToString() != "authtoken"))
                param.Add(parameter.Key.ToString(), parameter.Value.ToString());
            ub.Query = param.ToString();
            return ub.ToString();
        }

        /// <summary>
        /// Makes a GET request and fetch the responce for the given URL and Query Parameters.
        /// </summary>
        /// <param name="url">Service URL passed by the user.</param>
        /// <param name="parameters">The parameters contains the query string parameters in the form of key, value pair.</param>
        /// <returns>HttpResponseMessage which contains the data in the form of JSON .</returns>
        /// <exception cref="BooksException">Throws the Exception with error messege return from the server.</exception>
        public static HttpResponseMessage get(string url, Dictionary<object, object> parameters)
        {
            var client = getClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            if (parameters.ContainsKey("authtoken"))
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken { parameters["authtoken"] }");
            var responce = client.GetAsync(getQueryString(url, parameters)).Result;
            if (responce.IsSuccessStatusCode)
                return responce;
            else
                throw new BooksException(responce);
        }

        /// <summary>
        /// Makes a POST request and creates a resource for the given URL and a request body.
        /// </summary>
        /// <param name="url">Service URL passed by the user.</param>
        /// <param name="requestBody">It contains the request body parameters to make the POST request.</param>
        /// <returns>HttpResponseMessage which contains the data in the form of JSON .</returns>
        /// <exception cref="BooksException">Throws the Exception with error messege return from the server.</exception>
        public static HttpResponseMessage post(string url, Dictionary<object, object> requestBody)
        {
            var client = getClient();
            if (requestBody.ContainsKey("authtoken"))
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken { requestBody["authtoken"] }");
            List<KeyValuePair<string, string>> contentBody = new List<KeyValuePair<string, string>>();
            foreach (var requestbodyParam in requestBody)
            {
                var temp = new KeyValuePair<string, string>(requestbodyParam.Key.ToString(), requestbodyParam.Value.ToString());
                contentBody.Add(temp);
            }
            var content = new FormUrlEncodedContent(contentBody);
            var responce = client.PostAsync(url, content).Result;
            if (responce.IsSuccessStatusCode)
                return responce;
            else
                throw new BooksException(responce);
        }

        /// <summary>
        /// Makes a POST request and creates a resource for the given URL and a MultiPart form data.
        /// </summary>
        /// <param name="url">Service URL passed by the user.</param>
        /// <param name="parameters">The parameters contains the query string parameters in the form of key, value pair.</param>
        /// <param name="requestBody">It contains the request body parameters to make the POST request.</param>
        /// <param name="attachments">It contains the files to attach or post for the requested URL .</param>
        /// <returns>HttpResponseMessage which contains the data in the form of JSON .</returns>
        /// <exception cref="BooksException">Throws the Exception with error messege return from the server.</exception>
        public static HttpResponseMessage post(string url, Dictionary<object, object> parameters, Dictionary<object, object> requestBody, KeyValuePair<string, string[]> attachments)
        {
            var client = getClient();
            if (parameters.ContainsKey("authtoken"))
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken { parameters["authtoken"] }");

            var boundary = DateTime.Now.Ticks.ToString();
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            MultipartFormDataContent content = new MultipartFormDataContent("--boundary--");
            if (requestBody != null)
                foreach (var requestbodyParam in requestBody)
                    content.Add(new StringContent(requestbodyParam.Value.ToString()), requestbodyParam.Key.ToString());
            if (attachments.Value != null)
            {
                foreach (var file_path in attachments.Value)
                    if (file_path != null)
                    {
                        string _filename = Path.GetFileName(file_path);
                        FileStream fileStream = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.Read);
                        StreamContent fileContent = new StreamContent(fileStream);
                        content.Add(fileContent, attachments.Key, _filename);
                    }
            }
            var responce = client.PostAsync(getQueryString(url, parameters.Where(p => p.Key.ToString() != "authtoken").ToDictionary(p => p.Key, v => v.Value)), content).Result;
            if (responce.IsSuccessStatusCode)
                return responce;
            else
                throw new BooksException(responce);
        }

        /// <summary>
        /// Makes a PUT request and update the resource for the given URL and a request body.
        /// </summary>
        /// <param name="url">Service URL passed by the user.</param>
        /// <param name="requestBody">It contains the request body parameters to make the PUT request.</param>
        /// <returns>HttpResponseMessage which contains the data in the form of JSON .</returns>
        /// <exception cref="BooksException">Throws the Exception with error messege return from the server.</exception>
        public static HttpResponseMessage put(string url, Dictionary<object, object> requestBody)
        {
            var client = getClient();
            if (requestBody.ContainsKey("authtoken"))
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken { requestBody["authtoken"] }");
            List<KeyValuePair<string, string>> contentBody = new List<KeyValuePair<string, string>>();
            foreach (var requestbodyParam in requestBody)
            {
                var temp = new KeyValuePair<string, string>(requestbodyParam.Key.ToString(), requestbodyParam.Value.ToString());
                contentBody.Add(temp);
            }
            var content = new FormUrlEncodedContent(contentBody);
            var responce = client.PutAsync(url, content).Result;
            if (responce.IsSuccessStatusCode)
                return responce;
            else
                throw new BooksException(responce);
        }

        /// <summary>
        /// Makes a PUT request and update the resource for the given URL and a request body.
        /// </summary>
        /// <param name="url">Service URL passed by the user.</param>
        /// <param name="parameters">The parameters contains the query string parameters in the form of key, value pair.</param>
        /// <param name="requestBody">It contains the request body parameters to make the POST request.</param>
        /// <param name="attachment">It contains the files to attach or post for the requested URL.</param>
        /// <returns>HttpResponseMessage which contains the data in the form of JSON.</returns>
        /// <exception cref="BooksException">Throws the Exception with error messege return from the server.</exception>
        public static HttpResponseMessage put(string url, Dictionary<object, object> parameters, Dictionary<object, object> requestBody, KeyValuePair<string, string> attachment)
        {
            var client = getClient();
            if (parameters.ContainsKey("authtoken"))
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken { parameters["authtoken"] }");
            var boundary = DateTime.Now.Ticks.ToString();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            MultipartFormDataContent content = new MultipartFormDataContent(boundary);
            foreach (var requestBodyParam in requestBody)
                content.Add(new StringContent(requestBodyParam.Value.ToString()), requestBodyParam.Key.ToString());
            if (attachment.Value != null)
            {
                string _filename = Path.GetFileName(attachment.Value);
                FileStream fileStream = new FileStream(attachment.Value, FileMode.Open, FileAccess.Read, FileShare.Read);
                StreamContent fileContent = new StreamContent(fileStream);
                content.Add(fileContent, attachment.Key, _filename);
            }
            var responce = client.PutAsync(getQueryString(url, parameters.Where(p => p.Key.ToString() != "authtoken").ToDictionary(p => p.Key, v => v.Value)), content).Result;
            if (responce.IsSuccessStatusCode)
                return responce;
            else
                throw new BooksException(responce);
        }


        /// <summary>
        /// Make a DELETE request for the given URL and a query string.
        /// </summary>
        /// <param name="url">Service URL passed by the user.</param>
        /// <param name="parameters">The parameters contains the query string parameters in the form of key, value pair.</param>
        /// <returns>HttpResponseMessage which contains the data in the form of JSON.</returns>
        /// <exception cref="BooksException">Throws the Exception with error messege return from the server.</exception>
        public static HttpResponseMessage delete(string url, Dictionary<object, object> parameters)
        {
            var client = getClient();
            if (parameters.ContainsKey("authtoken"))
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken { parameters["authtoken"] }");
            var responce = client.DeleteAsync(getQueryString(url, parameters)).Result;
            if (responce.IsSuccessStatusCode)
                return responce;
            else
                throw new BooksException(responce);
        }

        /// <summary>
        /// Gets the file data for the given GET request .
        /// </summary>
        /// <param name="url">Service URL passed by the user.</param>
        /// <param name="parameters">The parameters contains the query string parameters in the form of key, value pair.</param>
        /// <exception cref="BooksException">Throws the Exception with error messege return from the server.</exception>
        public static void getFile(string url, Dictionary<object, object> parameters)
        {
            var client = getClient();
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            if (parameters.ContainsKey("authtoken"))
                client.DefaultRequestHeaders.Add("Authorization", $"Zoho-oauthtoken { parameters["authtoken"] }");
            var responce = client.GetAsync(getQueryString(url, parameters)).Result;
            if (responce.IsSuccessStatusCode)
            {
                string contentDisposition = responce.Content.Headers.ContentDisposition.ToString();
                const string contentFileNamePortion = "filename=\"";
                var fileNameStartIndex = contentDisposition.IndexOf(contentFileNamePortion, StringComparison.InvariantCulture) + contentFileNamePortion.Length;
                var originalFileNameLength = contentDisposition.Length - fileNameStartIndex - 1;
                var filename = contentDisposition.Substring(fileNameStartIndex, originalFileNameLength);
                FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                responce.Content.CopyToAsync(fileStream);
                fileStream.Close();
                Process.Start(filename);
            }
            else
                throw new BooksException(responce);
        }
    }
}
