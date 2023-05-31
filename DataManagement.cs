namespace ShoppingListCli
{
    public static class DataManagement
    {
        public readonly static string AppdataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShoppingListCli");

        public static void PrepareAppdata()
        {
            if (!Path.Exists(AppdataFolder))
            {
                Directory.CreateDirectory(AppdataFolder);
            }
        }
        public static void SaveListToFile()
        {
            string jsonDump = JsonConvert.SerializeObject(Program.ListItems);
            string saveFilePath = GetPathToSaveTo();
            using (StreamWriter sw = new(saveFilePath))
            {
                sw.Write(jsonDump);
            }
        }

        public static string GetPathToSaveTo()
        {
            bool keepAskingForFilename = true;
            while (keepAskingForFilename)
            {
                System.Console.Write("Please enter a filename: ");
                string userInput = Console.ReadLine();
                if (userInput.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    System.Console.WriteLine("Invalid filename. Please try again.");
                }
                else if (userInput == String.Empty)
                {
                    System.Console.WriteLine("Please enter a filename.");
                }
                else if (Path.Exists(Path.Combine(AppdataFolder, userInput + ".json")))
                {
                    System.Console.Write("The file you have specified exists. Overwrite it? (y/n) ");
                    string userOverwriteResponse = Console.ReadLine();
                    if (userOverwriteResponse == "y")
                    {
                        keepAskingForFilename = false;
                        return Path.Combine(AppdataFolder, userInput + ".json");

                    }
                    else
                    {
                        System.Console.WriteLine("Cancelled. Try again.");
                    }
                }
                else
                {
                    return Path.Combine(AppdataFolder, userInput + ".json");
                }
            }
            return String.Empty;

        }

        public static void ListLoadingMenu()
        {
            bool keepAskingForFilename = true;
            while (keepAskingForFilename)
            {
                string[] availabeShoppingLists = GetAvailableShoppingLists();
                for (int i = 0; i < availabeShoppingLists.Length; i++)
                {
                    System.Console.WriteLine($"{i}. {availabeShoppingLists[i]}");
                }
                System.Console.WriteLine("Please type the index of the list you want to load.");
                string userInput = Console.ReadLine();
                int selectedIndex = 0;
                try
                {
                    if (Math.Abs(Convert.ToInt32(userInput)) < availabeShoppingLists.Length)
                    {
                        selectedIndex = Math.Abs(Convert.ToInt32(userInput));
                        LoadListFromFile(Path.Combine(AppdataFolder, availabeShoppingLists[selectedIndex]));
                        keepAskingForFilename = false;
                    }
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Try again.");
                }
            }

        }

        public static void LoadListFromFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader sr = new(path))
                    {
                        string jsonDump = sr.ReadToEnd();
                        Program.ListItems = JsonConvert.DeserializeObject<List<ShoppingListItem>>(jsonDump);
                        if (Program.ListItems == null) Program.ListItems = new();
                    }
                }
                catch (JsonException)
                {
                    System.Console.WriteLine("Could not load list. Is it corrupted?");
                    Program.ListItems = new();
                }
            }
        }

        public static string[] GetAvailableShoppingLists()
        {
            string[] availableListsPaths = Directory.GetFiles(AppdataFolder, "*.json");
            List<string> availableLists = new();
            foreach (string list in availableListsPaths)
            {
                availableLists.Add(list.Split("\\").Last<string>());
            }
            return availableLists.ToArray();
        }
    }
}