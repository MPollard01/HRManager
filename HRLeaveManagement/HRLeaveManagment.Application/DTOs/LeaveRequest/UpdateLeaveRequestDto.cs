﻿using HRLeaveManagment.Application.DTOs.Common;

namespace HRLeaveManagment.Application.DTOs.LeaveRequest
{
    public class UpdateLeaveRequestDto : UpdateBaseDto, ILeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
        public string? RequestComments { get; set; }
        public bool Cancelled { get; set; }
    }
}
