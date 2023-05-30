namespace ShoppingListCli
{
    public class ShoppingListItem : IEquatable<ShoppingListItem>
    {
        public string Name { get; private set; }
        public int Count { get; set; }

        public ShoppingListItem(string name, int count)
        {
            Name = name;
            Count = count;
        }


        public bool Equals(ShoppingListItem? other)
        {
            if (other == null)
            {
                return false;
            }
            return (this.Name == other.Name && this.Count == other.Count);
        }
    }
}