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
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, ResponseModel>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public DeleteNewsCommandHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public async Task<ResponseModel> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var news = await _clinincDbContext.News
                    .Where(x => x.IsDeleted == false)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (news == null)
                {
                    return new ResponseModel()
                    {
                        Message = "Not Found",
                        StatusCode = 500,
                        IsSuccess = false
                    };
                }

                news.IsDeleted = true;

                _clinincDbContext.News.Update(news);
                await _clinincDbContext.SaveChangesAsync(cancellationToken);

                return new ResponseModel()
                {
                    Message = "Succesfully Deleted",
                    IsSuccess = true,
                    StatusCode = 200
                };

            }catch (Exception ex)
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
