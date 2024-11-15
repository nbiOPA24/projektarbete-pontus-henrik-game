using System.Text.Json;
//using Newtonsoft.Json;
public static class Json
{
    public static string folderPath = Path.Combine("Json", "SavedFiles");
    //public static string folderPath = AppDomain.CurrentDomain.BaseDirectory;
    
    // public static void Save(Player player, List<Map> maps)
    // {
    //     string json = JsonSerializer.Serialize(player, new JsonSerializerOptions { WriteIndented = true });

    //     string fileName = player.Name;
    //     string filePath = Path.Combine(folderPath, $"Player_{fileName}.json");

    //     File.WriteAllText(filePath, json);

    //     string equippedGearJson = JsonSerializer.Serialize(player.EquippedGear);
    //     File.WriteAllText(Path.Combine(folderPath, $"EquippedItems_{player.Name}.json"), equippedGearJson);

    //     string inventoryJson = JsonSerializer.Serialize(player.Inventory);
    //     File.WriteAllText(Path.Combine(folderPath, $"Inventory_{player.Name}.json"), inventoryJson);

    //     Console.WriteLine($"Filen sparades: {filePath}");

    //     // Serialize maps with MaplevelJagged
    //     var mapsForSerialization = maps.Select(map => new
    //     {
    //         map.MaplevelJagged,
    //         // Add other properties of Map if needed
    //     }).ToList();

    //     json = JsonSerializer.Serialize(mapsForSerialization, new JsonSerializerOptions { WriteIndented = true });
    //     filePath = Path.Combine(folderPath, $"Maps_{fileName}.json");

    //     File.WriteAllText(filePath, json);

    //     Console.WriteLine($"Filen sparades: {filePath}");
    // }

    // public static void Load(string playerName, out Player player, out List<Map> maps)
    // {
    //     string playerFilePath = Path.Combine(folderPath, $"Player_{playerName}.json");
    //     string mapsFilePath = Path.Combine(folderPath, $"Maps_{playerName}.json");

    //     if (File.Exists(playerFilePath) && File.Exists(mapsFilePath))
    //     {
    //         string playerJson = File.ReadAllText(playerFilePath);
    //         player = JsonSerializer.Deserialize<Player>(playerJson);

    //         string mapsJson = File.ReadAllText(mapsFilePath);
    //         var mapsForDeserialization = JsonSerializer.Deserialize<List<Map>>(mapsJson);

    //         // Convert jagged array back to multidimensional array
    //         maps = mapsForDeserialization.Select(map => new Map
    //         {
    //             Maplevel = Map.ConvertToMultidimensionalArray(map.MaplevelJagged),
    //             // Add other properties of Map if needed
    //         }).ToList();

    //         string equippedGearFilePath = Path.Combine(folderPath, $"EquippedItems_{playerName}.json");
    //         if (File.Exists(equippedGearFilePath))
    //         {
    //             string equippedGearJson = File.ReadAllText(equippedGearFilePath);
    //             player.EquippedGear = JsonSerializer.Deserialize<EquippedGear>(equippedGearJson);
    //         }

    //         string inventoryFilePath = Path.Combine(folderPath, $"Inventory_{playerName}.json");
    //         if (File.Exists(inventoryFilePath))
    //         {
    //             string inventoryJson = File.ReadAllText(inventoryFilePath);
    //             player.Inventory = JsonSerializer.Deserialize<Inventory>(inventoryJson);
    //         }
    //     }
    //     else
    //     {
    //         throw new FileNotFoundException("Save files not found.");
    //     }
    // }

    
    
    
    
    
    // public static void AutoSave()
    // {
        
    // }
    public static void Save(Player player, List<Map> maps)
    {
        string json = JsonSerializer.Serialize(player, new JsonSerializerOptions { WriteIndented = true });

        // Skriv JSON-strängen till en fil
        //Console.Write("Enter name for the Save: ");
        string fileName = player.Name;
        //string fileName = Path.Combine(folderPath, saveName);
        string filePath = Path.Combine(folderPath, $"Player_{fileName}.json");

        File.WriteAllText(filePath, json);
        // Spara de separata objekten också
        string equippedGearJson = JsonSerializer.Serialize(player.EquippedGear);
        string itemFilePath = Path.Combine(folderPath, $"EquippedItems_{fileName}.json");
        File.WriteAllText(itemFilePath, equippedGearJson);

        string inventoryJson = JsonSerializer.Serialize(player.Inventory);
        string inventoryFilePath = Path.Combine(folderPath, $"Inventory_{fileName}.json");
        File.WriteAllText(inventoryFilePath, inventoryJson);

        Console.WriteLine($"Filen sparades: {filePath}");


        json = JsonSerializer.Serialize(maps, new JsonSerializerOptions { WriteIndented = true });

        // Skriv JSON-strängen till en fil
        //fileName = player.Name;//Path.Combine(folderPath, saveName);
        filePath = Path.Combine(folderPath, $"Maps_{fileName}.json");

        File.WriteAllText(filePath, json);

        Console.WriteLine($"Filen sparades: {filePath}");

    }

    //  public static void Load(string playerName, out Player player, out List<Map> maps)
    // {
    //     string playerFilePath = Path.Combine(folderPath, $"Player_{playerName}.json");
    //     string mapsFilePath = Path.Combine(folderPath, $"Maps_{playerName}.json");

    //     if (File.Exists(playerFilePath) && File.Exists(mapsFilePath))
    //     {
    //         string playerJson = File.ReadAllText(playerFilePath);
    //         player = JsonSerializer.Deserialize<Player>(playerJson);

    //         string mapsJson = File.ReadAllText(mapsFilePath);
    //         maps = JsonSerializer.Deserialize<List<Map>>(mapsJson);

    //         string equippedGearFilePath = Path.Combine(folderPath, $"EquippedItems_{playerName}.json");
    //         if (File.Exists(equippedGearFilePath))
    //         {
    //             string equippedGearJson = File.ReadAllText(equippedGearFilePath);
    //             player.EquippedGear = JsonSerializer.Deserialize<Item[]>(equippedGearJson);
    //         }

    //         string inventoryFilePath = Path.Combine(folderPath, $"Inventory_{playerName}.json");
    //         if (File.Exists(inventoryFilePath))
    //         {
    //             string inventoryJson = File.ReadAllText(inventoryFilePath);
    //             player.Inventory = JsonSerializer.Deserialize<Inventory>(inventoryJson);
    //         }
    //     }
    //     else
    //     {
    //         throw new FileNotFoundException("Save files not found.");
    //     }
    // }
    //Can you fix this method so it loads the player and maps from the selected file?
    public static void Load(Player shitPlayer, List<Map> shitMaps, out Player player, out List<Map> maps)
    {
        player = shitPlayer;
        maps = shitMaps;
        //Save(player, maps);
        //Skriv ut namnen på dom existerande json-filerna, lägg in vald fil som "filePath"
        if (Directory.Exists(folderPath))
        {
            // Hämta alla JSON-filer
            string[] allFiles = Directory.GetFiles(folderPath, "*.json");

            // Hämta unika `fileName` genom att matcha Player_ och Maps_
            var fileNames = allFiles
                .Select(file => Path.GetFileNameWithoutExtension(file)) // Ta bara filnamnet utan extension
                .Where(name => name.StartsWith("Player_") || name.StartsWith("Maps_")) // Filtrera Player_ och Maps_
                .Select(name => name.Substring(name.IndexOf('_') + 1)) // Extrahera `fileName` efter _
                .Distinct() // Ta bort dubletter
                .ToList();

            // Kontrollera om det finns några filnamn
            if (fileNames.Any())
            {
                Console.WriteLine("Tillgängliga sparningar:");

                // Skriv ut alla `fileName`
                for (int i = 0; i < fileNames.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {fileNames[i]}");
                }

                // Låt användaren välja ett `fileName`
                Console.Write("\nAnge numret på sparningen du vill öppna: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= fileNames.Count)
                {
                    string selectedFileName = fileNames[choice - 1];
                    Console.WriteLine($"\nDu valde: {selectedFileName}");

                    // Hitta tillhörande Player_ och Maps_ filer
                    string playerFile = Path.Combine(folderPath, $"Player_{selectedFileName}.json");
                    string mapsFile = Path.Combine(folderPath, $"Maps_{selectedFileName}.json");

                    // Läs och visa innehållet i båda filerna
                    if (File.Exists(playerFile))
                    {
                        string playerJson = File.ReadAllText(playerFile);
                        try
                        {
                            player = JsonSerializer.Deserialize<Player>(playerJson);
                            string equippedGearJson = File.ReadAllText("EquippedItems_" + player.Name + ".json");
                            player.EquippedGear = JsonSerializer.Deserialize<Item[]>(equippedGearJson);

                            string inventoryJson = File.ReadAllText("Inventory_" + player.Name + ".json");
                            player.Inventory = JsonSerializer.Deserialize<Inventory>(inventoryJson);
                            Console.WriteLine($"Spelare laddad: {player.Name}, Level: {player.Level}");
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"Fel vid laddning av spelardata: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Spelarfilen hittades inte: {playerFile}");
                    }
                // Läs och ladda `mapsFile`
                    // if (File.Exists(mapsFile))
                    // {
                    //     string mapsJson = File.ReadAllText(mapsFile);
                    //     try
                    //     {
                    //         maps = JsonSerializer.Deserialize<List<Map>>(mapsJson);
                    //         Console.WriteLine($"Kartor laddade: {maps.Count}");
                    //         int i = 0;
                    //         foreach (var map in maps)
                    //         {
                    //             i++;
                    //             Console.WriteLine(i);
                    //         }
                    //     }
                    //     catch (JsonException ex)
                    //     {
                    //         Console.WriteLine($"Fel vid laddning av kartdata: {ex.Message}");
                    //     }
                    // }
                    // else
                    // {
                    //     Console.WriteLine($"Kartfilen hittades inte: {mapsFile}");
                    // }
                }
                else
                {
                    Console.WriteLine("Ogiltigt val.");
                }
            }
            else
            {
                Console.WriteLine("Inga sparade filer hittades.");
            }
        }
        else
        {
            Console.WriteLine("Mappen 'SavedFiles' hittades inte.");
        }

        // if (File.Exists(playerFile))
        // {
        //     string playerJson = File.ReadAllText(playerFile);
        //     try
        //     {
        //         player = JsonSerializer.Deserialize<Player>(playerJson);
        //         Console.WriteLine($"Spelare laddad: {player.Name}, Level: {player.Level}");
        //     }
        //     catch (JsonException ex)
        //     {
        //         Console.WriteLine($"Fel vid laddning av spelardata: {ex.Message}");
        //     }
        // }
        // else
        // {
        //     Console.WriteLine($"Spelarfilen hittades inte: {playerFile}");
        // }

        // // Läs och ladda `mapsFile`
        // if (File.Exists(mapsFile))
        // {
        //     string mapsJson = File.ReadAllText(mapsFile);
        //     try
        //     {
        //         maps = JsonSerializer.Deserialize<List<Map>>(mapsJson);
        //         Console.WriteLine($"Kartor laddade: {maps.Count}");
        //         int i = 0;
        //         foreach (var map in maps)
        //         {
        //             i++;
        //             Console.WriteLine(i);
        //         }
        //     }
        //     catch (JsonException ex)
        //     {
        //         Console.WriteLine($"Fel vid laddning av kartdata: {ex.Message}");
        //     }
        // }
        // else
        // {
        //     Console.WriteLine($"Kartfilen hittades inte: {mapsFile}");
        // }

        // string json = JsonSerializer.Deserialize(player, new JsonDeserializerOptions { WriteIndented = true });

        // // Skriv JSON-strängen till en fil
        // //Console.Write("Enter name for the Save: ");
        // string saveName = player.Name;
        // string fileName = Path.Combine(folderPath, saveName);
        // string filePath = $"Player_{fileName}.json";

        // File.WriteAllText(filePath, json);

        // Console.WriteLine($"Filen sparades: {filePath}");


        // json = JsonSerializer.Serialize(maps, new JsonSerializerOptions { WriteIndented = true });

        // // Skriv JSON-strängen till en fil
        // fileName = Path.Combine(folderPath, saveName);
        // filePath = $"Maps_{fileName}.json";

        // File.WriteAllText(filePath, json);

        // Console.WriteLine($"Filen sparades: {filePath}");
    }
}