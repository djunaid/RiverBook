

using FastEndpoints;
using FluentValidation;

namespace RiverBooks.Books;

internal class CreateRequest {

    public Guid? Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public decimal Price { get; set; } = 0;
}


internal class CreateRequestValidator : Validator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x=> x.Title).NotEmpty().NotNull().WithMessage("Title of the book is required.");

        RuleFor(x=> x.Author).NotEmpty().NotNull().WithMessage("Author is required.");

        RuleFor(x=> x.Price).GreaterThanOrEqualTo(0).WithMessage("Price can not be less than 0.");
    }
}


