using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.News.Commands;
using Clinic.Domain.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.News.Handlers.CommandHandlers
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, ResponseModel>
    {
        private readonly IClinincDbContext _ClinicDbContext;

        public UpdateNewsCommandHandler(IClinincDbContext clinicDbContext)
        {
            _ClinicDbContext = clinicDbContext;
        }

        public async Task<ResponseModel> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var news = await _ClinicDbContext.News.Where(x => x.IsDeleted == false)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);
                if (news == null)
                {
                    return new ResponseModel()
                    {
                        Message = "Not Found",
                        StatusCode = 404,
                        IsSuccess = false
                    };
                }

                news.Title = request.Title;
                news.Description = request.Description;
                news.DoctorId = request.DoctorId;

                _ClinicDbContext.News.Update(news);
                await _ClinicDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel()
                {
                    Message = "Successfully Updated",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex) 
            {
                return new ResponseModel()
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    IsSuccess = false
                };
            }
        }
    }
}
