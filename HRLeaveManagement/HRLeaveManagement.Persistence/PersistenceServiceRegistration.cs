using HRLeaveManagement.Persistence.Repositories;
using HRLeaveManagment.Application.Persistence.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRLeaveManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
            services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
            services.AddScoped<IHoursDayRepository, HoursDayRepository>();
            services.AddScoped<ITemplateTimeRepository, TemplateTimeRepository>();
            services.AddScoped<IEmployeeDetailsRepository, EmployeeDetailsRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();

            return services;
        }
    }
}
