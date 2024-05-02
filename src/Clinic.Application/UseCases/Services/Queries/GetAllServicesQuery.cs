using Clinic.Domain.Entities;
using MediatR;

namespace Clinic.Application.UseCases.Services.Queries
{
    public class GetAllServicesQuery:IRequest<IEnumerable<Service>>
    {
    }
}
