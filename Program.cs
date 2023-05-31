namespace ShoppingListCli
{
    public static class Program
    {
        public static List<ShoppingListItem> UndoShadowList = new();
        public static List<ShoppingListItem> ListItems = new();


        public static void Main(string[] args)
        {
            DataManagement.PrepareAppdata();
            DataManagement.ListLoadingMenu();
            bool exit = false;
            while (!exit)
            {
                System.Console.Write("ShoppingList> ");
                string? userInput = Console.ReadLine();

                if (userInput != null && userInput != String.Empty)
                {
                    string[] userArguments = userInput.Split(" ");
                    List<string> commandArguments = new();
                    if (userArguments.Length > 1)
                    {
                        commandArguments = userArguments.ToList().GetRange(1, userArguments.Length - 1);
                    }
                    switch (userArguments[0])
                    {
                        case "add":
                            UpdateUndoShadow();
                            AddItemCommandInvoked(commandArguments);
                            break;
                        case "list":
                            SortList();
                            OutputList();
                            break;
                        case "remove":
                            UpdateUndoShadow();
                            RemoveCommandInvoked(commandArguments);
                            break;
                        case "clear":
                            UpdateUndoShadow();
                            ClearListCommandInvoked();
                            break;
                        case "undo":
                            RestoreUndoStack();
                            break;
                        case "exit":
                            exit = true;
                            DataManagement.SaveListToFile();
                            break;
                        default:
                            System.Console.WriteLine("Unknown or incomplete command.");
                            break;
                    }
                }
            }
        }


        public static void UpdateUndoShadow()
        {
            UndoShadowList = new(ListItems);
        }

        public static void RestoreUndoStack()
        {
            ListItems = new(UndoShadowList);
        }

        public static void ClearListCommandInvoked()
        {
            ListItems.Clear();
        }

        public static void AddItemCommandInvoked(IList<string> userArguments)
        {

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

        public static void RemoveCommandInvoked(IList<string> userArguments)
        {
            if (userArguments.Count < 2) { System.Console.WriteLine("Not enough arguments."); return; }
            if (userArguments[1] == "0") { System.Console.WriteLine("Cannot remove 0 of an item."); return; }
            if (userArguments[1] == "*") { userArguments[1] = "0"; }
            RemoveItemFromList(new ShoppingListItem(userArguments[0], Convert.ToInt32(userArguments[1])));
        }

        public static void RemoveItemFromList(ShoppingListItem ItemToRemove)
        {
            ShoppingListItem? ItemInList = ListItems.Find((x) => { return x.Name == ItemToRemove.Name; });
            if (ItemInList == null) { System.Console.WriteLine($"Cannot remove {ItemToRemove.Name} because it is not in the list!"); return; }
            if (ItemInList.Count < ItemToRemove.Count) { System.Console.WriteLine($"Unable to remove {ItemToRemove.Count} instances of {ItemToRemove.Name} because there are only {ItemInList.Count} occourences in the list."); return; }

            if (ItemToRemove.Count == 0 || ItemToRemove.Count == ItemInList.Count)
            {
                ListItems.Remove(ItemInList);
                return;
            }

            ItemInList.Count -= ItemToRemove.Count;

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