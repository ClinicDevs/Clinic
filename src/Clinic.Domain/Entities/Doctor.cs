﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Domain.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long StartWork { get; set; }
        public string TUsername { get; set; }
        public string? PicturePath { get; set; }

    }
}
