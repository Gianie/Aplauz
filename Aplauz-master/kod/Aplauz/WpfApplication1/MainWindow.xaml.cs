using System;
using System.Collections;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Coin> coins = new List<Coin>();
        List<Coin> coinsLifted = new List<Coin>();
        List<Player> players = new List<Player>();
        List<Mine> mines = new List<Mine>();
        private int currentPlayer=0;
        private int playersCount = 4;
        private int turn = 0;

        private Label[] names = new Label[4];
        private Border[] borders = new Border[4];
        Dictionary<string, Button> coinButtons = new Dictionary<string, Button>();
        Dictionary<string, Button> mineButtons = new Dictionary<string, Button>();


        public MainWindow()
        {
            InitializeComponent();
            PopulateLabels();
            PopulatePlayers(playersCount);
            PopulateCoins();
            PopulateMines();

            PutMine(3, 1);
            
        }


        private void PutMine(int level, int column)
        {
            Random rnd = new Random();
            bool temp = true;
            Mine m = mines[0];
            int key = 0;
            while (temp)
            {
                key = rnd.Next(0, mines.Count());
                m = mines[key];
                if (m.level == level)
                    temp = false;
            }

            string content = m.level + m.color + m.prestige + m.Price["white"] + m.Price["blue"] + m.Price["green"] + m.Price["red"] + m.Price["black"];

            mines.RemoveAt(key);

            string coords = level.ToString() + column.ToString();
            mineButtons[coords].Content = content;


        }

        private void PopulateMines()
        {
            int counter = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader("Mines.txt");
            while ((line = file.ReadLine()) != null)
            {
                int level = line[0] - 48;
                string scolor;

                if (line[1] == 'k') scolor = "black";
                else if (line[1] == 'w') scolor = "white";
                else if (line[1] == 'u') scolor = "blue";
                else if (line[1] == 'g') scolor = "green";
                else if (line[1] == 'r') scolor = "red";
                else scolor = "none";

                int prestige = line[2] -48;

                string prices = "";
                prices += line[3]-48;
                prices += line[4] - 48;
                prices += line[5] - 48;
                prices += line[6] - 48;
                prices += line[7] - 48;

                Mine m = new Mine(level, scolor, prestige, prices);

                mines.Add(m);
                //Console.WriteLine(line);
                counter++;
            }

            file.Close();
        }
        private void PopulateLabels()
        {
            names[0] = name1;
            names[1] = name2;
            names[2] = name3;
            names[3] = name4;

            borders[0] = border1;
            borders[1] = border2;
            borders[2] = border3;
            borders[3] = border4;

            coinButtons.Add("white", coinWhite);
            coinButtons.Add("black", coinBlack);
            coinButtons.Add("red", coinRed);
            coinButtons.Add("green", coinGreen);
            coinButtons.Add("blue", coinBlue);

            mineButtons.Add("11", mine11);
            mineButtons.Add("12", mine12);
            mineButtons.Add("13", mine13);
            mineButtons.Add("21", mine21);
            mineButtons.Add("22", mine22);
            mineButtons.Add("23", mine23);
            mineButtons.Add("31", mine31);
            mineButtons.Add("32", mine32);
            mineButtons.Add("33", mine33);
        }

        private void PopulateCoins()
        {
            for (int i = 0; i < 7; i++)
            {
                
                coins.Add(new Coin("blue"));
                coins.Add(new Coin("green"));
                coins.Add(new Coin("black"));
                coins.Add(new Coin("red"));
                coins.Add(new Coin("white"));
            }
            for (int i =0; i<5; i++)
            {
                coins.Add(new Coin("gold"));
            }
            CountAllCoins();
        }

        private void PopulatePlayers(int quantity)
        {
            for (int i =0; i<quantity; i++)
            {
                Player p = new Player("temp_p" + (i +1));
                players.Add(p);

                names[i].Content = players[i].name;
                
                
            }
            borders[currentPlayer].BorderBrush = System.Windows.Media.Brushes.Red;



        }

        private void CountAllCoins()
        {
            coinBlue.Content = coins.Where(i => i.Color == "blue").Count();
            coinWhite.Content = coins.Where(i => i.Color == "white").Count();
            coinBlack.Content = coins.Where(i => i.Color == "black").Count();
            coinGreen.Content = coins.Where(i => i.Color == "green").Count();
            coinRed.Content = coins.Where(i => i.Color == "red").Count();
            coinGold.Content = coins.Where(i => i.Color == "gold").Count();


            coinsBlueLifted.Content = coinsLifted.Where(i => i.Color == "blue").Count();
            coinsWhiteLifted.Content = coinsLifted.Where(i => i.Color == "white").Count();
            coinsBlackLifted.Content = coinsLifted.Where(i => i.Color == "black").Count();
            coinsGreenLifted.Content = coinsLifted.Where(i => i.Color == "green").Count();
            coinsRedLifted.Content = coinsLifted.Where(i => i.Color == "red").Count();
            coinsGoldLifted.Content = coinsLifted.Where(i => i.Color == "gold").Count();


            coins1Blue.Content = players[0].CountCoins("blue");
            coins1White.Content = players[0].CountCoins("white");
            coins1Black.Content = players[0].CountCoins("black");
            coins1Green.Content = players[0].CountCoins("green");
            coins1Red.Content = players[0].CountCoins("red");
            coins1Gold.Content = players[0].CountCoins("gold");

            coins2Blue.Content = players[1].CountCoins("blue");
            coins2White.Content = players[1].CountCoins("white");
            coins2Black.Content = players[1].CountCoins("black");
            coins2Green.Content = players[1].CountCoins("green");
            coins2Red.Content = players[1].CountCoins("red");
            coins2Gold.Content = players[1].CountCoins("gold");

            coins3Blue.Content = players[2].CountCoins("blue");
            coins3White.Content = players[2].CountCoins("white");
            coins3Black.Content = players[2].CountCoins("black");
            coins3Green.Content = players[2].CountCoins("green");
            coins3Red.Content = players[2].CountCoins("red");
            coins3Gold.Content = players[2].CountCoins("gold");

            coins4Blue.Content = players[3].CountCoins("blue");
            coins4White.Content = players[3].CountCoins("white");
            coins4Black.Content = players[3].CountCoins("black");
            coins4Green.Content = players[3].CountCoins("green");
            coins4Red.Content = players[3].CountCoins("red");
            coins4Gold.Content = players[3].CountCoins("gold");


        }


        private void LiftCoin(string color) 
        {

                Coin c = coins.Find(i => i.Color == color);
                if (coins.Remove(c))
                    coinsLifted.Add(c);
                CountAllCoins();

            if(coinsLifted.Count() == 3)
            {
                foreach(var item in coinButtons)
                {
                    item.Value.IsEnabled = false;
                }
                TakeCoins.IsEnabled = true;
            }
            else if(coinsLifted.Count() ==2)
            {
                if(coinsLifted[0].Color == coinsLifted[1].Color)
                {
                    foreach (var item in coinButtons)
                    {
                        item.Value.IsEnabled = false;
                    }
                    TakeCoins.IsEnabled = true;
                }
                else
                {
                    string color0 = coinsLifted[0].Color;
                    string color1 = coinsLifted[1].Color;
                    coinButtons[color0].IsEnabled = false;
                    coinButtons[color1].IsEnabled = false;
                }
            }      
        }

        private void TakeLiftedCoins() 
        {
            foreach(Coin item in coinsLifted)
            {
                Coin c = item;
                //coinsLifted.Remove(c);
                players[currentPlayer].AddCoin(c);
            }
            coinsLifted.Clear();

            foreach (var item in coinButtons)
            {
                item.Value.IsEnabled = true;
            }
            TakeCoins.IsEnabled = false;

            CountAllCoins();
            NextTurn();
        }

        private void NextTurn() // switches turn to next player
        {
            if (currentPlayer < playersCount-1)
            {
                borders[currentPlayer].BorderBrush = System.Windows.Media.Brushes.Black;
                currentPlayer++;
                
            }
            else
            {
                borders[currentPlayer].BorderBrush = System.Windows.Media.Brushes.Black;
                currentPlayer = 0;
                turn++;
            }
            labelKolej.Content = players[currentPlayer].name;
            borders[currentPlayer].BorderBrush = System.Windows.Media.Brushes.Red;
        }

        private void coinGold_Click(object sender, RoutedEventArgs e) //does nothing, gold cannot be taken by clicking
        {
            //TakeCoin(currentPlayer, "gold");
        }

        private void coinWhite_Click(object sender, RoutedEventArgs e)
        {
            LiftCoin("white");
        }

        private void coinBlue_Click(object sender, RoutedEventArgs e)
        {
            LiftCoin( "blue");
        }

        private void coinGreen_Click(object sender, RoutedEventArgs e)
        {
            LiftCoin("green");
        }

        private void coinRed_Click(object sender, RoutedEventArgs e)
        {
            LiftCoin("red");
        }

        private void coinBlack_Click(object sender, RoutedEventArgs e)
        {
           LiftCoin("black");
        }

        private void TakeCoins_Click(object sender, RoutedEventArgs e)
        {
            TakeLiftedCoins();
        }
    }
}
