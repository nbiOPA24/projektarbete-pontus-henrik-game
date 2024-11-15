public class Merchant
{
    public string Name { get; set; }
    public int Gold { get; set; }
    public Inventory MerchantInventory { get; set; }

    public Merchant() { }
    public Merchant(string name, int gold, List<Item> items)
    {
        Name = name;
        Gold = gold;
        MerchantInventory = new Inventory();
        MerchantInventory.inventory = items;
    }

    public void Interact(Player player)     // Interaktion med merchant, loopar hans inventory, visar stats och pris och låter player köpa eller sälja    
    {
          // Lägg till textures där merchant säger tack för att vi räddat honom från fienden
        Textures.PrintSavedMerchant();
        var choice = Console.ReadKey(true); // För att slippa trycka enter
        Console.CursorVisible = false;

        if (choice.Key == ConsoleKey.Y)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Clear.Row(10, 33, 31);
            Clear.Row(11, 33, 31);
            Clear.Row(12, 33, 31);
            Clear.Row(14, 23, 12);
            Console.SetCursorPosition(33, 10);
            Write.OneLetterAtATime("Ok, please enter my shop!    \n");

            Thread.Sleep(2000);
        }
        else if (choice.Key == ConsoleKey.N)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Clear.Row(10, 33, 31);
            Clear.Row(11, 33, 31);
            Clear.Row(12, 33, 31);
            Clear.Row(14, 23, 12);
            Console.SetCursorPosition(33, 10);
            Write.OneLetterAtATime("Ok, thanks again for helping me! \n");

            Thread.Sleep(2000);
            return;
        }

        bool isShopping = true;
        while (isShopping)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.Write($"{Name}: ");
            PrintColor.Yellow($"{Gold} {'\u00A9'}", "WriteLine");
            Console.WriteLine();

            for (int i = 0; i < MerchantInventory.inventory.Count; i++)
            {
                if (MerchantInventory.inventory[i] is Consumable)
                {
                    Console.Write($"[{i + 1}] {MerchantInventory.inventory[i].ItemType, -13} {MerchantInventory.inventory[i].ItemName,-16}");  
                    PrintColor.Yellow($"{MerchantInventory.inventory[i].Price, 5} {'\u00A9'}", "WriteLine"); 
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"[{i + 1}] {MerchantInventory.inventory[i].ItemType, -13} {MerchantInventory.inventory[i].ItemName,-16}");  
                    PrintColor.Yellow($"{MerchantInventory.inventory[i].Price, 5} {'\u00A9'}", "WriteLine"); 
                    PrintColor.Blue($"{MerchantInventory.inventory[i].Health, 6} Hp {MerchantInventory.inventory[i].Damage,3} Dmg {MerchantInventory.inventory[i].Resistance,3} Res {MerchantInventory.inventory[i].Agility,3} Agi", "WriteLine"); 
                    Console.WriteLine();
                }  
            }

            Console.SetCursorPosition(45, 0);
            Console.Write($"{player.Name}: ");
            PrintColor.Yellow($"{player.Gold} {'\u00A9'}", "WriteLine");
            Console.WriteLine();
            Console.SetCursorPosition(45, 2);
            Console.ForegroundColor = ConsoleColor.Red;
            player.HealingPot.ShowItem();
            Console.ResetColor();
            PlayerEquipment.ShowWornGear(player);

            Console.SetCursorPosition(45, 16);
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine();
            Console.SetCursorPosition(45, 18);
            Console.WriteLine("[Q] Handla");
            Console.SetCursorPosition(45, 19);
            Console.WriteLine("[E] Sälja");
            Console.SetCursorPosition(45, 20);
            Console.WriteLine("[C] Lämna");

            var input = Console.ReadKey(true);
            if (input.Key == ConsoleKey.Q)
            {
                Buy(player);
            }
            else if (input.Key == ConsoleKey.E)
            {
                Sell(player);
            }
            else if (input.Key == ConsoleKey.C)
            {
                isShopping = false;
            }
        }
    }

    public void Buy(Player player) //Låter player köpa, flyttar pengar från player till merch, visar stats om det är gear, 
    {
        Console.Clear();
        Console.CursorVisible = false;
        Console.Write($"{Name}: ");
        PrintColor.Yellow($"{Gold} {'\u00A9'}", "WriteLine");
        Console.WriteLine();
        Console.WriteLine($"To buy:");
        Console.WriteLine();
        for (int i = 0; i < MerchantInventory.inventory.Count; i++)
        {
            if (MerchantInventory.inventory[i] is Consumable)
            {
                Console.Write($"[{i + 1}] {MerchantInventory.inventory[i].ItemType, -13} {MerchantInventory.inventory[i].ItemName,-16}");  
                PrintColor.Yellow($"{MerchantInventory.inventory[i].Price, 5} {'\u00A9'}", "WriteLine"); 
                Console.WriteLine();
            }
            else
            {
                Console.Write($"[{i + 1}] {MerchantInventory.inventory[i].ItemType, -13} {MerchantInventory.inventory[i].ItemName,-16}");  
                PrintColor.Yellow($"{MerchantInventory.inventory[i].Price, 5} {'\u00A9'}", "WriteLine"); 
                PrintColor.Blue($"{MerchantInventory.inventory[i].Health, 6} Hp {MerchantInventory.inventory[i].Damage,3} Dmg {MerchantInventory.inventory[i].Resistance,3} Res {MerchantInventory.inventory[i].Agility,3} Agi", "WriteLine"); 
                Console.WriteLine();
            }  
        }
        Console.WriteLine();
        
        Console.SetCursorPosition(45, 0);
        Console.Write($"{player.Name}: ");
        PrintColor.Yellow($"{player.Gold} {'\u00A9'}", "WriteLine");
        Console.SetCursorPosition(45, 2);
        Console.ForegroundColor = ConsoleColor.Red;
        player.HealingPot.ShowItem();
        Console.ResetColor();
        PlayerEquipment.ShowWornGear(player);

        Console.SetCursorPosition(45, 16);
        Console.WriteLine("Vad vill du köpa? - [C] för att lämna");
        var input = Console.ReadKey(true);
        if (input.Key == ConsoleKey.C)
        {
            return;
        }
        int itemIndex = Convert.ToInt32(input.KeyChar.ToString()) - 1;
        var itemToBuy = MerchantInventory.inventory[itemIndex];
        if (player.Gold >= itemToBuy.Price) // Kontrollerar om player har maximalt antal healing potions, annars genomförs "köpet"
        {
            if (itemToBuy is Consumable)
            {
                if (player.HealingPot.Ammount < player.HealingPot.MaxAmmount)
                {
                    player.Gold -= itemToBuy.Price;
                    player.HealingPot.Ammount++;
                    Gold += itemToBuy.Price;
                    Console.SetCursorPosition(45, 18);
                    Console.WriteLine("+1 Healing Potion");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.SetCursorPosition(45, 18);
                    Console.WriteLine("Du har det maximala antalet Healing Potions redan");
                    Thread.Sleep(1000);
                }
            }
            else
            {
                player.Gold -= itemToBuy.Price;
                Gold += itemToBuy.Price;
                player.Inventory.inventory.Add(itemToBuy);
                Console.SetCursorPosition(45, 18);
                Console.WriteLine($"Du har köpt {itemToBuy.ItemName}");
                Thread.Sleep(1000);
                
                MerchantInventory.inventory.RemoveAt(itemIndex);
                
            }
        }
        else
        {
            Console.WriteLine("Du har inte råd");
            Thread.Sleep(1000);
        }
    }

    public void Sell(Player player) // Säljmetod som fungerar likt ovan med bakvänt
    {
        Console.Clear();
        Console.CursorVisible = false;
        Console.Write($"{Name}: ");
        PrintColor.Yellow($"{Gold}g", "WriteLine");
        Console.WriteLine();
        Console.WriteLine($"To sell:");
        Console.WriteLine();
        player.Inventory.ShowInventorySortedByPrice();
    
        Console.WriteLine();

        Console.SetCursorPosition(45, 0);
        Console.Write($"{player.Name}: ");
        PrintColor.Yellow($"{player.Gold} {'\u00A9'}", "WriteLine");
        Console.SetCursorPosition(45, 2);
        Console.ForegroundColor = ConsoleColor.Red;
        player.HealingPot.ShowItem();
        Console.ResetColor();
        PlayerEquipment.ShowWornGear(player);

        Console.SetCursorPosition(45, 16);
        Console.WriteLine("Vad vill du sälja? - [C] för att lämna");
        var input = Console.ReadKey(true);
        if (input.Key == ConsoleKey.C)
        {
            return;
        }
        int itemIndex = Convert.ToInt32(input.KeyChar.ToString()) - 1;
        var itemToSell = player.Inventory.inventory[itemIndex];

        player.Gold += itemToSell.Price / 2;
        Gold -= itemToSell.Price / 2;
        MerchantInventory.inventory.Add(itemToSell);
        Console.SetCursorPosition(45, 18);
        Console.WriteLine($"Du har sålt {itemToSell.ItemName}");
        Thread.Sleep(1000);
        
        player.Inventory.inventory.RemoveAt(itemIndex);
     
    }
}