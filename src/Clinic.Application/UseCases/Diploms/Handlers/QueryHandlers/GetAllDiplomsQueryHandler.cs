﻿using Clinic.Application.Abstractions;
using Clinic.Application.UseCases.Diploms.Quries;
using Clinic.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Diploms.Handlers.QueryHandlers
{
    public class GetAllDiplomsQueryHandler : IRequestHandler<GetAllDiplomsQuery, IEnumerable<Diplom>>
    {
        private readonly IClinincDbContext _clinincDbContext;

        public GetAllDiplomsQueryHandler(IClinincDbContext clinincDbContext)
        {
            _clinincDbContext = clinincDbContext;
        }

        public async Task<IEnumerable<Diplom>> Handle(GetAllDiplomsQuery request, CancellationToken cancellationToken)
        {
            return await _clinincDbContext.Diploms.ToListAsync();
        }
    }
}