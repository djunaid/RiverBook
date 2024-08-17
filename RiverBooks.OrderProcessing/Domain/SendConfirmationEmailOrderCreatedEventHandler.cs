using MediatR;
using RiverBooks.EmailSending;
using RiverBooks.Users.Contracts;

namespace RiverBooks.OrderProcessing.Domain
{
    internal class SendConfirmationEmailOrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IMediator _mediator;

        public SendConfirmationEmailOrderCreatedEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken ct)
        {
            var csvBookDescription = string.Join(" ,", notification.NewOrder.OrderItems
                                                .Select(x =>
                                                {
                                                    return string.Join(": Quanatity ", x.Description, x.Quantity);
                                                }
                                                ));

            var userQuery = new UserDetailsByIdQuery(notification.NewOrder.UserID);
            var result = await _mediator.Send(userQuery);

            if (!result.IsSuccess)
            {
                //log the error
                return;
            }

            var command = new SendEmailCommand
            {
                To = result.Value.EmailAddress,
                From = "donotreply@riverbook.com",
                Body = $"Your order for book(s) {csvBookDescription} has been confirmed",
                Subject = "Thank you for ordering from RiverBook"
            };

            var EmaiId = await _mediator.Send(command);
        }
    }
}
