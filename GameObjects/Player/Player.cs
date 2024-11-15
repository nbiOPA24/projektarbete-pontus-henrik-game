using System.ComponentModel;
using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Security;
// public class EquippedItems
// {
//     public Item[] Items { get; set; }
//     public EquippedItems()
//     {
//         Items = new Item[6];
//     }
// }
public class Player : GameObject
{
    public bool isPlayer = true;
    public int CurrentXp { get; set; }
    public int MaxXp { get; set; }
    public int Level { get; set; }
    public int Gold { get; set; }
    public Consumable HealingPot { get; set; } // Vårat objekt för healingspotions
    private HealthBar healthBar;

    public double BonusAgility { get; set; }
    public double BonusHp { get; set; }
    public double BonusDamage { get; set; }
    public double BonusResistance { get; set; }
    public override double TotalHp
    {
        get { return BaseHp + BonusHp; }        //Något fel med CurrentHP efter vi har satt på oss gear, den visar rätt, men på nått sätt innehåller den mer hp än vad som skrivs
    }
    public override double TotalDamage
    {
        get { return BaseDamage + BonusDamage; }
    }
    public override double TotalAgility
    {
        get { return BaseAgility + BonusAgility; }
    }
    public override double TotalResistance
    {
        get { return BaseResistance + BonusResistance; }
    }

    public Inventory Inventory { get; set; }

    public Item[] EquippedGear { get; set; }// = new Item[6];

    public Player(string name)
    {
        Name = name;
        Level = 1;
        Gold = 0;
        CurrentXp = 0;
        MaxXp = 100;
        BaseHp = 100;
        CurrentHp = BaseHp;
        BaseDamage = 15;
        BaseResistance = 5;
        BaseAgility = 10;

        HealingPot = new Consumable();
        Inventory = new Inventory();
        healthBar = new HealthBar();
        EquippedGear = new Item[6];
    }
    public Player() { }

    #region HP
    public void ShowHp()    //Skriver ut en HealthBar och hp i text under
    {
        healthBar.PrintHealthBar(PercentHp, isPlayer);
        PrintColor.Green($"{CurrentHp:F0}/{TotalHp:F0}({PercentHp:F0}%)", "WriteLine");
    }
    #endregion

    #region INVENTORY
    public void OpenInventory(Player player)    //Anropar Inventory-klassens metoder
    {
        Inventory.InventoryUI(player);
    }
    #endregion

    #region LOOT
    public void Loot(Chest chest)     //Lägg till Item till inventory
    {
        Item item;
        if (Inventory.inventory.Count < 15)
        {
            Console.WriteLine($"{Name} har lootat:");
            chest.PrintChest();
            for (int i = 0; i < chest.ChestLoot.Count; i++)
            {
                item = chest.ChestLoot[i];
                if (item is Consumable)
                {
                    if (HealingPot.Ammount < HealingPot.MaxAmmount)
                    {
                        HealingPot.Ammount += 1;
                    }
                    else
                    {
                        Console.WriteLine("Du har det maximala antalet Healing Potions redan");
                    }
                }
                else
                {
                    Inventory.inventory.Add(item);
                }
            }
            Gold += chest.Gold;
        }
        else
        {
            Console.WriteLine("Inventory är full");
        }
    }
    #endregion

    #region HEAL
    public string Heal() // Ökar player hp
    {
        double healAmmount;
        if (HealingPot.Ammount > 0)
        {
            double missingHealth = TotalHp - CurrentHp;     //Räkna ut hur mycket hp spelaren saknar
            healAmmount = HealingPot.Healing; //50 + random.Next(0, 20);
            if (healAmmount > missingHealth)    //Om Heal är mer än spelarens saknade hp, heala till fullt, så att currentHealth inte kan bli mer än TotalHp
            {
                healAmmount = missingHealth;
            }
            CurrentHp += healAmmount;
            HealingPot.Ammount--;
            return $"+{healAmmount:F0}HP";
        }
        else
        {
            return " ";
        }
    }
    #endregion
    #region ATTACK
    public string Attack(Enemy enemy, out string attackMessage) //Avgör critical hit, ger skadan beroende på 
    {
        Random rndCrit = new Random();
        double totalDamageDone;
        int critChance = Convert.ToInt32(BaseAgility); // Om Agility är 10 // CRITICAL HITS
        int crit = rndCrit.Next(0, 101); // 0 - 10
        double damage;
        bool attackCrit = false;
        string printDamage;

        if (crit <= critChance)
        {
            damage = TotalDamage * 1.8;
            attackCrit = true;
        }
        else
        {
            damage = TotalDamage;
        }

        Random rndDamage = new Random();
        double damageNegation = enemy.TotalResistance * 0.2;
        totalDamageDone = damage + rndDamage.Next(0, 10) - damageNegation;

        printDamage = enemy.TakeDamage(totalDamageDone, attackCrit, out attackMessage);

        return printDamage;
    }
    #endregion
    #region XP OCH LEVELUP
    public void EnemyKilled(Enemy enemy) // Ger xp-drop, anropar textures, kontrollerar om player levlar upp
    {
        CurrentXp += enemy.XpDrop;
        Console.SetCursorPosition(0, 11);
        PrintColor.DarkYellow($"+{enemy.XpDrop} XP        ", "WriteLine");

        Console.SetCursorPosition(0, 12);
        Clear.Row(12, 0, 40);
        Console.WriteLine("             \n            \n              \n          "); // För att input-text ska försvinna
        Console.ResetColor();

        Textures.PrintGrave();

        if (CurrentXp >= MaxXp)
        {
            int transferXpToNextLevel = CurrentXp - MaxXp;  //Räkna ut hur mycket xp som ska överföras till nästa level
            CurrentXp = transferXpToNextLevel;
            MaxXp += 50;    //Öka xp som krävs för att gå upp till nästa level
            LevelUp();
        }
    }
    private void LevelUp() // Ökar players stats när level ökar
    {
        Level++;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{Name} reached level: {Level}", "WriteLine");
        double BaseHpAdded = BaseHp * 0.15;
        double BaseDamageAdded = BaseDamage * 0.15;
        double BaseResistanceAdded = BaseResistance * 0.15;
        double BaseAgilityAdded = BaseAgility * 0.15;
        BaseHp = BaseHp + BaseHpAdded;
        BaseDamage = BaseDamage + BaseDamageAdded;
        BaseResistance = BaseResistance + BaseResistanceAdded;
        BaseAgility = BaseAgility + BaseAgilityAdded;
        CurrentHp = BaseHp;
        Console.WriteLine($"+{BaseHpAdded:F0} Hp");
        Console.WriteLine($"+{BaseDamageAdded:F0} Damage");
        Console.WriteLine($"+{BaseResistanceAdded:F0} Resistance");
        Console.WriteLine($"+{BaseAgilityAdded:F0} Agility");
        Console.ResetColor();
    }
    #endregion
    #region DEFEND
    public string Defend(Player player, Enemy enemy, out string attackMessage)
    {
        string damage = enemy.Attack(player, out string message);
        double totalDamage = Convert.ToDouble(damage);
        totalDamage = totalDamage * 0.5;
        CurrentHp += totalDamage;
        attackMessage = "DEFENDED";
        return $"{totalDamage:F0}";
    }
    #endregion
}
