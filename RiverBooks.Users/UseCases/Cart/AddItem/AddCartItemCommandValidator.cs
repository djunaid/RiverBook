using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.Users.UseCases.Cart.AddItem
{
    public class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
    {
        public AddCartItemCommandValidator()
        {
            RuleFor(x=> x.EmailAddress).NotEmpty()
                .WithMessage("Email is required.");

            RuleFor(x => x.Quantity).GreaterThan(0)
                .WithMessage("Quantity must be atleast 1.");

            RuleFor(x => x.BookId).NotEmpty()
                .WithMessage("Not a valid BookId.");
        }
    }
}
