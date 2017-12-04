using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine;

namespace Aplauz.Launcher
{
    internal class UserInterface
    {
        private static void Main(string[] args)
        {
            Console.BackgroundColor=ConsoleColor.Gray;
            Console.Clear();
            Console.WriteLine("Witaj w Aplauzie");
            SuggestHelp();

            UseBasicUserInterface();
        }

        private static void UseBasicUserInterface()
        {
            while (true)
            {
                string input;
                input = Console.ReadLine();
                if (input == "h")
                    ShowHelp();
                else if (input == "1")
                {
                    var board = new Board(new[] { "adam", "basia", "cyprian", "danuta" }, new[] { "Human", "Human", "Human", "Human" });
                }
                else if (input == "2")
                {
                    var board = new Board(new[] { "adam", "basia", "cyprian", "danuta" }, new[] { "Random", "Random", "Random", "Random" });
                }
                else if (input == "3")
                {
                    var board = new Board(new[] { "adam", "basia", "cyprian", "danuta" }, new[] { "Human", "Random", "Random", "Random" });
                }
                else if (input == "4")
                {
                    var board = new Board(new[] { "adam", "basia", "cyprian", "danuta" }, new[] { "Human", "Upgrade", "Upgrade", "Upgrade" });
                }
                else if (input == "5")
                {
                    var board = new Board(new[] { "adam", "basia", "cyprian", "danuta" }, new[] { "Human", "Neural", "Neural", "Neural" });
                }
                else if (input == "6")
                {
                    var board = new Board(new[] { "adam", "basia", "cyprian", "danuta" }, new[] { "Human", "Neural", "Upgrade", "Greedy" });
                }
                else if (input == "7")
                {
                    var board = new Board(new[] { "adam", "basia", "cyprian", "danuta" }, new[] { "Human", "Neural", "Upgrade", "Monte" });
                }
                else if (input == "my")
                {
                    string[] names = new string[4];
                    string[] types = new string[4];
                    for (int i = 0; i < 4; i++)
                    {
                        Console.WriteLine("imię" + i + ": ");
                        input = Console.ReadLine();
                        names[i] = input;
                        bool isCorrect = false;
                        Console.WriteLine("typ" + i + ": ");
                        while (!isCorrect)
                        {

                            input = Console.ReadLine();
                            isCorrect = IsTypeCorrect(input);
                            if (isCorrect == false)
                            {
                                Console.WriteLine("błędny typ, spróbuj jeszcze raz: ");
                            }

                        }
                        types[i] = input;
                    }
                    var board = new Board(names, types);

                }
                else
                {
                    Console.WriteLine("niepoprawne polecenie");
                }


            }
        }

        private static void SuggestHelp()
        {
            Console.WriteLine("Wpisz h aby uzyskać pomoc");
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Aby rozpocząć gre użyj cyfry dla jednego z gotowych szablonów konfiguracji z domyślnymi imionami:");
            Console.WriteLine("1 - 4Humans, 2 - 4Randoms, 3 - 1Human3Randoms, 4 - 1Human3Upgrade, 5 - 1Human3Neural, 6 -  1Human1Neural1Upgrade1Greedy, 7 - 1Human1Neural1Upgrade1Monte");
            Console.WriteLine("lub wpisz polecenie 'my' w celu wykonania własnej konfiguracji");
            Console.WriteLine("Dostępne typy graczy: Human, Random, Neural, Monte, Greedy, Upgrade");
            Console.WriteLine("");
            Console.WriteLine("Wytlumaczenie stanu gry wyświetlanego graczowi typu Human:");
            Console.WriteLine("Kolorowe liczby na szarym tle oznaczają liczbę żetonów");
            Console.WriteLine("Szare liczby na kolorowym tle oznaczają zasoby wynikające z posiadanych kart");
            Console.WriteLine("Karty na stole są reprezentowane w następującym formacie: ");
            Console.WriteLine("Poziom|NumerKartyOdLewejNa Stole|: |Poziom|Kolor|Prestiż|CenaBiała|Niebieska|Zielona|Czerwona|Czarna");
        }

        private static bool IsTypeCorrect(string type)
        {
            if (type == "Human" || type == "Random" || type == "Neural" || type == "Monte" || type == "Greedy" ||
                type == "Upgrade")
            {
                return true;
            }
            return false;
        }
    }
}
