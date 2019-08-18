using AWSServerless1.Controllers;
using System.Collections.Generic;

namespace AWSServerless1.Services
{
    public interface IShoppingListService
    {
        Dictionary<string, int> GetItemsFromShoppingList();
        void AddItemToShoppingList(ShoppingListModel shoppingListItem);
        void DeleteItemFromShoppingList(string shoppingListName);
    }
}