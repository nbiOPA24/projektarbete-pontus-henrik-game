public class DarkMap : Map
{
    public DarkMap() { }
    public DarkMap(char[,] map, List<Enemy> enemies, Assassin assassin, List<Chest> chests, Enemy boss, Merchant merchant)
    {
        Maplevel = map;
        Enemies = enemies;
        Chests = chests;
        BossEnemy = boss;
        MerchantObject = merchant;
        Assassin = assassin;
    }
    #region PRINTMAP
    public override void PrintMap(Player player, Map map)
    {
        Console.Clear();
        char[,] gameMap = map.Maplevel;
        // INFO OM KARTAN
        MapInfo();
        Console.CursorVisible = false;
        // SKRIVER UT MAP, med olika textfärger baserat på char

        int posX = 0;   //posX,posY är positionen som player har för tillfället
        int posY = 0;

        Console.WriteLine();
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

        for (int i = posX - 4; i < posX + 4; i++)
        {
            if (i >= 0 && i < gameMap.GetLength(0))
            {
                for (int j = posY - 4; j < posY + 4; j++)
                {
                    if (j >= 0 && j < gameMap.GetLength(1))
                    {
                        if (gameMap[i, j] == Player)
                            PrintColor.Green($" {gameMap[i, j]} ", "Write");

                        else if (gameMap[i, j] == Enemy)
                            PrintColor.Red($" {gameMap[i, j]} ", "Write");

                        else if (gameMap[i, j] == Chest)
                            PrintColor.Yellow($" {gameMap[i, j]} ", "Write");

                        else if (gameMap[i, j] == OpenChest) // ANVÄNDS INTE ÄN
                            PrintColor.Gray($" # ", "Write");

                        else if (gameMap[i, j] == Trap)
                            PrintColor.Gray($" {gameMap[i, j]} ", "Write");

                        else if (gameMap[i, j] == Boss)
                            PrintColor.Red($" {gameMap[i, j]} ", "Write");

                        else if (gameMap[i, j] == Merchant)
                            PrintColor.Yellow(" M ", "Write");

                        else if (gameMap[i, j] == Coin)
                            PrintColor.DarkYellow($" {gameMap[i, j]} ", "Write");

                        else if (gameMap[i, j] == Wall || gameMap[i, j] == Terrain)
                            PrintColor.BackgroundDarkGray("   ", "Write");

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
                            Console.Write(gameMap[i, j] + "  ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
    #endregion
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
            HandleEmpty(gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Enemy)
        {
            HandleEnemy(player, enemies, gameMap, newX, newY);
        }
        else if (gameMap[newX, newY] == Coin)
        {
            HandleGold(player, gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Trap)
        {
            HandleTrap(player, gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Chest)
        {
            HandleChest(chests, player, gameMap, newX, newY);
        }
        else if (gameMap[newX, newY] == Heart)
        {
            HandleHeart(player, gameMap, posX, posY, newX, newY);
        }
        else if (gameMap[newX, newY] == Boss)
        {
            HandleBoss(player, boss, gameMap, newX, newY);
        }
        else if (gameMap[newX, newY] == Merchant)
        {
            HandleMerchant(merchant, player);
        }
        else if (gameMap[newX, newY] == Door || gameMap[newX, newY] == Door2)
        {
            level++;
            NextLevel();
        }
        else if (gameMap[newX, newY] == GoBack)
        {
            level--;
            PreviousLevel();
        }
        else if (gameMap[newX, newY] == invisableAssassin)
        {
            HandleInvisibleAssassin(player, assassin, gameMap, newX, newY);
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
        }
        #endregion

        #region HEAL
        if (keyPressed.Key == ConsoleKey.Q) //Använder Health-Potions
        {
            player.Heal();
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
}
}