public class Inventory
{
    public List<Item> inventory { get; set; } = new List<Item>();

    public Inventory() { }

    public void InventoryUI(Player player) // NÄR VI TRYCKER C
    {
        while (true)    //Anropar en massa olika metoder för att ge oss en sammanställd inventoryUI 
        {
            Console.Clear();
            PlayerUI.UI(player);    //visar guld, hp, xp högst upp
            Console.WriteLine();
            Textures.PrintPlayerCharacter(4, 21);       //Skriver ut players streckgubbe i mitten av inventoryn
            PlayerUI.ShowStats(player);             //Skriver ut players totala stats
            Console.WriteLine();

            Console.WriteLine();
            ShowInventory();        //Skriver ut items som vi har lootat, om den inte är tom
            Console.WriteLine();
            PlayerEquipment.ShowWornGear(player);       //Skriver ut items som vi har equippat
            Console.ResetColor();
            Console.SetCursorPosition(45, 14);
            Console.WriteLine("Tryck 'E' för att hantera equipments");
            Console.SetCursorPosition(45, 15);
            Console.WriteLine("Tryck 'C' för att gå tillbaka");
            var keyInput = Console.ReadKey(true);
            if (keyInput.Key == ConsoleKey.E)
            {
                EquipmentMenu(player);  //Om användaren tycker 'E' så går vi in i denna metod, som låter användaren välja ett item i inventoryn för att equippa det
            }
            else if (keyInput.Key == ConsoleKey.C)
            {
                return;
            }
            Console.WriteLine();
        }
    }
    private void ShowInventory()        //Skriver ut alla items i inventoryn
    {
        PrintColor.DarkGreen($"Inventory - Space: {inventory.Count}/10", "WriteLine");
        Console.WriteLine("---------------------");
        if (inventory.Count > 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.Write($"[{i + 1}] {inventory[i].ItemType,-8} {inventory[i].ItemName,-15}"); PrintColor.Yellow($"{inventory[i].Price / 2,7} {'\u00A9',1}", "WriteLine");
            }
        }
        else
        {
            Console.WriteLine("Din inventory är tom");
        }
    }

    public void ShowInventorySortedByPrice()        //Skriver ut alla items i inventoryn
    {

        if (inventory.Count > 0)
        {
            inventory.Sort((item1, item2) => item2.Price.CompareTo(item1.Price)); // Sortera inventory-listan efter pris i fallande ordning

            for (int i = 0; i < inventory.Count; i++)
            {
                Console.Write($"[{i + 1}] {inventory[i].ItemType, -13} {inventory[i].ItemName,-16}");  
                PrintColor.Yellow($"{inventory[i].Price/2, 5} {'\u00A9'}", "WriteLine"); 
                PrintColor.Blue($"{inventory[i].Health, 6} Hp {inventory[i].Damage,3} Dmg {inventory[i].Resistance,3} Res {inventory[i].Agility,3} Agi", "WriteLine"); 
                Console.WriteLine();    
            }
        }
        else
        {
            Console.WriteLine("Din inventory är tom");
        }
    }

    private void EquipmentMenu(Player player)
    {
        Console.Clear();
        PlayerEquipment.ShowWornGear(player);   // Skriv ut gear som player har equippat
        Console.WriteLine();
        player.Inventory.ShowEquipmentInventory();  // Skriv ut gear som player har i inventory tillsammans med gearens stats

        Console.WriteLine("Välj ett item för att interagera ([C] - tillbaka):");

        while (true)
        {
            var input = Console.ReadKey(true);  // Läs användarens inmatning utan att visa den på skärmen
            if (input.Key == ConsoleKey.C)  // Om användaren trycker 'C', gå tillbaka till InventoryUI
            {
                return;
            }

            string strInput = input.KeyChar.ToString();  // Om inmatningen inte är 'C', hantera den som ett val
            int index;

            if (int.TryParse(strInput, out index) && index >= 1 && index <= player.Inventory.inventory.Count) // Försök att parsa inmatningen till ett heltal
            {
                PlayerEquipment.CheckGearType(player, player.Inventory.inventory[index - 1]); // Om det är ett giltigt index, kolla itemets typ och jämför stats
                break;  // Avsluta loop
            }
            else
            {
                Console.WriteLine("Ogiltigt val, välj ett giltigt index eller tryck [C] för att gå tillbaka.");
            }
        }
    }

    private void ShowEquipmentInventory() // För att visa inventory MED stats
    {
        PrintColor.DarkGreen($"Inventory - Space: {inventory.Count}/10", "WriteLine");
        if (inventory.Count > 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.Write($"[{i + 1}] ");
                inventory[i].ShowStats();
            }
        }
        else
        {
            Console.WriteLine("Din inventory är tom");
        }
        Console.WriteLine();
    }

}