public class CellarMap : Map
{
    public CellarMap() { }
    public CellarMap(char[,] map, char[,] cellarMap, Assassin assassin, List<Enemy> enemies, List<Chest> chests, Enemy boss, Merchant merchant)
    {
        Maplevel = map;
        CellarLevel = cellarMap;
        Enemies = enemies;
        Chests = chests;
        BossEnemy = boss;
        MerchantObject = merchant;
        Assassin = assassin;
    }

    public override void MovePlayer(Player player, Map map, List<Map> maps, int currentLevel, out int level)
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
            else if (gameMap[newX, newY] == Cellar)
            {
                CellarMovement(map, player);
                //GoToCellar(player, map);
                break;
            }
            else if (gameMap[newX, newY] == Door || gameMap[newX, newY] == Door2)
            {
                level++;
                NextLevel();
                break;
            }
            else if (gameMap[newX, newY] == GoBack)
            {
                level--;
                PreviousLevel();
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
    public static void PrintCellar(char[,] gameMap, Player player)
    {
        Console.Clear();
        
        MapInfo(); // INFO OM KARTAN
        Console.CursorVisible = false;

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
    public static void CellarMovement(Map cellarMap, Player player) // Players rörelse på banan, wasd osv.
    {
        int posX = 0;   //posX,posY är positionen som player har för tillfället
        int posY = 0;
        int newX;       //newX,newY är den nya positionen som vi vill förflytta våran player till
        int newY;
        bool inCellar = true;
        Console.CursorVisible = false;
        char[,] gameMap = cellarMap.CellarLevel;
        Merchant merchant = cellarMap.MerchantObject; 
        List<Enemy> enemies = cellarMap.Enemies;
        List<Chest> chests = cellarMap.Chests;

        PrintCellar(gameMap, player);
        while (inCellar && player.CurrentHp > 0)
        {
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
                PrintCellar(gameMap, player);
            }
            else if (gameMap[newX, newY] == Coin)
            {
                UpdatePlayerMovement(posX, posY, newX, newY);
                Console.SetCursorPosition(0, 13);
                HandleGold(player, gameMap, posX, posY, newX, newY);
                Console.SetCursorPosition(0, 9);
                PlayerUI.UI(player);
            }
            else if (gameMap[newX, newY] == Trap)
            {
                UpdatePlayerMovement(posX, posY, newX, newY);
                HandleTrap(player, gameMap, posX, posY, newX, newY);
                Console.SetCursorPosition(0, 9);
                PlayerUI.UI(player);
            }
            else if (gameMap[newX, newY] == Chest)
            {
                Console.SetCursorPosition(newY * 3, newX +2); // Skriv ut den nya positionen
                PrintColor.Gray(" #", "Write");
                Console.SetCursorPosition(0, 13);
                HandleChest(chests, player, gameMap, newX, newY);
            }
            else if (gameMap[newX, newY] == Merchant)
            {
                HandleMerchant(merchant, player);
                PrintCellar(gameMap, player);
            }
            else if (gameMap[newX, newY] == Heart)
            {
                UpdatePlayerMovement(posX, posY, newX, newY);
                HandleHeart(player, gameMap, posX, posY, newX, newY);
                Console.SetCursorPosition(0, 9);
                PlayerUI.UI(player);
            }
            else if (gameMap[newX, newY] == Cellar)
            {
                inCellar = false;
                return;
            }
            else
            {
                
            }
            #endregion

            #region INVENTORY
            if (keyPressed.Key == ConsoleKey.C) //Visa playerStats
            {
                player.OpenInventory(player);
                PrintCellar(gameMap, player);
            }
            #endregion

            #region HEAL
            if (keyPressed.Key == ConsoleKey.Q)
            {
                player.Heal();  //Använder en Health-Potion
                PlayerUI.UI(player);
            }
            #endregion

            
        }
    }
}