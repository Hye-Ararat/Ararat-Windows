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
using LXCLIENT.Classes;
using Newtonsoft.Json;
using LXCLIENT.Structures;
using LXCLIENT.Pages;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LXCLIENT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Rest client;
        public MainPage()
        {
            this.InitializeComponent();
            this.InitializeClient();
        }
        async void InitializeClient()
        {
            var data_path = Windows.Storage.ApplicationData.Current.LocalFolder;
            Debug.WriteLine(data_path.Path);
            if (File.Exists(data_path.Path + "/auth.pfx"))
            {
                byte[] bytes = File.ReadAllBytes(data_path.Path + "/auth.pfx");
                client = new Rest("https://172.20.20.170:8443", bytes, "Boefje2013");
                this.DisplayInstances();
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
                    client = new Rest("https://172.20.20.170:8443", bytes, "Boefje2013");
                    this.DisplayInstances();
                }
                else
                {

                }
            }


        }
        public async void DisplayInstances()
        {
            var response = await client.GetURL("/1.0/instances?recursion=1");
            var json = JsonConvert.DeserializeObject<Structures.Instance.InstancesResponse>(response.Content);
            foreach (var instance in json.metadata)
            {
                // make a nav menu item for the suit
                NavigationViewItem newMenu = new NavigationViewItem();
                newMenu.Content = instance.name;
                newMenu.Icon = new SymbolIcon(Symbol.XboxOneConsole);

                nav.MenuItems.Add(newMenu);
            }
        }

        private async void nav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string InstanceName = args.InvokedItemContainer.Content.ToString();
            if (InstanceName == "Settings")
            {

                sender.Content = new Settings();
                return;
            }
            var response = await client.GetURL("/1.0/instances/" + InstanceName);
            var json = JsonConvert.DeserializeObject<Structures.Instance.InstanceResponse>(response.Content);
            sender.Content = new InstanceView(InstanceName);
        }
    }
}
