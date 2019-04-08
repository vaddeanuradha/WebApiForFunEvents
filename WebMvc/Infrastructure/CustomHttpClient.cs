using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebMvc.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        private ILogger<CustomHttpClient> _logger;
        private HttpClient _client;
        //private IHttpContextAccessor _httpContextAccessor;
        public CustomHttpClient(ILogger<CustomHttpClient> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            //_httpContextAccessor = httpContextAccessor;
        }
        Task<HttpResponseMessage> IHttpClient.DeleteAsync(string uri, string authorizationToken, string authorizationMethod)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetStringAsync(string uri, string authorizationToken, string authorizationMethod)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            // SetAuthorizationHeader(requestMessage);
            if (authorizationToken != null)
            {


                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            var response = await _client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();
        }

        //private void SetAuthorizationHeader(HttpRequestMessage requestMessage)
        //{
        //    var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        //    if (!string.IsNullOrEmpty(authorizationHeader))
        //    {
        //        requestMessage.Headers.Add("Authorization", new List<string> { authorizationHeader });
        //    }
        //}
        Task<HttpResponseMessage> IHttpClient.PostAsync<T>(string uri, T item, string authorizationToken, string authorizationMethod)
        {
            throw new NotImplementedException();
        }

        Task<HttpResponseMessage> IHttpClient.PutAsync<T>(string uri, T item, string authorizationToken, string authorizationMethod)
        {
            throw new NotImplementedException();
        }
    }
}
