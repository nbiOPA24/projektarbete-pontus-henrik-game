public static class Clear   //Använder SetCursorPosition för att "Cleara" specifika ställen i consolen
{
    //Metod för att Cleara bara en rad istället för hela consolen.
    public static void Row(int line)
    {
        int currentLine = Console.CursorTop;         // Spara aktuell radposition
        Console.SetCursorPosition(0, line);          // Flytta till den rad som ska rensas
        Console.Write(new string(' ', Console.WindowWidth)); // Skriv tomma mellanslag över hela raden
        Console.SetCursorPosition(0, currentLine);   // Flytta tillbaka markören till ursprunglig position
    }

    public static void Damage()    //Rensar raden där enemyDamage skrivs ut
    {
        Console.SetCursorPosition(18, 5);   // Flytta till den rad som ska rensas
        Console.Write(new string(' ', 20)); // Skriv tomma mellanslag där damage skrivs ut
        Console.SetCursorPosition(18, 6);   // Flytta till den rad som ska rensas
        Console.Write(new string(' ', 20)); // Skriv tomma mellanslag där damage skrivs ut
        Console.SetCursorPosition(18, 7);   // Flytta till den rad som ska rensas
        Console.Write(new string(' ', 20)); // Skriv tomma mellanslag där damage skrivs ut
        
        Console.SetCursorPosition(7, 12);   // Flytta tillbaka markören till positionen där "Input: " skrivs ut
    }

    public static void PlayerHp()   //Rensar raden där playerHp skrivs ut
    {
        Console.SetCursorPosition(0, 2);    // Flytta till den rad som ska rensas
        Console.Write(new string(' ', 18)); // Skriv tomma mellanslag där playerHp skrivs ut
        Console.SetCursorPosition(0, 3);
        Console.Write(new string(' ', 18));
        Console.SetCursorPosition(7, 12);   // Flytta tillbaka markören till positionen där "Input: " skrivs ut
    }

    public static void EnemyHp()    //Rensar raden där enemyHp skrivs ut
    {
        Console.SetCursorPosition(38, 2);   // Flytta till den rad som ska rensas
        Console.Write(new string(' ', 18)); // Skriv tomma mellanslag där enemyHp skrivs ut
        Console.SetCursorPosition(38, 3);
        Console.Write(new string(' ', 18));
        Console.SetCursorPosition(7, 12);   // Flytta tillbaka markören till positionen där "Input: " skrivs ut
    }
}

public static class Write
{
    //Metod för att skriva ut text på en specifik rad och position via SetCursorPosition
    public static void MultipleLines(List<string> textToWrite, int startLine, int linePosition)
    {
        for (int i = 0; i < textToWrite.Count; i++)
        {
            Console.SetCursorPosition(linePosition, startLine);
            Console.WriteLine(textToWrite[i]);
            startLine++;
        }
        Console.SetCursorPosition(0, startLine);
    }

    
}

//Klass med metoder för att skriva ut olika färger på texten
public static class PrintColor
{
    public static void Red(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void Green(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void Yellow(string stringToPrint, string Write)
    {
             if (Write == "WriteLine")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void Blue(string stringToPrint, string Write)
    {
             if (Write == "WriteLine")
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void Gray(string stringToPrint, string Write)
    {
             if (Write == "WriteLine")
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void DarkYellow(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void DarkGreen(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void Magenta(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void BackgroundDarkCyan(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void BackgroundDarkGray(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void BackgroundDarkRed(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void BackgroundDarkYellow(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void BackgroundDarkBlue(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }

        else if (Write == "Write")
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void BackgroundRed(string stringToPrint, string Write)
    {
        if (Write == "WriteLine")
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }
        else if (Write == "Write")
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }
    public static void BackgroundGreen(string stringToPrint, string Write)
    {  
        if (Write == "WriteLine")
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine(stringToPrint);
            Console.ResetColor();
        }
        else if (Write == "Write")
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.Write(stringToPrint);
            Console.ResetColor();
        }
    }

}

public static class Misc
{
    public static string FirstToUpper(string input)
    {
        input = char.ToUpper(input[0]) + input.Substring(1).ToLower();
        return input;
    }
}