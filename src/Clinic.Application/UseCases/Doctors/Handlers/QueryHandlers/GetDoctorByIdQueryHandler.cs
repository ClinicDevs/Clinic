using Clinic.Application.Abstractions;
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
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, Doctor>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public GetDoctorByIdQueryHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public async Task<Doctor> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _clinincDbContext.Doctors.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == request.Id);

            return doctor ?? throw new Exception();
        }
    }
}
