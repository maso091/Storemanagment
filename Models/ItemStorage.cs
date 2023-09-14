using System.Collections.Generic;
using System.Linq;

namespace PINProjekt.Models
{
    public class ItemStorage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();


    public bool CanAddItem(Item item)
        {
            return Items.Sum(i => i.Quantity) + item.Quantity <= Capacity;
        }

        public bool AddItem(Item item)
        {
            if (CanAddItem(item))
            {
               
                var existingItem = Items.FirstOrDefault(i => i.Name == item.Name);
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                }
                else
                {
                    Items.Add(item);
                }
                return true; 
            }
            else
            {
                return false; 
            }
        }

        public bool RemoveItem(Item item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                if (existingItem.Quantity > item.Quantity)
                {
                    existingItem.Quantity -= item.Quantity;
                }
                else
                {
                    Items.Remove(existingItem);
                }
                return true; 
            }
            else
            {
                return false;
            }
        }
    }
}
