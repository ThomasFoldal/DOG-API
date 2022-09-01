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
using System.Collections;

namespace DOG_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal List<Breed> breeds;
        public MainWindow()
        {
            InitializeComponent();

            // uses the url to to get a random image of a dog and shows it when the program starts

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

            // Gets a list form the url that contains all the breeds and subbreeds of dogs

            url = "https://dog.ceo/api/breeds/list/all";
            httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Accept = "application/json";

            httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                breeds = JsonRead(result); // makes the JSON string into a list of objekts that i can work with
                List<string> breed_list = new List<string>();
                foreach (Breed b in breeds)
                {
                    breed_list.Add(b.breed);
                }
                breed_CB.ItemsSource = breed_list; // makes the combobox able to select any of the breeds, but not the subbreeds
            }
        }
        private void new_button_Click(object sender, RoutedEventArgs e)
        {
            var breed = breed_CB.SelectionBoxItem;
            var subBreed = subBreed_CB.SelectionBoxItem;
            var url = "";
            if (breed != "")
            {
                if (subBreed != "")
                {
                    foreach (Breed b in breeds)
                    {
                        if (b.breed == breed)
                        {
                            if (b.subBreeds.Count() != 0)
                            {
                                subBreed_row.IsEnabled = true;
                                subBreed_CB.ItemsSource = b.subBreeds;
                            }
                        }
                    }
                }
                else
                {
                    url = "https://dog.ceo/api/breed/" + breed + "/images/random";
                }
            }
            else
            {
                url = "https://dog.ceo/api/breeds/image/random";
            }
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
        }
        public static List<Breed> JsonRead(string input)
        {
            char[] chars = new char[] { '{', '}' };
            List<string> split = input.Split(chars,StringSplitOptions.TrimEntries).ToList();
            for (int i = 0; i < split.Count; i++)
            {
                if (split[i].Contains("message") || split[i].Contains("status"))
                {
                    split.RemoveAt(i);
                    i--;
                }
            }
            chars = new char[] { '[', ']' };
            List<string> breeds = split[1].Split(chars,StringSplitOptions.None).ToList();
            Dictionary<string, string> test = new Dictionary<string, string>();
            List<Breed> options = new List<Breed>();
            for (int i = 0; i < breeds.Count()-1; i+=2)
            {
                test.Add(breeds[i], breeds[i + 1]);
            }
            foreach (KeyValuePair<string,string> j in test)
            {
                options.Add(new Breed(j.Key, j.Value.Split(',',StringSplitOptions.RemoveEmptyEntries)));
            }
            return options;
        }
        private void new_immage(object sender, SelectionChangedEventArgs e)
        {
            RoutedEventArgs E = new RoutedEventArgs();
            new_button_Click(sender, E);
        }
    }
}
