namespace ShoppingListCli
{
    public static class Program
    {
        public static List<ShoppingListItem> ListItems = new();
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
            string[] userArguments = userInput.Split(" ");
            ShoppingListItem NewShoppingListItem = new(userArguments[0], Convert.ToInt32(userArguments[1]));
            ShoppingListItem? ExistingItemInList = GetExistingItemInList(NewShoppingListItem);

            if (ExistingItemInList == null)
            {
                ListItems.Add(NewShoppingListItem);
            }
            else
            {
                ExistingItemInList.Count += NewShoppingListItem.Count;
            }
        }

        public static ShoppingListItem? GetExistingItemInList(ShoppingListItem ItemToBeAdded)
        {
            return ListItems.Find((x) => x.Name == ItemToBeAdded.Name);
        }

        public static void SortList()
        {
            ListItems.Sort((a, b) =>
            {
                return a.Name.CompareTo(b.Name);
            });
        }
        public static void OutputList()
        {
            foreach (ShoppingListItem item in ListItems)
            {
                System.Console.WriteLine($"{item.Count} {item.Name}");
            }
        }
    }
}