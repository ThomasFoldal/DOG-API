using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace DOG_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var url = "https://dog.ceo/api/breeds/image/random";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Accept = "application/json";

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Img_Data? data = JsonConvert.DeserializeObject<Img_Data>(result);
                try
                {
                pic_dog.Source = data!.Message;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            

            url = "https://dog.ceo/api/breeds/list/all";
            httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Accept = "application/json";

            httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                List_Data? data = JsonConvert.DeserializeObject<List_Data>(result);
                try
                {
                    breed_CB.ItemsSource = data!.Message;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        private void new_button_Click(object sender, RoutedEventArgs e)
        {
            var breed = breed_CB.SelectionBoxItem;
            var url = "https://dog.ceo/api/breeds/" + breed + "/images/random";

        }
    }
}
