using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RefrigeratorExercise
{
    class Refrigerator
    {
        
        public Guid RefrigeratorId { get; set; }
        public string RefrigeratorModel { get; set; }
        public string RefrigeratorColor { get; set; }
        public int NumOfShelves { get; set; }
        public List<Shelf> Shelves { get; set; }
        

        public Refrigerator(string refrigeratorModel, string refrigeratorColor, int numOfShelves)
        {
            RefrigeratorId = Guid.NewGuid();
            RefrigeratorModel = refrigeratorModel;
            RefrigeratorColor = refrigeratorColor;
            if (numOfShelves > 0)
                NumOfShelves = numOfShelves;
            else  // erorr
                NumOfShelves = 0; // My decision is arbitrary
            
            Shelves = new List<Shelf>();
            for (int index = 1; index <= numOfShelves; index++)
            {
                AddShelf(index);
            }
        }


        private void AddShelf(int ShelfFloorNumber)
        {
            Shelf newsShelf = new Shelf(ShelfFloorNumber);
            Shelves.Add(newsShelf);
        }


        public void PuttingItemInTheFridge(Item item)
        {
            double storageSpace = item.StorageSpace;
            bool itemOnShelf = false;
            foreach (var shelf in Shelves)
            {
                if (shelf.SpaceOnShelf >= storageSpace)
                {
                    shelf.ItemsInShelf.Add(item);
                    item.ShelfId = shelf; 
                    shelf.SpaceOnShelf -= item.StorageSpace;
                    itemOnShelf = true;
                    break;
                }
            }
            if (itemOnShelf == false)
            {
                throw new Exception("there is no enough place in the fridge");
            }
        }


        public List<Item> CleaningRefrigerator()
        {
            List<Item> expiredItems = new List<Item>();
            foreach (var shelf in Shelves)
            {
                for (int i = shelf.ItemsInShelf.Count - 1; i >= 0; i--)
                {
                    Item item = shelf.ItemsInShelf[i];
                    if (item.ExpirationData < DateTime.Today)
                    {
                        shelf.SpaceOnShelf += item.StorageSpace;
                        expiredItems.Add(item);
                        shelf.ItemsInShelf.RemoveAt(i);
                    }
                }
            }
            return expiredItems;
        }



        

        public List<Item> WhatToEat(string kosherType, string itemType)
        {
            List<Item> itemsToEat = new List<Item>();
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.ItemsInShelf)
                {
                    if (item.KosherType.Equals(kosherType, StringComparison.OrdinalIgnoreCase) && item.ItemType.Equals(itemType, StringComparison.OrdinalIgnoreCase) && item.ExpirationData > DateTime.Today)
                    {
                        itemsToEat.Add(item);
                    }
                }
            }
            if (itemsToEat.Count == 0)
            {
                throw new Exception("There are no foods that match your requirements in the refrigerator.");
            }
            return itemsToEat;
        }


        

        public double FreeSpaceInFridge()
        {
            double freeSpaceInFridge = 0;
            for (int index = 0; index < NumOfShelves; index++)
            {
                freeSpaceInFridge += Shelves[index].SpaceOnShelf;
            }
            return freeSpaceInFridge;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Refrigerator ID: {RefrigeratorId}");
            result.AppendLine($"Model: {RefrigeratorModel}");
            result.AppendLine($"Color: {RefrigeratorColor}");
            result.AppendLine($"Number of Shelves: {NumOfShelves}");
            result.AppendLine("Shelves Contents:\n");
            foreach (var shelf in Shelves)
            {
                result.AppendLine(shelf.ToString());
            }

            return result.ToString();
            
        }

        public Item TakingItemOutOfFridge(string userGuid)
        {
            Item outItem = null;
            bool isExistItem = false;
            if (Guid.TryParse(userGuid, out Guid itemId))
            {
                foreach (var shelf in Shelves)
                {
                    if (isExistItem)
                    {
                        break;
                    }
                    foreach (var item in shelf.ItemsInShelf)
                    {
                        if (item.ItemID.Equals(itemId))
                        {
                            isExistItem = true;
                            outItem = item;
                            shelf.ItemsInShelf.Remove(item);
                            shelf.SpaceOnShelf += outItem.StorageSpace;
                            break;
                        }
                    }
                }
                if (!isExistItem)
                {
                    throw new Exception("the item doesnt exist"); 
                }
            }
            else
            {
                throw new Exception("The input you entered is invalid"); 
            }
            return outItem;
        }


        public List<Item> SortProductsByExpirationDate()
        {
            List<Item> itemsByExpirationDate = new List<Item>();

            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.ItemsInShelf)
                {
                    itemsByExpirationDate.Add(item);
                }
            }
            if (itemsByExpirationDate.Count == 0)
            {
                throw new Exception("there is no items in the fridge");
            }
            List<Item> sortedItemsByExpirationDate = itemsByExpirationDate.OrderBy(item => item.ExpirationData).ToList();

            return sortedItemsByExpirationDate;
        }


        public List<Shelf> SortShelvesBySize()
        {

            List<Shelf> sortShelvesBySize = Shelves.OrderBy(shelf => shelf.SpaceOnShelf).ToList();

            if (Shelves.Count == 0)
            {
                throw new Exception("there is no shelves in the fridge");
            }

            return sortShelvesBySize;
        }


        public string GettingReadyForShopping()
        {
            List<Item> allTheExpiredItems = new List<Item>();

            if (FreeSpaceInFridge() > 20)
            {
                return "you can do shopping:)";
            }

            CleaningRefrigerator();
            double freeSpaceInFridge = FreeSpaceInFridge();
            if (freeSpaceInFridge > 20)
            {
                return "you can do shopping:)";
            }
            var result1 = RemovalProductsByValidity("dairy", 3);
            List<Item> expiredItemsDairy = result1.expiredItems;
            allTheExpiredItems.AddRange(expiredItemsDairy);

            if (freeSpaceInFridge + result1.spaceFree > 20)
            {
                return RemovalProductsByValidity(allTheExpiredItems);
            }
            freeSpaceInFridge += result1.spaceFree;

            var result2 = RemovalProductsByValidity("meat", 7);
            List<Item> expiredItemsMeat = result2.expiredItems;
            allTheExpiredItems.AddRange(expiredItemsMeat);
            if (freeSpaceInFridge + result2.spaceFree > 20)
            {
                return RemovalProductsByValidity(allTheExpiredItems);
            }
            freeSpaceInFridge += result2.spaceFree;

            var result3 = RemovalProductsByValidity("fur", 1);
            List<Item> expiredItemsFur = result3.expiredItems;
            allTheExpiredItems.AddRange(expiredItemsFur);
            if (freeSpaceInFridge + result3.spaceFree > 20)
            {
                return RemovalProductsByValidity(allTheExpiredItems);
            }
            return " The fridge is full, not the time to do shopping.";
            
        }

        private string RemovalProductsByValidity(List<Item> allTheExpiredItems)
        {
            string removeItems = "";
            foreach (var shelf in Shelves)
            {
                for (int i = shelf.ItemsInShelf.Count - 1; i >= 0; i--)
                {
                    Item item = shelf.ItemsInShelf[i];
                    if (allTheExpiredItems.Contains(item))
                    {
                        removeItems += item.Name +"\n";
                        shelf.ItemsInShelf.RemoveAt(i);
                        shelf.SpaceOnShelf += item.StorageSpace;
                    }
                }
            }
            return removeItems + "this items have been removed from the fridge, now you can do shopping:)";
        }


        private (List<Item> expiredItems , double spaceFree) RemovalProductsByValidity(string itemType, int day)
        {
            List<Item> expiredItems = new List<Item>();
            double spaceFree = 0;
            foreach (var shelf in Shelves)
            {
                foreach (var item in shelf.ItemsInShelf)
                {
                    if (item.KosherType.Equals(itemType))
                    {
                        if (item.ExpirationData.AddDays(-day) < DateTime.Today)
                        {
                            spaceFree += item.StorageSpace;
                            expiredItems.Add(item);
                        }
                    }
                        
                }
            }
            return (expiredItems , spaceFree);

        }
    }
}
