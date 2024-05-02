using Clinic.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.ServiceTypes.Queries
{
    public class GetAllServiceTypesQuery : IRequest<IEnumerable<ServiceType>>
    {
    }
}
