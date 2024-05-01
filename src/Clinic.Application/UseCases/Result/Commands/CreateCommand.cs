using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Result.Commands
{
    public class CreateCommand
    {
        public string Name { get; set; }
        public string PhotoBefore { get; set; }
        public string PhotoAfter { get; set; }
    }
}
