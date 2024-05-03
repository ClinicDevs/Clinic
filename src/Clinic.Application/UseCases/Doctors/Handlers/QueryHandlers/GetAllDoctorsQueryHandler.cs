using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.Diploms.Quries;
using Clinic.Application.UseCases.Doctors.Queries;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Doctors.Handlers.QueryHandlers
{
    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, IEnumerable<Doctor>>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public GetAllDoctorsQueryHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public async Task<IEnumerable<Doctor>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            return await _clinincDbContext.Doctors
                .Where(x => x.IsDeleted == false)
                    .Skip(request.PageIdex)
                        .Take(request.Size)
                            .ToListAsync();
        }
    }
}
