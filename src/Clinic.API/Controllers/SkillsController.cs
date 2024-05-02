﻿using Clinic.Application.UseCases.Skills.Commands;
using Clinic.Application.UseCases.Skills.Quries;
using Clinic.Domain.DTOs;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public SkillsController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<ResponseModel> CreateSkills(CreateSkillCommand request)
        {
            return await _mediatr.Send(request);
        }

        [HttpGet]
        public async Task<IEnumerable<Skill>> GetAllSkills(int pageIndex, int size)
        {
            return await _mediatr.Send(new GetAllSkillsQuery
            {
                PageIndex = pageIndex,
                Size = size
            });
        }

        [HttpGet]
        public async Task<Skill> GetSkillById(Guid Id)
        {
            return await _mediatr.Send(new GetSkillByIdQuery
            { 
                Id = Id 
            });
        }

        [HttpPut]
        public async Task<ResponseModel> UpdateSkill(UpdateSkillCommand request)
        {
            return await _mediatr.Send(request);
        }

        [HttpDelete]
        public async Task<ResponseModel> DeleteSkill(DeleteSkillCommand request)
        {
            return await _mediatr.Send(request);
        }
    }
}
