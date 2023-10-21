using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RefrigeratorExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Refrigerator> refrigerators =  PreparingTheRefrigerator();
            ConsulAplication(refrigerators);
        }



        public static List<Refrigerator> PreparingTheRefrigerator()
        {
            List<Refrigerator> refrigerators = new List<Refrigerator>();
            Refrigerator refrigerator1 = new Refrigerator("BOSH", "RED", 3);
            Refrigerator refrigerator2 = new Refrigerator("YEL", "BLACK", 5);

            refrigerators.Add(refrigerator1);
            refrigerators.Add(refrigerator2);

            List<Item> items = new List<Item>();
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    items.Add(new Item("Gil", "food", "dairy", DateTime.Today.AddDays(i), 15 + i + 5));
                }
                for (int i = 0; i < 4; i++)
                {
                    items.Add(new Item("meet", "drink", "fur", DateTime.Today.AddDays(i - 5), 2 + i + 5));
                }
                foreach (var item in items)
                {
                    refrigerator1.PuttingItemInTheFridge(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return refrigerators;
        }


        public static void ConsulAplication(List<Refrigerator> refrigerators)
        {
            int choice;
            Refrigerator refrigerator1 = refrigerators[0];
            do
            {
                Console.WriteLine("Click 1: Print items in the fridge: ");
                Console.WriteLine("Click 2: Printing the remaining space in the refrigerator: ");
                Console.WriteLine("Click 3: Add an item to the fridge: ");
                Console.WriteLine("Click 4: Remove an item from the refrigerator: ");
                Console.WriteLine("Click 5: Cleans the refrigerator of expired products: ");
                Console.WriteLine("Click 6: What do I want to eat?");
                Console.WriteLine("Click 7: Print items sorted by expiration date: ");
                Console.WriteLine("Click 8: Print shelves arranged according to available space: ");
                Console.WriteLine("Click 9: Printing refrigerators arranged according to available space: ");
                Console.WriteLine("Click 10: Preparing the refrigerator for shopping");
                Console.WriteLine("Click 100: System shutdown: ");

                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    try
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine(refrigerator1.ToString());
                                break;
                            case 2:
                                Console.WriteLine("The free space left in the fridge: " + refrigerator1.FreeSpaceInFridge() + "sqm");
                                break;
                            case 3:
                                PuttingItemInTheFridge(refrigerator1);
                                break;
                            case 4:
                                Console.WriteLine("Enter item ID.");
                                string itemID = Console.ReadLine();
                                Console.WriteLine(refrigerator1.TakingItemOutOfFridge(itemID));
                                break;
                            case 5:
                                List<Item> items2 = refrigerator1.CleaningRefrigerator();
                                foreach (var item in items2)
                                {
                                    Console.WriteLine(item);
                                    break;
                                }
                                Console.WriteLine("There were no expired products. ");
                                break;
                            case 6:
                                Console.WriteLine("Enter kosher type: For meat enter meat \nfor dairy enter dairy \nfor parve enter fur.");
                                string koshertype = Console.ReadLine();
                                if (!koshertype.Equals("meat") && !koshertype.Equals("dairy") && !koshertype.Equals("fur"))
                                {
                                    throw new Exception("The input is incorrect");
                                }

                                Console.WriteLine("Enter item type: for food enter food \nfor drink enter drink.");
                                string itemtype = Console.ReadLine();
                                if (!itemtype.Equals("food") && !itemtype.Equals("drink"))
                                {
                                    throw new Exception("The input is incorrect");
                                }

                                List<Item> itemList = refrigerator1.WhatToEat(koshertype, itemtype);
                                foreach (var item in itemList)
                                {
                                    Console.WriteLine(item.ToString());
                                }
                                break;
                            case 7:
                                List<Item> itemList3 = refrigerator1.SortProductsByExpirationDate();
                                foreach (var item in itemList3)
                                {
                                    Console.WriteLine(item.ToString());
                                }
                                break;
                            case 8:
                                List<Shelf> shelf = refrigerator1.SortShelvesBySize();
                                foreach (var shelf1 in shelf)
                                {
                                    Console.WriteLine(shelf1.ToString());
                                }
                                Console.WriteLine();
                                break;
                            case 9:
                                List<Refrigerator> friges = SortRefrigeratorsAvailableSpace(refrigerators);
                                foreach (var refrigerator in friges)
                                {
                                    Console.WriteLine(refrigerator.ToString());
                                }
                                break;
                            case 10:
                                Console.WriteLine(refrigerator1.GettingReadyForShopping());
                                break;
                            case 100:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please choose again.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input.\nIf you wish to continue, enter the correct input. If you want to finish, press 100.");
                }
            } while (choice != 100);

        }

        public static List<Refrigerator> SortRefrigeratorsAvailableSpace(List<Refrigerator> refrigerators)
        {
            List<Refrigerator> sortedRefrigerators = refrigerators.OrderBy(r => r.FreeSpaceInFridge()).ToList();
            return sortedRefrigerators;
        }



        private static void PuttingItemInTheFridge(Refrigerator refrigerator1)
        {
            Console.WriteLine("Enter item name.");
            string name = Console.ReadLine();
            


            Console.WriteLine("Enter item type: for food enter-food, for drink enter-drink.");
            string itemType = Console.ReadLine();
            string ItemType;
            if (itemType.Equals("food") || itemType.Equals("drink"))
            {
                ItemType = itemType;
            }
            else
            {
                throw new Exception("The input is incorrect");
            }

            Console.WriteLine("Enter kosher type: For meat enter-meat, for dairy enter-dairy, for parve enter-fur.");
            string kosherType = Console.ReadLine();
            string KosherType;
            if (kosherType.Equals("meat") || kosherType.Equals("dairy") || kosherType.Equals("fur"))
            {
                KosherType = kosherType;
            }
            else
            {
                throw new Exception("The input is incorrect");
            }

            Console.WriteLine("Enter expiration date (dd/MM/yyyy).");
            string expirationDate = Console.ReadLine();
            DateTime newExpirationDate;
            DateTime ExpirationData;
            if (DateTime.TryParseExact(expirationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out newExpirationDate))
            {
                ExpirationData = newExpirationDate;
            }
            else
            {
                throw new Exception("The input is not in a valid date format.");
            }

            Console.WriteLine("Enter storage space (sqm).");
            string storageSpace = Console.ReadLine();
            double StorageSpace;
            if (double.TryParse(storageSpace, out double newStorageSpace))
            {
                if (newStorageSpace > 0)
                {
                    StorageSpace = newStorageSpace;
                }
                else
                {
                    throw new Exception("The item size is invalid.");
                }

            }
            else
            {
                throw new Exception("The input is incorrect");
            }

            refrigerator1.PuttingItemInTheFridge(new Item(name, ItemType, KosherType, ExpirationData, StorageSpace));

        }
    }
}









