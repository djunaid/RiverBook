



using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;

namespace RiverBooks.Books;

public record UpdatePriceRequest (Guid Id, decimal Price);

public class UpdatePriceValidator : Validator<UpdatePriceRequest> {

    public UpdatePriceValidator(){

        RuleFor(x=> x.Id)
            .NotNull()
            .NotEqual(Guid.Empty)
            .WithMessage("A book id is required.");

        RuleFor(x=> x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price can not be less than 0.");

    }
}