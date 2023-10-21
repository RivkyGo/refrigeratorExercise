using System;
using System.Collections.Generic;
using System.Text;


namespace RefrigeratorExercise
{
    class Shelf
    {
        private const double SizeOfShelf = 50;
        public Guid ShelfId { get; set; }
        public int FloorNumberShelf { get; set; }
        public double SpaceOnShelf { get; set; }
        public List<Item> ItemsInShelf { get; set; }
        

        public Shelf(int floorNumberShelf)
        {
            ShelfId = Guid.NewGuid();
            FloorNumberShelf = floorNumberShelf;
            SpaceOnShelf = SizeOfShelf;
            ItemsInShelf = new List<Item>();
        }



        public void AddItemToShelf(Item item)
        {

            if (item.ShelfId.Equals(ShelfId))
            {
                if (SpaceOnShelf - item.StorageSpace >= 0)
                {
                    ItemsInShelf.Add(item);
                    SpaceOnShelf -= item.StorageSpace;
                }
                else
                {
                    throw new Exception("There is no room on the shelf for more items");
                }
            }
            else
            {
                throw new Exception("The item cannot be on this shelf");
            }
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Shelf ID: {ShelfId}");
            result.AppendLine($"Floor Number: {FloorNumberShelf}");
            result.AppendLine($"Space on Shelf: {SpaceOnShelf} sq. m.");
            result.AppendLine("Items on Shelf:\n");
            foreach (var item in ItemsInShelf)
            {
                result.AppendLine(item.ToString());
            }

            return result.ToString();
        }
    }
}
