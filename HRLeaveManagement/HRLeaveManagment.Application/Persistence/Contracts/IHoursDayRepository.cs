using HRLeaveManagement.Clean.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagment.Application.Persistence.Contracts
{
    public interface IHoursDayRepository : IRepository<HoursDay>
    {
        Task<List<HoursDay>> GetEmployeesHours(int id);
    }
}
