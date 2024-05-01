using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Doctor.Commands
{
    public class CreateCommand
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int StartWork { get; set; }
        public string TUsername { get; set; }
        public string? PicturePath { get; set; }
    }
}
