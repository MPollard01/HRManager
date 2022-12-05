using AutoMapper;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateLeaveTypeDto, CreateLeaveTypeVM>().ReverseMap();
            CreateMap<CreateLeaveRequestDto, CreateLeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequestDto, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveRequestListDto, LeaveRequestVM>().ReverseMap();
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveAllocationDto, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocationDto, ViewLeaveAllocationsVM>().ReverseMap();
            CreateMap<RegisterVM, RegistrationRequest>().ReverseMap();
            CreateMap<EmployeeVM, Employee>().ReverseMap();
            CreateMap<TimeEntryDto, TimeEntryVM>().ReverseMap();
            CreateMap<CreateTimeEntryDto, CreateTimeEntryVM>().ReverseMap();
            CreateMap<CreateTimeEntryDto, TimeEntryVM>().ReverseMap();
            CreateMap<TemplateTimeDto, TemplateTimeVM>().ReverseMap();
            CreateMap<CreateTemplateTimeDto, CreateTemplateTimeVM>().ReverseMap();
            CreateMap<TemplateTimeVM, CreateTemplateTimeVM>().ReverseMap();
        }
    }
}
