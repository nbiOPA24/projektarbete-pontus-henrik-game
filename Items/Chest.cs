public class Chest
{
    public int Gold { get; set; }
    public List<Item> ChestLoot { get; set; } = new List<Item>();
    
    public Chest() { }

    public Chest(List<Item> items)
    {
        Random rnd = new Random();
        int itemsInChest = rnd.Next(1, 3);
        Random random = new Random();
        int itemIndex;
        List<Item> itemstoAdd = new List<Item>();
        for (int i = 0; i < itemsInChest; i++)
        {
            itemIndex = random.Next(0, items.Count);
            itemstoAdd.Add(items[itemIndex]);       //Lägger till random items från listan som innehåller alla items som finns i spelet
        }
        Random goldDrop = new Random();
        Gold = goldDrop.Next(0, 11);    //Random hur mycket guld en kista innehåller, om ens något guld alls
        ChestLoot = itemstoAdd;
    }

    public void PrintChest()    //Skriver ut alla items som finns i en kista, anropas via player.Loot-metod
    {
        foreach (Item item in ChestLoot)
        {
            if (item is Consumable)
            {
                PrintColor.Red($"{item.ItemName}, {item.ItemType}", "WriteLine");
            }
            else
            {
                PrintColor.Green($"{item.ItemName}, {item.ItemType}", "WriteLine");
            }  
        }
        if (Gold > 0)
        {
            PrintColor.Yellow($"+{Gold} {'\u00A9'}", "WriteLine");
        }
    }
}