namespace ShoppingListCli
{
    public static class Program
    {
        public static List<string> ListItems = new();
        public static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                System.Console.Write("ShoppingList> ");
                string? userInput = Console.ReadLine();
                if (userInput != "stop")
                {
                    AddItem(userInput);
                }
                else
                {
                    exit = true;
                    SortList();
                    OutputList();
                }
            }
        }

        public static void AddItem(string userInput)
        {
            ListItems.Add(userInput);
        }

        public static void SortList()
        {
            ListItems.Sort();
        }
        public static void OutputList()
        {
            foreach (string item in ListItems)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}