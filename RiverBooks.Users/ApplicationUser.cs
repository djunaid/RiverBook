
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace RiverBooks.Users;

public class ApplicationUser : IdentityUser 
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = new();

    public void AddItemToCart(CartItem item){
        
        Guard.Against.Null(item);
        
        var existingBook = _cartItems.SingleOrDefault(c=> c.BookId == item.BookId);

        if(existingBook != null)
        {
            existingBook.UpdateQuantity(existingBook.Quantity + item.Quantity);
            return;
        }

        _cartItems.Add(item);
    }


}
