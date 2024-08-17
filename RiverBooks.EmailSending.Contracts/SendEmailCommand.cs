using Ardalis.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiverBooks.EmailSending
{
    public class SendEmailCommand : IRequest<Result<Guid>>
    {
        public string To { get; set; } = string.Empty;

        public string From { get; set; } = string.Empty; 

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}
