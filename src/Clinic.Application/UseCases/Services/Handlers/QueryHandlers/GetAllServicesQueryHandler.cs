using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.Services.Queries;
using Clinic.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Services.Handlers.QueryHandlers
{
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, IEnumerable<Service>>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public GetAllServicesQueryHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public async Task<IEnumerable<Service>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return _clinincDbContext.Services.Where(s=>s.IsDeleted==false).Skip(request.PageIndex-1).Take(request.Size).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(message: "Somthing went wrong in Backend)", ex);
            }
        }
    }
}
