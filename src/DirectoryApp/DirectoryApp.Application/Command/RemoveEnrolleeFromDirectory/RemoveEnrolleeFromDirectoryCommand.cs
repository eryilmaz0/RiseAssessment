using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Application.Command.RemoveEnrolleeFromDirectory
{
    public class RemoveEnrolleeFromDirectoryCommand : IRequest<RemoveEnrolleeFromDirectoryResponse>
    {
        public Guid EnrolleeId { get; set; }
    }
}
