using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.EmailSending.Integrations;

internal class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Result<Guid>>
{
    private readonly ISendEmail _emailSender;

    public SendEmailCommandHandler(ISendEmail emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task<Result<Guid>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await _emailSender.SendEmail(request.To, request.From, request.Subject, request.Body);

        return Guid.Empty;
    }
}
