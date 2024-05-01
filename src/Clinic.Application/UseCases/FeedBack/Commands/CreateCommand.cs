using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.FeedBack.Commands
{
    public class CreateCommand
    {
        public string? VideoPath { get; set; }
        public string Description { get; set; }
        public DateTimeOffset WritedDate { get; set; }
    }
}
