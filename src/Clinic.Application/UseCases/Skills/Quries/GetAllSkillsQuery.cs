using Clinic.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Skills.Quries
{
    public class GetAllSkillsQuery: IRequest<IEnumerable<Skill>>
    {
    }
}
