using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MemoriesOfTheVoid.Core
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Texture2D Icon { get; set; }
    }

    public class Inventory
    {
        public List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();

        public void AddItem(InventoryItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(InventoryItem item)
        {
            Items.Remove(item);
        }

        public bool HasItem(string itemName)
        {
            return Items.Exists(item => item.Name == itemName);
        }

        public InventoryItem GetItem(string itemName)
        {
            return Items.Find(item => item.Name == itemName);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}