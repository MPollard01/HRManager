﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.DTOs.TimeEntry
{
    public interface ITimeEntryDto
    {
        public DateTime StartWeek { get; set; }
        public DateTime EndWeek { get; set; }
        public int Hours { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
