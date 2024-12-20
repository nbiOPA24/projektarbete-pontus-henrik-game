public class Archer : Enemy
{

    private bool isCharging;
    private int chargeCounter;

    public Archer(int level, string name)
    {
        double multiplier = level * 0.60;
        Random random = new Random();
        Name = name;
        Description = "Archer";
        BaseHp = (80 + random.Next(0, 15)) * multiplier; // Låg HP
        CurrentHp = TotalHp;
        BaseDamage = (18 + random.Next(0, 10)) * multiplier; // Standard attackskada
        BaseResistance = (10 + random.Next(0, 5)) * multiplier; // Låg motståndskraft
        BaseAgility = 10 * multiplier; // Hög rörlighet
        chargeCounter = 0;
        isCharging = true;
        healthBar = new HealthBar();
        XpDrop = 20;
    }

    public override void PrintCharacter(Enemy enemy)
    {
        Textures.PrintArcher();
    }
    public override void CharacterAttackAnimation(Enemy enemy)
    {
        if (isCharging)
        {
            Textures.ArcherAttackAnimation();
        }
        else 
        {
            Textures.ArcherSpecialAttackAnimation();
        }    
    }

    public override string Attack(Player player, out string attackMessage)
    {
        //Kontrollerar om specialattack ska göras annars vanlig attack
        double damageDone = CalculateDamage(player, out bool attackCrit);
        if (chargeCounter == 3)
        {
            damageDone = MultipleArrows(damageDone);
            chargeCounter = 0;
            isCharging = false;
            player.CurrentHp -= damageDone;
            attackMessage = "3x ARROWS!";
            return $"{damageDone:F0}";
        }
        else // Vanlig attack
        {
            isCharging = true;
            chargeCounter++;
            return base.Attack(player, out attackMessage);
        }
    }

    private double MultipleArrows(double damage)
    {
        damage = damage * 3; 
        return damage;
    }

    public override string TakeDamage(double damage, bool crit, out string attackMessage)
    {
        return base.TakeDamage(damage, crit, out attackMessage);
    }

}
