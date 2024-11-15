using System.ComponentModel.Design;

public abstract class Map
{
    //EN MAP, innehåller massa olika fiender och items osv specifikt för mappen
    public char[,] Maplevel { get; set; }
    public char[,] CellarLevel { get; set; }
    public Merchant? MerchantObject { get; set; }
    public Assassin? Assassin { get; set; }
    public List<Enemy> Enemies { get; set; }
    public List<Chest> Chests { get; set; }
    public Enemy BossEnemy { get; set; }

    internal bool showHelp = false;
    internal static char Player = '@';
    internal static char Enemy = '£';
    internal static char Boss = 'B';
    internal static char Coin = '$';
    internal static char Wall = '|';
    internal static char Terrain = '_';
    internal static char Chest = '#';
    internal static char Trap = '¤';
    internal static char Empty = ' ';
    internal static char Door = '\\';
    internal static char Door2 = '/';
    internal static char Heart = '\u2665';
    internal static char GoBack = '=';
    internal static char Cellar = ')';
    internal static char Merchant = 'M';
    internal static char OpenChest = '4';
    internal static char invisableAssassin = 'a';

    public char[][] MaplevelJagged
    {
        get => ConvertToJaggedArray(Maplevel);
        set => Maplevel = ConvertToMultidimensionalArray(value);
    }

    private static char[][] ConvertToJaggedArray(char[,] multiArray)
    {
        int rows = multiArray.GetLength(0);
        int cols = multiArray.GetLength(1);
        char[][] jaggedArray = new char[rows][];
        for (int i = 0; i < rows; i++)
        {
            jaggedArray[i] = new char[cols];
            for (int j = 0; j < cols; j++)
            {
                jaggedArray[i][j] = multiArray[i, j];
            }
        }
        return jaggedArray;
    }

    private static char[,] ConvertToMultidimensionalArray(char[][] jaggedArray)
    {
        int rows = jaggedArray.Length;
        int cols = jaggedArray[0].Length;
        char[,] multiArray = new char[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                multiArray[i, j] = jaggedArray[i][j];
            }
        }
        return multiArray;
    }

    #region ENEMY
    internal static void HandleEnemy(Player player, List<Enemy> enemies, char[,] gameMap, int newX, int newY) // När player går på enemy
    {
        Combat.FightMode(player, enemies[0]);
        if (enemies[0].CurrentHp <= 0)
        {
            enemies.RemoveAt(0);
            gameMap[newX, newY] = Empty;
        }
    }
    #endregion
    internal static void HandleInvisibleAssassin(Player player, Assassin assassin, char[,] gameMap, int newX, int newY)
    {
        Combat.FightMode(player, assassin);
        if (assassin.CurrentHp <= 0)
        {
            gameMap[newX, newY] = Empty;
        }
    }
    #region BOSS
    internal static void HandleBoss(Player player, Enemy boss, char[,] gameMap, int newX, int newY) // När player går på boss
    {
        Combat.FightMode(player, boss);
        if (boss.CurrentHp <= 0)
        {
            gameMap[newX, newY] = Empty;
        }
    }
    #endregion
    #region GOLD
    internal static void HandleGold(Player player, char[,] gameMap, int posX, int posY, int newX, int newY) // När player går på guld
    {
        Random random = new Random();
        int goldDrop = random.Next(1, 6);
        player.Gold += goldDrop;
        PrintColor.Yellow($"+{goldDrop} {'\u00A9'}", "WriteLine");

        gameMap[newX, newY] = Player;
        gameMap[posX, posY] = Empty;
    }
    #endregion

    #region CHEST
    internal static void HandleChest(List<Chest> chest, Player player, char[,] gameMap, int newX, int newY) // När player går på chest
    {
        player.Loot(chest[0]);
        chest.RemoveAt(0);
        gameMap[newX, newY] = OpenChest;

        Console.ReadKey(true);
    }
    #endregion

    #region MERCHANT
    internal static void HandleMerchant(Merchant merchant, Player player) // När player går på merchant
    {
        merchant.Interact(player);
    }
    #endregion

    #region HEART
    internal static void HandleHeart(Player player, char[,] gameMap, int posX, int posY, int newX, int newY) // När player går på hjärta
    {
        player.HealingPot.Ammount = 5;
        player.CurrentHp = player.TotalHp;
        gameMap[newX, newY] = Player;
        gameMap[posX, posY] = Empty;
    }
    #endregion

    #region TRAP
    internal static void HandleTrap(Player player, char[,] gameMap, int posX, int posY, int newX, int newY) // När player går på mina
    {
        player.CurrentHp -= 20;
        gameMap[newX, newY] = Player;
        gameMap[posX, posY] = Empty;
    }
    #endregion

    #region NEXTLEVEL
    internal static void NextLevel() // Går till nästa map i listan av maps
    {
        Console.Clear();
        // Loada nästa level
        Console.WriteLine("Du klarade nivån");
        Textures.PrintLoading();
    }
    #endregion

    #region GOBACK
    internal static void PreviousLevel() // Går till förgående map i listan av maps
    {
        Console.Clear();
        // Loada nästa level
        Console.WriteLine("Du gick tillbaka en nivå");
        Textures.PrintLoading();
    }
    #endregion

    #region EMPTY
    internal static void HandleEmpty(char[,] gameMap, int posX, int posY, int newX, int newY)
    {
        gameMap[newX, newY] = Player; // Byter plats
        gameMap[posX, posY] = Empty; // Där vi stod blir tom
    }
    #endregion

    #region MOVE PLAYER
    internal static void UpdatePlayerMovement(int posX, int posY, int newX, int newY)
    {
        Console.SetCursorPosition(posY * 3, posX + 2); // Töm den gamla positionen
        Console.Write("  "); // Antag att symbolerna är enkla, annars justera bredden

        Console.SetCursorPosition(newY * 3, newX + 2); // Skriv ut den nya positionen
        PrintColor.Green(" @", "Write");
    }

    public static void Help()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.SetCursorPosition(71, 2);
        Console.WriteLine("H - BACK  ");
        Console.SetCursorPosition(71, 4);
        Console.WriteLine("W - UP");
        Console.SetCursorPosition(71, 5);
        Console.WriteLine("A - LEFT");
        Console.SetCursorPosition(71, 6);
        Console.WriteLine("S - DOWN");
        Console.SetCursorPosition(71, 7);
        Console.WriteLine("D - RIGHT");
        Console.SetCursorPosition(71, 8);
        Console.WriteLine("");
        Console.SetCursorPosition(71, 9);
        Console.WriteLine("C - INVENTORY");
        Console.SetCursorPosition(71, 10);
        Console.WriteLine("Q - HEAL");

        Console.SetCursorPosition(71, 12);
        Console.WriteLine("ESC - MENY");
        Console.ResetColor();


    }
    #endregion

    public static void PauseMenu(Player player, List<Map> maps)
    {
        Console.Clear();
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
            Console.WriteLine("Continue");
            Console.SetCursorPosition(40, 7);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("        2. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Save Game");
            Console.SetCursorPosition(40, 8);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("        3. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Load Game");
            Console.SetCursorPosition(40, 9);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("        4. ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Quit Game");
            Console.SetCursorPosition(40, 10);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("******************************");
            Console.ResetColor();
            //Player player = new Player("Player");
            var choice = Console.ReadKey(true);
            //if (choice.Key == ConsoleKey.Escape)
            //string choice = Console.ReadLine();

            switch (choice.Key)     //Lägga in pil med piltangent upp/ner?
            {
                case ConsoleKey.Escape:
                    return;

                case ConsoleKey.D1:
                    return;

                case ConsoleKey.D2: // GREJER FÖR ATT LADDA, JSON
                    Json.Save(player, maps);
                    break;

                case ConsoleKey.D3:
                    //Player player = new Player("Player");
                    // Json.Load(player.Name, out Player loadedplayer, out List<Map> loadedmaps);
                    // player = loadedplayer;
                    // maps = loadedmaps;
                    break;

                case ConsoleKey.D4:
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

    internal static void MapInfo() //Skriver ut info ovanför mappen
    {
        Console.WriteLine();
        PrintColor.Green($" Player: {Player}  ", "Write");
        PrintColor.Red($"Enemy: {Enemy}  ", "Write");
        PrintColor.DarkYellow($"Chest: {Chest}  ", "Write");
        PrintColor.Yellow($"Coin: {'\u00A9'}  ", "Write");
        PrintColor.Gray($"Trap: {Trap}  ", "Write");
        PrintColor.Red($"Boss: {Boss}  ", "Write");
        PrintColor.Green($"Door: {Door}  ", "Write");
        Console.ResetColor();
        Console.WriteLine();
    }

    public virtual void MovePlayer(Player player, Map map, List<Map> maps, int currentLevel, out int level)
    {
        level = currentLevel;
        
        int posX = 0;   //posX,posY är positionen som player har för tillfället
        int posY = 0;
        int newX;       //newX,newY är den nya positionen som vi vill förflytta våran player till
        int newY;

        Console.CursorVisible = false;

        Merchant? merchant = MerchantObject;    //Hämtar merchant, enemylista, chestlista, gameMap och boss via Map-objektet
        char[,] gameMap = Maplevel;
        List<Enemy> enemies = Enemies;
        Enemy boss = BossEnemy;
        List<Chest> chests = Chests;
        Assassin assassin = Assassin;

        while (player.CurrentHp > 0)
        {
            //Clearar raderna för utskrift om loot
            Console.SetCursorPosition(0, 28);
            Console.WriteLine("                                                                                 ");
            Console.SetCursorPosition(0, 29);
            Console.WriteLine("                                                                                 ");
            Console.SetCursorPosition(0, 30);
            Console.WriteLine("                                                                                 ");
            Console.SetCursorPosition(0, 31);
            Console.WriteLine("                                                                                 ");
            Console.SetCursorPosition(0, 32);
            Console.WriteLine("                                                                                 ");
            Console.SetCursorPosition(0, 33);
            Console.WriteLine("                                                                                 ");

            Console.CursorVisible = false;
            var keyPressed = Console.ReadKey(true);

            for (int i = 0; i < gameMap.GetLength(0); i++)      //hitta positionen för player och ge dessa värden till posX och posY
            {
                for (int j = 0; j < gameMap.GetLength(1); j++)
                {
                    if (gameMap[i, j] == Player)
                    {
                        posX = i;
                        posY = j;
                    }
                }
            }
            newX = posX;
            newY = posY;

            //Ger värde till newX och newY baserat på åt vilket håll vi väljer att gå, via WASD
            #region UP
            if (keyPressed.Key == ConsoleKey.W)
            {
                newX = posX - 1;
                newY = posY;
            }
            #endregion

            #region LEFT
            if (keyPressed.Key == ConsoleKey.A)
            {
                newX = posX;
                newY = posY - 1;
            }
            #endregion

            #region Down
            if (keyPressed.Key == ConsoleKey.S)
            {
                newX = posX + 1;
                newY = posY;
            }
            #endregion

            #region Right
            if (keyPressed.Key == ConsoleKey.D)
            {
                newX = posX;
                newY = posY + 1;
            }
            #endregion


            //Anropar metoder baserat på newX och newY positionerna
            #region MOVEMENTACTIONS
            if (gameMap[newX, newY] == Empty)
            {
                UpdatePlayerMovement(posX, posY, newX, newY);
                HandleEmpty(gameMap, posX, posY, newX, newY);
            }
            else if (gameMap[newX, newY] == Enemy)
            {
                HandleEnemy(player, enemies, gameMap, newX, newY);
                return;
            }
            else if (gameMap[newX, newY] == Coin)
            {
                UpdatePlayerMovement(posX, posY, newX, newY);
                Console.SetCursorPosition(0, 29);
                HandleGold(player, gameMap, posX, posY, newX, newY);
                Console.SetCursorPosition(0, 25);
                PlayerUI.UI(player);
            }
            else if (gameMap[newX, newY] == Trap)
            {
                UpdatePlayerMovement(posX, posY, newX, newY);
                HandleTrap(player, gameMap, posX, posY, newX, newY);
                Console.SetCursorPosition(0, 25);
                PlayerUI.UI(player);
            }
            else if (gameMap[newX, newY] == Chest)
            {
                Console.SetCursorPosition(newY * 3, newX + 2); // Skriv ut den nya positionen
                PrintColor.Gray(" #", "Write");
                Console.SetCursorPosition(0, 29);
                HandleChest(chests, player, gameMap, newX, newY);
            }
            else if (gameMap[newX, newY] == Heart)
            {
                UpdatePlayerMovement(posX, posY, newX, newY);
                HandleHeart(player, gameMap, posX, posY, newX, newY);
                Console.SetCursorPosition(0, 25);
                PlayerUI.UI(player);
            }
            else if (gameMap[newX, newY] == Boss)
            {
                HandleBoss(player, boss, gameMap, newX, newY);
                return;
            }
            else if (gameMap[newX, newY] == Merchant)
            {
                HandleMerchant(merchant, player);
                return;
            }
            else if (gameMap[newX, newY] == Door || gameMap[newX, newY] == Door2)
            {
                NextLevel();
                level++;
                break;
            }
            else if (gameMap[newX, newY] == GoBack)
            {
                PreviousLevel();
                level--;
                break;
            }
            else if (gameMap[newX, newY] == invisableAssassin)
            {
                HandleInvisibleAssassin(player, assassin, gameMap, newX, newY);
                return;
            }

            else // Väggar och terräng
            {
                //Gör ingenting
            }
            #endregion

            #region INVENTORY
            if (keyPressed.Key == ConsoleKey.C) //Visa playerStats
            {
                player.OpenInventory(player);
                return;
                //PrintGameBoard(map, player);
            }
            #endregion

            #region HEAL
            if (keyPressed.Key == ConsoleKey.Q) //Använder Health-Potions
            {
                player.Heal();
                Console.SetCursorPosition(0, 25);
                PlayerUI.UI(player);
            }
            #endregion

            if (keyPressed.Key == ConsoleKey.H)
            {
                
                if(showHelp == false)
                {
                    Help();
                    showHelp = true;
                }
                else
                {
                    PlayerUI.HelpText();
                    showHelp = false;
                }
            }

            if (keyPressed.Key == ConsoleKey.Escape)
            {
                PauseMenu(player, maps);
                return;
            }
            Console.SetCursorPosition(0, 27);
        }
    }

    #region PRINT MAP
    public virtual void PrintMap(Player player, Map map)
    {
        Console.Clear();

        MapInfo(); // INFO OM KARTAN
        Console.CursorVisible = false;
        char[,] gameMap = Maplevel;

        // SKRIVER UT MAP, med olika textfärger baserat på char
        for (int i = 0; i < gameMap.GetLength(0); i++)
        {
            for (int j = 0; j < gameMap.GetLength(1); j++)
            {
                if (gameMap[i, j] == Player)
                    PrintColor.Green($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Enemy)
                    PrintColor.Red($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == invisableAssassin)
                    PrintColor.Red($"   ", "Write");

                else if (gameMap[i, j] == Chest)
                    PrintColor.Yellow($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == OpenChest)
                    PrintColor.Gray(" # ", "Write");

                else if (gameMap[i, j] == Merchant)
                    PrintColor.Yellow(" M ", "Write");

                else if (gameMap[i, j] == Trap)
                    PrintColor.Gray($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Boss)
                    PrintColor.Red($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == Coin)
                    PrintColor.DarkYellow($" {'\u00A9'} ", "Write");

                else if (gameMap[i, j] == Wall || gameMap[i, j] == Terrain)
                    PrintColor.BackgroundDarkCyan("   ", "Write");

                else if (gameMap[i, j] == Door || gameMap[i, j] == Door2)
                    PrintColor.DarkGreen($" {gameMap[i, j]} ", "Write");

                else if (gameMap[i, j] == GoBack)
                    PrintColor.BackgroundGreen($" {gameMap[i, j]} ", "Write");
                else if (gameMap[i, j] == Cellar)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write($" {gameMap[i, j]} ");
                    Console.ResetColor();
                }
                else
                    Console.Write($" {gameMap[i, j]} ");
            }
            Console.WriteLine();
        }
        PlayerUI.UI(player);    //visa UI under mappen
    }
    #endregion
}

public class RegularMap : Map
{
    public RegularMap() { }
    public RegularMap(char[,] map, List<Enemy> enemies, Assassin assassin, List<Chest> chests, Enemy boss, Merchant merchant)
    {
        Maplevel = map;
        Enemies = enemies;
        Chests = chests;
        BossEnemy = boss;
        MerchantObject = merchant;
        Assassin = assassin;
    }
}

