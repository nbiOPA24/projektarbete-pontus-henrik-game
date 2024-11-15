using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

public static class Combat
{
    private static string attackMessagePlayer;
    private static string attackMessageEnemy;
    private static string enemyDamage;
    private static string playerDamage;
    private static string playerHealing;

#region COMBATMENU
    private static void CombatMenu(Player player, Enemy enemy)
    {
        //Menyn
        Console.SetCursorPosition(0, 11);
        Console.WriteLine($"E. Attack");
        if (player.HealingPot.Ammount == 0)
        {
            Console.SetCursorPosition(0, 12);
            Console.Write($"Q. Heal - ");
            Console.ForegroundColor = ConsoleColor.Red;
            player.HealingPot.ShowItem();
            Console.ResetColor();
        }
        else
        {
            Console.SetCursorPosition(0, 12);
            Console.Write($"Q. Heal - ");
            Console.ForegroundColor = ConsoleColor.Green;
            player.HealingPot.ShowItem();
            Console.ResetColor();

        }
        Console.SetCursorPosition(0, 13);
        Console.WriteLine($"D. Defend");

        Console.SetCursorPosition(0, 14);

        Console.WriteLine($"C. Flee");
        // Console.SetCursorPosition(0, 16);
    }
    #endregion
    #region ENEMYKILLED

    private static void EnemyKilled(Player player, Enemy enemy)
    {
        Textures.AttackPlayerAnimation();
        Clear.Damage();
        Clear.EnemyHp();
        enemy.ShowHp();
        Console.SetCursorPosition(20, 6);
        PrintColor.Green($"{attackMessagePlayer}", "WriteLine");
        Console.SetCursorPosition(20, 7);
        PrintColor.Green($"DMG {playerDamage} -->", "WriteLine");
        Console.SetCursorPosition(0, 11);
        player.EnemyKilled(enemy);  //Skriver ut skadan
        Console.WriteLine("            ");
        Console.WriteLine("            "); // FÖR ATT DÖLJA TIDIGARE TEXT
        Console.WriteLine("            ");
        Console.ReadKey(true);
    }
#endregion
#region PLAYERATTACK
    private static void PlayerAttack(Enemy enemy)
    {
        Textures.AttackPlayerAnimation();
        Clear.Damage();   //Rensa damagetext, och enemys hp
        Clear.EnemyHp();
        enemy.ShowHp();

        Console.SetCursorPosition(20, 6);
        PrintColor.Green($"{attackMessagePlayer}", "WriteLine");
        Console.SetCursorPosition(20, 7);
        PrintColor.Green($"DMG {playerDamage} -->", "WriteLine");
    }
#endregion
#region ENEMYATTACK
    private static void EnemyAttack(Player player, Enemy enemy)
    {
        enemy.CharacterAttackAnimation(enemy);

        Clear.Damage();    //Rensa sedan damagetext och players hp
        Clear.PlayerHp();

        Console.SetCursorPosition(0, 2);       //Och uppdatera detta
        player.ShowHp();

        Clear.Damage();
        
        Console.SetCursorPosition(20, 6);
        PrintColor.Red($"{attackMessageEnemy}", "WriteLine");
        Console.SetCursorPosition(20, 7);
        PrintColor.Red($"<-- {enemyDamage} DMG", "WriteLine");
    }
#endregion
#region PLAYERHEAL
    private static void PlayerHeal(Player player)
    {
        Clear.PlayerHp();
        Console.SetCursorPosition(0, 2);  //Rad, Kolumn
        player.ShowHp();

        Console.SetCursorPosition(20, 7);

        PrintColor.Green($"{playerHealing}", "WriteLine");
    }
#endregion
#region STARTPOSITION
    private static void StartPosition(Player player, Enemy enemy)
    {
        Console.SetCursorPosition(0, 0);
        PrintColor.Green($"{player.Name}                   ", "Write");
        Console.SetCursorPosition(25, 0);
        Console.Write("VS");
        Console.SetCursorPosition(40, 0);
        PrintColor.Red($"{enemy.Name}({enemy.Description})", "Write");

        //HP        Skriv ut Hp och "gubbar" i början av loopen
        Console.SetCursorPosition(0, 2);
        player.ShowHp();    //Ska uppdateras
        enemy.ShowHp();     //Ska uppdateras
        Console.WriteLine();

        // Gubben
        Textures.PrintPlayerCharacter(5, 0);

        // Fienden
        Console.SetCursorPosition(25, 5);

        enemy.PrintCharacter(enemy);

    }
#endregion
    public static void FightMode(Player player, Enemy enemy)
    {
        attackMessagePlayer = "";
        attackMessageEnemy = "";
        enemyDamage = "";
        playerDamage = "";
        playerHealing = "";

        while (true)
        {
            Console.CursorVisible = false;
            Console.Clear();
            StartPosition(player, enemy);

            if (attackMessageEnemy != "STUNNED")
            {
                CombatMenu(player, enemy);

                var input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.E:
                        playerDamage = player.Attack(enemy, out attackMessagePlayer);   //Skriver ut skadan // SPARAR den returnerade stringen i playerDamage
                        break;
                    case ConsoleKey.Q:
                        playerHealing = player.Heal();
                        break;
                    case ConsoleKey.D: // Denna tom för att inte komma till default case utan för att faktiskt defenda. Vi vill inte uppdatera förrän längre ner.
                        Console.SetCursorPosition(18, 6);
                        Console.WriteLine($"{player.Name}");
                        Console.SetCursorPosition(18, 7);
                        Console.WriteLine("is ready to defend");
                        //Textures för defendposition för player
                        break;
                    case ConsoleKey.C:
                        return;
                    default:
                        Console.SetCursorPosition(18, 6);
                        Console.WriteLine("You MISSED your turn");
                        Console.SetCursorPosition(18, 7);
                        Console.WriteLine("..unlucky!");
                        break;
                }

                if (enemy.CurrentHp < 1)   //Fixa så att enemy.CurrentHp inte kan bli lägre än 0
                {
                    EnemyKilled(player, enemy);
                    return;
                }

                if (input.Key == ConsoleKey.Q)   //Om vi healar, uppdatera player.CurrentHp innan enemy attackerar
                {
                    PlayerHeal(player);
                }

                if (input.Key == ConsoleKey.E)    //Vid attack
                {
                    PlayerAttack(enemy);
                }
                
                if (input.Key == ConsoleKey.D)
                {
                    enemyDamage = player.Defend(player, enemy, out attackMessageEnemy);
                }
                else
                {
                    enemyDamage = enemy.Attack(player, out attackMessageEnemy);
                }
            }
            else
            {
                Console.WriteLine("You are stunned and cant attack this round");
                enemyDamage = enemy.Attack(player, out attackMessageEnemy);
            }
            // if (input.Key == ConsoleKey.D)
            // {
            //     enemyDamage = player.Defend(enemy, out attackMessageEnemy);
            // }
            // else
            // {
            //     enemyDamage = enemy.Attack(player, out attackMessageEnemy);
            // }
            // enemyDamage = enemy.Attack(player, out attackMessageEnemy);

            Thread.Sleep(700);

            EnemyAttack(player, enemy);

            Thread.Sleep(700);

            if (player.CurrentHp < 1)
            {
                return;
            }
        }
    }
}