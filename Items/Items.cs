
using System.Diagnostics.Contracts;
public abstract class Item
{
    public string ItemType { get; set; }
    public string ItemName { get; set; }
    public double Damage { get; set; }
    public double Health { get; set; }
    public double Resistance { get; set; }
    public double Agility { get; set; }
    public int Price { get; set; }

    public abstract void ShowItem();
    public abstract void ShowStats();
}
public class Gear : Item
{
    public Gear() { }
    public Gear(string name, double damage, double health, double resistance, double agility)
    {
        ItemName = name;
        Damage = damage;
        Health = health;
        Resistance = resistance;
        Agility = agility;
        Price = CalculatePrice(this);
    }

    public override void ShowItem()
    {
        PrintColor.Green($"{ItemType}: {ItemName,10}, {Price/2}g", "WriteLine");
    }

    public override void ShowStats()
    {
        Console.WriteLine($"{ItemType, -8} {ItemName,-16}   {Health,3} Hp {Damage,3} Dmg {Resistance,3} Res {Agility,3} Agi");
    }
    public static int CalculatePrice(Item item)     //Räknar ut värdet på ett item baserat på statsen
    {
        int price;      

        double pricePerStat = 2;
        double totalPrice = 0;
        
        totalPrice += (item.Damage + item.Health + item.Resistance + item.Agility) * pricePerStat;
        price = Convert.ToInt32(totalPrice);

        return price;
    }
}

//Olika typer av Gear/Equipment
public class THelm : Gear
{
    public THelm() { }
    public THelm(string name, double damage, double health, double resistance, double agility)
    : base(name, damage, health, resistance, agility)
    {
        ItemType = "Helm";
    }
}
public class TBreastPlate : Gear
{
    public TBreastPlate() { }
    public TBreastPlate(string name, double damage, double health, double resistance, double agility)
    : base(name, damage, health, resistance, agility)
    {
        ItemType = "Chest";
    }
}
public class TLegs : Gear
{
    public TLegs() { }
    public TLegs(string name, double damage, double health, double resistance, double agility)
    : base(name, damage, health, resistance, agility)
    {
        ItemType = "Legs";
    }
}
public class TBoots : Gear
{
    public TBoots() { }
    public TBoots(string name, double damage, double health, double resistance, double agility)
    : base(name, damage, health, resistance, agility)
    {
        ItemType = "Boots";
    }
}
public class TGloves : Gear
{
    public TGloves() { }
    public TGloves(string name, double damage, double health, double resistance, double agility)
    : base(name, damage, health, resistance, agility)
    {
        ItemType = "Gloves";
    }
}
public class TWeapon : Gear
{
    public TWeapon() { }
    public string WeaponType { get; set; }
    public TWeapon(string name, string weaponType, double damage, double health, double resistance, double agility)
    : base(name, damage, health, resistance, agility)
    {
        WeaponType = weaponType;
        ItemType = "Weapon";
    }
}





