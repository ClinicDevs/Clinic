using Clinic.Application.UseCases.ServiceTypes.Commands;
using Clinic.Application.UseCases.ServiceTypes.Handlers.QueryHandlers;
using Clinic.Application.UseCases.ServiceTypes.Queries;
using Clinic.Domain.DTOs;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Clinic.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ResponseModel> CreateServiceType(CreateServiceTypeCommand request)
        {
            return await _mediator.Send(request);
        }
        [HttpGet]
        public async Task<IEnumerable<ServiceType>> GetAllServiceType()
        {
            return await _mediator.Send(new GetAllServiceTypesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ServiceType> GetServiceTypeById(Guid id)
        {
            return await _mediator.Send(new GetServiceTypeByIdQuery()
            {
                Id = id
            });
        }
        [HttpPut("{id}")]
        public async Task<ResponseModel> UpdateServiceType(UpdateServiceTypeCommand request)
        {
            return await _mediator.Send(request);
        }
        [HttpDelete("{id}")]
        public async Task<ResponseModel> DeleteServiceType(Guid id)
        {
            return await _mediator.Send(new DeleteServiceTypeCommand()
            {
                Id= id
            });
        }

    }
}
