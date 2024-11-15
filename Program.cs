using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.CursorVisible = false;
        Textures.PrintFirstScreen();

        bool inMenu = true;
        //Lägga till en likadan meny fast som även innehåller Continue och Save Game alternativ? och kanske Highscore?
        while (inMenu)
        {
            // Lägga till funktion att välja med wasd eller upp och ner tangenterna?
            Console.Clear();
            Console.SetCursorPosition(40, 5);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("********** MAINMENU **********");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(40, 6);
            Console.Write("        1. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("New Game");
            Console.SetCursorPosition(40, 7);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("        2. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Load Game");     //Lägga till json 
            Console.SetCursorPosition(40, 8);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("        3. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Quit Game");
            Console.SetCursorPosition(40, 9);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("******************************");
            Console.ResetColor();
            Player player = new Player("Player");
            List<Map> maps = new List<Map>();
            var choice = Console.ReadKey(true);
            //string choice = Console.ReadLine();

            switch (choice.Key)     //Lägga in pil med piltangent upp/ner?
            {
                case ConsoleKey.D1:
                    player = new Player("Player");
                    maps = [AddMaps.Level1(player), AddMaps.Level2(player), AddMaps.Level3(player)];
                    Console.SetCursorPosition(40, 11);
                    Console.WriteLine("Whats your name?");
                    Console.SetCursorPosition(60, 11);
                    player.Name = Console.ReadLine();
                    PlayGame(player, maps);
                    break;

                case ConsoleKey.D2: // GREJER FÖR ATT LADDA, JSON
                    // Json.Load(player.Name, out Player loadedplayer, out List<Map> loadedmaps);
                    // player = loadedplayer;
                    // maps = loadedmaps;
                    // PlayGame(player, maps);
                    break;

                case ConsoleKey.D3:
                    Console.WriteLine("quitting...");
                    inMenu = false;
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Thats not a choice!");
                    break;
            }
        }
    }

    static void PlayGame(Player player, List<Map> maps)
    {
        bool gameOver = false;
        int level = 0;

        while (!gameOver)
        {
            
            Console.CursorVisible = false;
            maps[level].PrintMap(player, maps[level]);
            maps[level].MovePlayer(player, maps[level], maps, level, out level);

            if (player.CurrentHp < 1)    //börjar sedan om loop och skriver ut mapp igen, om inte player.CurrentHp är 0, isåfall avslutas loop(GameOver)
            {
                Console.Clear();
                Console.WriteLine("Du dog"); // LÄGG IN EN ANIMATION
                Textures.PrintDeadText();
                Console.ReadKey(true);
                gameOver = true;
            }
        }
    }

    // public static void PauseMenu(Player player, List<Map> maps)
    // {
    //     bool inMenu = true;
    //     //Lägga till en likadan meny fast som även innehåller Continue och Save Game alternativ? och kanske Highscore?
    //     while (inMenu)
    //     {
    //         // Lägga till funktion att välja med wasd eller upp och ner tangenterna?
    //         Console.Clear();
    //         Console.SetCursorPosition(40, 5);
    //         Console.ForegroundColor = ConsoleColor.Cyan;
    //         Console.WriteLine("********** MAINMENU **********");
    //         Console.ForegroundColor = ConsoleColor.Yellow;
    //         Console.SetCursorPosition(40, 6);
    //         Console.Write("        1. ");
    //         Console.ForegroundColor = ConsoleColor.White;
    //         Console.WriteLine("Continue");
    //         Console.SetCursorPosition(40, 7);
    //         Console.Write("        2. ");
    //         Console.ForegroundColor = ConsoleColor.White;
    //         Console.WriteLine("Save Game");
    //         Console.SetCursorPosition(40, 8);
    //         Console.Write("        3. ");
    //         Console.ForegroundColor = ConsoleColor.White;
    //         Console.WriteLine("Load Game");
    //         Console.SetCursorPosition(40, 9);
    //         Console.ForegroundColor = ConsoleColor.Yellow;
    //         Console.Write("        4. ");
    //         Console.ForegroundColor = ConsoleColor.White;
    //         Console.WriteLine("Quit Game");
    //         Console.SetCursorPosition(40, 10);
    //         Console.ForegroundColor = ConsoleColor.Cyan;
    //         Console.WriteLine("******************************");
    //         Console.ResetColor();
    //         //Player player = new Player("Player");
    //         string choice = Console.ReadLine();

    //         switch (choice)     //Lägga in pil med piltangent upp/ner?
    //         {
    //             case "1":
    //                 return;

    //             case "2": // GREJER FÖR ATT LADDA, JSON
    //                 Json.Save(player, maps);
    //                 break;

    //             case "3":
    //                 //Player player = new Player("Player");
    //                 Json.Load(player, maps);
    //                 break;

    //             case "4":
    //                 Console.WriteLine("quitting...");
    //                 inMenu = false;
    //                 Environment.Exit(0);
    //                 break;

    //             default:
    //                 Console.WriteLine("Thats not a choice!");
    //                 break;
    //         }
    //     }
    // }

}