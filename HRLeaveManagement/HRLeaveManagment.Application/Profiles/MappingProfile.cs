using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagment.Application.DTOs;
using HRLeaveManagment.Application.DTOs.EmployeeDetails;
using HRLeaveManagment.Application.DTOs.LeaveAllocation;
using HRLeaveManagment.Application.DTOs.LeaveRequest;
using HRLeaveManagment.Application.DTOs.LeaveType;
using HRLeaveManagment.Application.DTOs.Payroll;
using HRLeaveManagment.Application.DTOs.TemplateTime;
using HRLeaveManagment.Application.DTOs.TimeEntry;

namespace HRLeaveManagment.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestListDto>()
                .ForMember(dest => dest.DateRequested, opt => opt.MapFrom(src => src.DateRequested))
                .ReverseMap();
            CreateMap<LeaveRequest, CreateLeaveRequestDto>().ReverseMap();
            CreateMap<LeaveRequest, UpdateLeaveRequestDto>().ReverseMap();
            
            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, CreateLeaveTypeDto>().ReverseMap();

            CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation, CreateLeaveAllocationDto>().ReverseMap();
            CreateMap<LeaveAllocation, UpdateLeaveAllocationDto>().ReverseMap();

            CreateMap<TimeEntry, TimeEntryDto>().ReverseMap();
            CreateMap<TimeEntry, CreateTimeEntryDto>().ReverseMap();

            CreateMap<TemplateTime, TemplateTimeDto>().ReverseMap();
            CreateMap<TemplateTime, CreateTemplateTimeDto>().ReverseMap();

            CreateMap<EmployeeDetail, EmployeeDetailsDto>().ReverseMap();
            CreateMap<EmployeeDetail, PayrollEmployeeDetailsDto>().ReverseMap();
            CreateMap<EmployeeDetail, CreateEmployeeDetailsDto>().ReverseMap();
            CreateMap<EmployeeDetail, UpdateEmployeeDetailsDto>().ReverseMap();

            CreateMap<Payroll, PayrollDto>().ReverseMap();
            CreateMap<Payroll, CreatePayrollDto>().ReverseMap();
        }
    }
}
