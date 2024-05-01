﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.UseCases.Education.Commands
{
    public class CreateCommand
    {
        public string Name { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public string Degree { get; set; }
    }
}
