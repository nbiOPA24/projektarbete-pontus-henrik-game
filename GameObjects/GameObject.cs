using System.Text.Json.Serialization;
public class GameObject
{
    public bool isPlayer;
    public string Name { get; set; }
    public string Description { get; set; } //Assassin eller Mage osv
    public double BaseHp { get; set; }
    public virtual double TotalHp 
    {   
        get { return BaseHp; }
    }
    private double currentHp; // Varför detta?!
    public double CurrentHp //if CurrentHp < 0, CurrentHp = 0;
    { 
        get { return currentHp; }
        set
        {
            if (value < 0)      //Kontrollera så att currentHp inte kan bli mindre än 0 vid attack
            {
                currentHp = 0;
            }  
            else if (value > TotalHp)
            {
                currentHp = TotalHp;
            }
            else
            {
                currentHp = value;
            }
        } 
    }   
    [JsonIgnore]
    public double PercentHp
    {
        get { return CurrentHp / TotalHp * 100; }
    }
    public double BaseDamage {get; set;}
    public virtual double TotalDamage  // SENARE: lägg till + damage från items OM player har items
    {
        get { return BaseDamage; }
    } 
    public double BaseResistance {get; set;} // Beroende på armor, fiendeklass osv
    public virtual double TotalResistance 
    {   
        get { return BaseResistance; }
    }
    public double BaseAgility {get; set;} // Smidighet, crit-chance, dodge osv
    public virtual double TotalAgility
    {   
        get { return BaseAgility; }
    }


}