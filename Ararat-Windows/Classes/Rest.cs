using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace LXCLIENT.Classes
{
    class Rest
    {
        RestClient client;
        string baseUrl;
        Func<ClientWebSocket> factory;
        public Rest(string baseURL, byte[] bytes, string certPassword)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            X509Certificate2 cert = new X509Certificate2(bytes, certPassword);
            var proxy = new WebProxy();
            baseUrl = baseURL;
            var opt = new RestClientOptions()
            {
                ClientCertificates = new X509CertificateCollection() { cert },
                Proxy = proxy,
                BaseUrl = new Uri(baseURL),
                ThrowOnAnyError = true,
                RemoteCertificateValidationCallback = new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; })
            };
            client = new RestClient(opt);
            factory = new Func<ClientWebSocket>(() => new ClientWebSocket
            {
                Options =
                  {
                     KeepAliveInterval = TimeSpan.FromSeconds(5),
                     Proxy = proxy,
                     ClientCertificates = new X509CertificateCollection() { cert }
                  }
            });

        }
        async public Task<RestResponse> GetURL(string url)
        {
            var restrequest = new RestRequest(url, Method.Get);
            var response = await client.ExecuteAsync(restrequest);
            return response;
        }
        async public Task<RestResponse> PostURL(string url, string body)
        {
            var request = new RestRequest(url, Method.Post) { RequestFormat = RestSharp.DataFormat.Json }
            .AddBody(body);

            var response = await client.ExecuteAsync(request);
            return response;
        }
        async public Task<RestResponse> PatchURL(string url, string body)
        {
            var restrequest = new RestRequest(url, Method.Patch) { RequestFormat = RestSharp.DataFormat.Json }
            .AddBody(body);
            var response = await client.ExecuteAsync(restrequest);
            return response;
        }
        async public Task<RestResponse> DeleteURL(string url)
        {
            var restrequest = new RestRequest(url, Method.Delete);
            var response = await client.ExecuteAsync(restrequest);
            return response;
        }
        public WebsocketClient WebsocketConnect(string url)
        {
            var client = new WebsocketClient(new Uri(baseUrl.Replace("http", "ws") + url), factory);
            client.Start();
            return client;
        }
    }
}
