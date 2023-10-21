using System;


namespace RefrigeratorExercise
{
    class Item
    {
        public Guid ItemID { get; set; } 
        public string Name { get; set; }
        public Shelf ShelfId { get; set; }
        public string ItemType { get; set; }
        public string KosherType { get; set; }
        public DateTime ExpirationData { get; set; }
        public double StorageSpace { get; set; }

        public Item(string name, string itemType, string kosherType, DateTime expirationData, double storageSpace)
        {
            ItemID = Guid.NewGuid();
            Name = name;
            ItemType = itemType;
            KosherType = kosherType;
            ExpirationData = expirationData;
            StorageSpace = storageSpace;
        }

        

        public override string ToString()
        {
            return $"Item ID: {ItemID}\n" +
                 $"Name: {Name}\n" +
                 $"Item Type: {ItemType}\n" +
                 $"Kosher Type: {KosherType}\n" +
                 $"Expiration Date: {ExpirationData}\n" +
                 $"Storage Space: {StorageSpace} sq. m.\n";
        }
    }
}
