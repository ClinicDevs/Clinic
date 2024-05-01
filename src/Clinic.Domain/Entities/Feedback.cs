using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public string? VideoPath { get; set; }
        public string Description { get; set; }
        public DateTimeOffset WritedDate { get; set; }
    }
}
