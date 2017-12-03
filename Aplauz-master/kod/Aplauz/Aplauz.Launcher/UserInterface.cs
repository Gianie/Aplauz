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
            Console.WriteLine("1 - 4humans, 2 - 4randoms, 3 - 1human3randoms, 4 - 1human3upgrade, 5 - 1human3neural, 6 -  1human1neural1upgrade1greedy, 7 - 1human1neural1upgrade1monte");
            Console.WriteLine("lub wpisz polecenie 'my' w celu wykonania własnej konfiguracji:");
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
