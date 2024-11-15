using System.Security.Cryptography.X509Certificates;

public class HealthBar
{
    static string[] healthBar = new string[10];
    public HealthBar() { }

    private static void UpdateHealthBar(double percentHp)
    {
        int percentHealth = Convert.ToInt32(percentHp);     //Konverterar först från double till int
        int healthBarAmount = percentHealth / 10;   //om percentHP är 70%, delar vi detta med 10 för att få ut 7, så att vi skriver ut 7 av våra 10 element med "|", och resten tomma

        for (int i = 0; i < healthBar.Length; i++) // Loopa 10 gånger
        {
            healthBar[i] = " ";
        }

        for (int i = 0; i < healthBarAmount; i++) // Loopa 10 gånger
        {
            healthBar[i] = "|";
        }
    }

    public void PrintHealthBar(double percentHp, bool isPlayer)     //tar in GameObjects percentHp, och en bool som avgör om det är en enemy eller player
    {
        UpdateHealthBar(percentHp);     //Uppdaterar först healthBar-array baserat på percentHp
        Console.Write("[");

        for (int i = 0; i < healthBar.Length; i++) // Loopa 10 gånger
        {
            if (healthBar[i] == "|")
            {
                if (isPlayer)
                    PrintColor.BackgroundGreen(healthBar[i], "Write");      //Om det är en player, skriv ut i grönt
                else if (!isPlayer)
                    PrintColor.BackgroundRed(healthBar[i], "Write");        //Om det är en enemy, skriv ut i rött
            }
            else
            {
                Console.Write(healthBar[i]); // Skrivs ut tomma för samma längd
            }
        }
        Console.Write("]");
        Console.WriteLine();
    }

}