using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.News.Commands;
using Clinic.Domain.DTOs;
using Clinic.Domain.Entities;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.News.Handlers.CommandHandlers
{
    public class CreateNewsCommandHandler:IRequestHandler<CreateNewsCommand,ResponseModel>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public CreateNewsCommandHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public async Task<ResponseModel> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            New newModel = new New
            {
                Picture = request.Picture,
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                DoctorId = request.DoctorId,
            };

            try
            {
                await _clinincDbContext.News.AddAsync(newModel);
                await _clinincDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Message = "Successfully Created!",
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }
    }
}
