using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.Doctors.Commands;
using Clinic.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Doctors.Handlers.CommandHandlers
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, ResponseModel>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public DeleteDoctorCommandHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public async Task<ResponseModel> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _clinincDbContext.Doctors.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new Exception();

            doctor.IsDeleted = true;

            _clinincDbContext.Doctors.Update(doctor);
            await _clinincDbContext.SaveChangesAsync(cancellationToken);
            return new ResponseModel()
            {
                Message = "Successfully deleted",
                StatusCode = 200,
                IsSuccess = true
            };
        }
    }
}
