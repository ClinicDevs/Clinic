using Clinic.Application.UseCases.Services.Commands;
using Clinic.Application.UseCases.Services.Queries;
using Clinic.Domain.DTOs;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    [Route("api/[controller]/[action")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public ServiceController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<ResponseModel> CreateService(CreateServiceCommand request)
        {
            return await _mediatr.Send(request);
        }

        [HttpGet]
        public async Task<IEnumerable<Service>> GetAllServices()
        {
            return await _mediatr.Send(new GetAllServicesQuery());
        }

        [HttpGet]
        public async Task<Service> GetServiceById(GetByIdServiceQuery request)
        {
            return await _mediatr.Send(request);
        }

        [HttpPut]
        public async Task<ResponseModel> UpdateService(UpdateServiceCommand request)
        {
            return await _mediatr.Send(request);
        }

        [HttpDelete]
        public async Task<ResponseModel> DeleteService(DeleteServiceCommand request)
        {
            return await _mediatr.Send(request);
        }
    }
}
