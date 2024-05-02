using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.News.Commands;
using Clinic.Domain.DTOs;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.News.Handlers.CommandHandlers
{
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, ResponseModel>
    {
        private readonly IClinincDbContext _clinincDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateNewsCommandHandler(IClinincDbContext clinincDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _clinincDbContext = clinincDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ResponseModel> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string fileName = "";
                string filePath = "";
                if (request.Picture is not null)
                {
                    var file = request.Picture;

                    try
                    {
                        fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        filePath = Path.Combine(_webHostEnvironment.WebRootPath, "NewPics", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }catch (Exception ex)
                    {
                        return new ResponseModel()
                        {
                            Message = ex.Message,
                            IsSuccess = false,
                            StatusCode = 500
                        };
                    }
                }
                var news = new New()
                {
                    PicturePath = "/NewPics" + filePath,
                    Title = request.Title,
                    Description = request.Description,
                    DoctorId = request.DoctorId
                };

                await _clinincDbContext.News.AddAsync(news);
                await _clinincDbContext.SaveChangesAsync();

                return new ResponseModel()
                {
                    Message = "Created",
                    StatusCode = 201,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Message = ex.Message,
                    IsSuccess = false,
                    StatusCode = 500
                };

            }
           
        }
    }
}
