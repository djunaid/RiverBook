
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using RiverBooks.SharedKernel;
using RiverBooks.Users.Interfaces;

namespace RiverBooks.Users.Domain;

public partial class ApplicationUser : IdentityUser, IHaveDomainEvents
{
    public string FullName { get; set; } = string.Empty;

    private readonly List<CartItem> _cartItems = new();
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    private readonly List<UserStreetAddress> _userAddresses = new();
    public IReadOnlyCollection<UserStreetAddress> UserAddresses => _userAddresses.AsReadOnly();

    private List<DomainEventBase> _domainEvents = new();

    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

    void IHaveDomainEvents.ClearDomainEvents() => _domainEvents.Clear();

    public void AddItemToCart(CartItem item)
    {

        Guard.Against.Null(item);

        var existingBook = _cartItems.SingleOrDefault(c => c.BookId == item.BookId);

        if (existingBook != null)
        {
            existingBook.UpdateQuantity(existingBook.Quantity + item.Quantity);

            existingBook.UpdateUnitPrice(item.UnitPrice);
            existingBook.UpdateDescription(item.Description);
            return;
        }

        _cartItems.Add(item);
    }

    internal void ClearCart()
    {
        _cartItems.Clear();
    }

    internal UserStreetAddress AddAddress(Address address)
    {
        Guard.Against.Null(address);

        var existingAddress = _userAddresses.SingleOrDefault(a => a.StreetAddress == address);

        if (existingAddress != null)
        {
            return existingAddress;
        }

        var newAddress = new UserStreetAddress(Guid.Parse(Id), address);
        _userAddresses.Add(newAddress);

        var domainEvent = new AddressAddedEvent(newAddress);
        RegisterDomainEvent(domainEvent);

        return newAddress;
    }
}
