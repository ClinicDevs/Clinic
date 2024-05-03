using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.Doctors.Queries;
using Clinic.Domain.DTOs;
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
            try
            {
                return await _clinincDbContext.Doctors.Where(d => d.IsDeleted == false).Skip(request.PageIndex - 1).Take(request.Size).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Something went Wrong (its not my problem)", ex);
            }
        }
    }
}
