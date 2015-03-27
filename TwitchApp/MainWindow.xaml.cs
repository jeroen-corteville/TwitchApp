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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace TwitchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void wdwMain_Loaded(object sender, RoutedEventArgs e)
        {
            getTwitchGames();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lvwGames.View = lvwGames.FindResource("GameView") as ViewBase;    
            getTwitchGames();
            btnBack.IsEnabled = false;
            lblName.Content = "All Games";
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvwGames.ItemsSource is List<Game>){
                ListViewItem item = sender as ListViewItem;

                Console.WriteLine(item);
                var games = new List<Game>();

                games = (List<Game>)lvwGames.ItemsSource;

                int index = lvwGames.SelectedIndex;
                //this.Close();

                GetStreams(games[index]);
            }
            if (lvwGames.ItemsSource is List<Stream>)
            {
                int index = lvwGames.SelectedIndex;

                var streams = new List<Stream>();
                streams = (List<Stream>)lvwGames.ItemsSource;

                Console.WriteLine(streams[index].Url);
                string strCmdText;
                strCmdText = streams[index].Url + " best";
                Console.WriteLine(strCmdText);
                System.Diagnostics.Process.Start("livestreamer", strCmdText);
            }
            
            
            
            
        }

        private async void GetStreams(Game game)
        {
            
            btnBack.IsEnabled = true;
            Console.WriteLine(game.GameName);
            using (var httpClient = new HttpClient())
            {
                String url = "https://api.twitch.tv/kraken/streams?game=";
                url += game.GameName;
                Console.WriteLine(Uri.EscapeUriString(url));
                var streamsForGame = await httpClient.GetStringAsync(url);
                JObject JsonStreamsForGame = JObject.Parse(streamsForGame);
                //Console.WriteLine(JsonStreamsForGame);

                var streams = new List<Stream>();

                for (int i = 0; i < 25; i++)
                {
                    Game streamGame = game;
                    int viewcount = (int)JsonStreamsForGame["streams"][i]["viewers"];
                    String preview = (String)JsonStreamsForGame["streams"][i]["preview"]["small"];
                    String streamurl = (String)JsonStreamsForGame["streams"][i]["channel"]["url"];
                    String status = (String)JsonStreamsForGame["streams"][i]["channel"]["status"];
                    String channel = (String)JsonStreamsForGame["streams"][i]["channel"]["display_name"];


                    Stream stream = new Stream(game, viewcount, preview, streamurl, status,channel);
                    Console.WriteLine(stream.Status);
                    streams.Add(stream);
                }
                lblName.Content = "Displaying Streams for: " + game.GameName;
                lvwGames.ItemsSource = streams;
                lvwGames.View = lvwGames.FindResource("StreamView") as ViewBase;
            }
            
        }
        private async void getTwitchGames()
        {
            using (var httpClient = new HttpClient()){
                var Games = await httpClient.GetStringAsync("https://api.twitch.tv/kraken/games/top");
                JObject JsonGames = JObject.Parse(Games);
                var games = new List<Game>();
                //Console.WriteLine(JsonGames);
                for (int i = 0; i < 10; i++)
                {
                    String gameName = (String)JsonGames["top"][i]["game"]["name"];
                    int viewers = (int)JsonGames["top"][i]["viewers"];
                    int channels = (int)JsonGames["top"][i]["channels"];
                    String logo = (String)JsonGames["top"][i]["game"]["box"]["small"];
                    Game game = new Game(gameName, viewers, channels, logo);
                    Console.WriteLine(game.Viewers);
                    games.Add(game);
                }
                lvwGames.ItemsSource = games;
                lvwGames.View = lvwGames.FindResource("GameView") as ViewBase;
            }
        }

        
    }
}
