using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Specialist.Commnads
{
    public class CreateCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
