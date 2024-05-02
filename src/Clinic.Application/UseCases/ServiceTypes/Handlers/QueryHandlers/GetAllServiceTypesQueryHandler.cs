using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.ServiceTypes.Queries;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.ServiceTypes.Handlers.QueryHandlers
{
    public class GetAllServiceTypesQueryHandler : IRequestHandler<GetAllServiceTypesQuery, IEnumerable<ServiceType>>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public GetAllServiceTypesQueryHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public async Task<IEnumerable<ServiceType>> Handle(GetAllServiceTypesQuery request, CancellationToken cancellationToken)
        {
            return await _clinincDbContext.ServiceTypes.ToListAsync();

        }
    }
}
