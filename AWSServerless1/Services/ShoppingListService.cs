using AWSServerless1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerless1.Services 
{
    public class ShoppingListService : IShoppingListService
    {
        private readonly Dictionary<string, int> _shoppingListStorage = new Dictionary<string, int>();
        public Dictionary<string, int> GetItemsFromShoppingList()
        {
            return _shoppingListStorage;
        }

        public void AddItemToShoppingList(ShoppingListModel shoppingListItem)
        {
            _shoppingListStorage.Add(shoppingListItem.Name, shoppingListItem.Quantity);
        }

        public void DeleteItemFromShoppingList(string shoppingListName)
        {
            _shoppingListStorage.Remove(shoppingListName);
        }
    }
}
