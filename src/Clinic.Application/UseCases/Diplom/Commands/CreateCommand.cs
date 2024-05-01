using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Diplom.Commands
{
    public class CreateCommand
    {
        public Guid LitsenzyaId { get; set; }
        public string PicturePath { get; set; }
    }
}
