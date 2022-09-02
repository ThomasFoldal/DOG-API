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
        private List<Breed> breeds;
        public MainWindow()
        {
            InitializeComponent();

            // uses the url to to get a random image of a dog and shows it when the program starts

            var url = "https://dog.ceo/api/breeds/image/random";
            DisplayImage(url);

            url = "https://dog.ceo/api/breeds/list/all";
            IncertBreedsIntoComboBox(url);
        }
        private void NewDogButtonClick(object sender, RoutedEventArgs e)
        {
            var breed = "";
            var subBreed = "";
            var url = "";
            if (sender.Equals(breed_CB))
            {
                var breed_cb = sender as ComboBox;
                breed = breed_cb!.SelectedItem.ToString();
            }
            else if (sender.Equals(subBreed_CB))
            {
                var subBreed_cb = sender as ComboBox;
                if (subBreed_cb!.SelectedItem != null)
                {
                    breed = breed_CB.SelectedItem.ToString();
                    subBreed = subBreed_cb.SelectedItem.ToString();
                }
            }
            else
            {
                breed = breed_CB.SelectedItem.ToString();
                subBreed = subBreed_CB.SelectedItem.ToString();
            }
            if (breed != "")
            {
                if (subBreed != "")
                {
                    url = "https://dog.ceo/api/breed/" + breed + "/" + subBreed + "/images/random";
                }
                else
                {
                    url = "https://dog.ceo/api/breed/" + breed + "/images/random";
                }
                foreach (Breed b in breeds)
                {
                    if (b.breed == breed)
                    {
                        if (b.subBreeds.Count() != 0)
                        {
                            subBreed_CB.IsEnabled = true;
                            subBreed_CB.ItemsSource = b.subBreeds;
                        }
                        else
                        {
                            subBreed_CB.IsEnabled = false;
                            subBreed_CB.ItemsSource = b.subBreeds;
                        }
                    }
                }
            }
            else
            {
                url = "https://dog.ceo/api/breeds/image/random";
            }
            DisplayImage(url);
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
        private void NewImage(object sender, SelectionChangedEventArgs e)
        {
            RoutedEventArgs E = new RoutedEventArgs();
            NewDogButtonClick(sender, E);
        }
        private void DisplayImage(string Url)
        {
            var url = Url;
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
        public void IncertBreedsIntoComboBox(string Url)
        {
            // Gets a list form the url that contains all the breeds and subbreeds of dogs
            var url = Url;
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.Accept = "application/json";

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
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
    }
}
