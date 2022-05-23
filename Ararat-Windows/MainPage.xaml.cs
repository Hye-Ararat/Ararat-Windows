using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Net;
using RestSharp;
using System.Diagnostics;
using System.Threading;
using System.Net.Security;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LXCLIENT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        RestClient client;
        public MainPage()
        {
            this.InitializeComponent();

            this.shit();
        }
        async void shit()
        {
            var data_path = Windows.Storage.ApplicationData.Current.LocalFolder;
            Debug.WriteLine(data_path.Path);
            if (File.Exists(data_path.Path + "/auth.pfx"))
            {
                byte[] bytes = File.ReadAllBytes(data_path.Path + "/auth.pfx");

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.DefaultConnectionLimit = 9999;
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                System.Net.ServicePointManager.SecurityProtocol =
            SecurityProtocolType.Tls12 |
            SecurityProtocolType.Tls11 |
            SecurityProtocolType.Tls;
                X509Certificate2 cert = new X509Certificate2(bytes, "Boefje2013");


                var opt = new RestClientOptions()
                {
                    ClientCertificates = new X509CertificateCollection() { cert },
                    Proxy = new WebProxy(),
                    BaseUrl = new Uri("https://172.20.20.170:8443/"),
                    ThrowOnAnyError = true,
                    RemoteCertificateValidationCallback = new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; })
                };
                client = new RestClient(opt);

                Debug.WriteLine("Request");
                var restrequest = new RestRequest("/1.0/instances", Method.Get);
                var response = await client.ExecuteAsync(restrequest);
                Debug.WriteLine(response.StatusDescription);
                Debug.WriteLine(response.ResponseStatus);
                Debug.WriteLine(response.ErrorMessage);
                Debug.WriteLine(response.StatusCode);
                textbox.Text = response.Content;
            }
            else
            {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.FileTypeFilter.Add(".pfx");


                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    IBuffer buffer = await FileIO.ReadBufferAsync(file);
                    byte[] bytes = buffer.ToArray();
                    File.WriteAllBytes(data_path.Path + "/auth.pfx", bytes);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                    System.Net.ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls;
                    X509Certificate2 cert = new X509Certificate2(bytes, "Boefje2013");


                    var opt = new RestClientOptions()
                    {
                        ClientCertificates = new X509CertificateCollection() { cert },
                        Proxy = new WebProxy(),
                        BaseUrl = new Uri("https://172.20.20.170:8443/"),
                        ThrowOnAnyError = true,
                        RemoteCertificateValidationCallback = new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; })
                    };
                    client = new RestClient(opt);

                    Debug.WriteLine("Request");
                    var restrequest = new RestRequest("/1.0/instances", Method.Get);
                    var response = await client.ExecuteAsync(restrequest);
                    Debug.WriteLine(response.StatusDescription);
                    Debug.WriteLine(response.ResponseStatus);
                    Debug.WriteLine(response.ErrorMessage);
                    Debug.WriteLine(response.StatusCode);
                    textbox.Text = response.Content;
                }
                else
                {

                }
            }
            

        }
    }
}
