using HRLeaveManagement.Clean.Domain;

namespace HRLeaveManagement.Persistence
{
    public class Seed
    {
        public static async Task SeedData(HRLeaveManagementDbContext context)
        {
            if (!context.LeaveTypes.Any())
            {
                var leaveTypes = new List<LeaveType>
                {
                    new LeaveType
                    {
                        Id = 1,
                        DefaultDays = 10,
                        Name = "Vacation",
                        DateCreated = DateTime.UtcNow,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow
                    },
                    new LeaveType
                    {
                        Id = 2,
                        DefaultDays = 12,
                        Name = "Sick",
                        DateCreated = DateTime.UtcNow,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow
                    }
                };

                await context.LeaveTypes.AddRangeAsync(leaveTypes);
            }

            if (!context.LeaveAllocations.Any())
            {
                var allocations = new List<LeaveAllocation>
                {
                    new LeaveAllocation
                    {
                        EmployeeId = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                        LeaveTypeId = 1,
                        NumberOfDays = 10,
                        Period = DateTime.UtcNow.Year,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                    },
                    new LeaveAllocation
                    {
                        EmployeeId = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                        LeaveTypeId = 2,
                        NumberOfDays = 12,
                        Period = DateTime.UtcNow.Year,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                    }
                };

                await context.LeaveAllocations.AddRangeAsync(allocations);
            }

            if (!context.EmployeeDetails.Any())
            {
                var employeeDetails = new EmployeeDetail
                {
                    Id = 1,
                    EmployeeId = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                    FirstName = "System",
                    LastName = "User",
                    DOB = new DateTime(1998, 1, 1).ToUniversalTime(),
                    WorkEmail = "user@localhost.com",
                    PersonalEmail = "user@mail.com",
                    Address1 = "123 User Street",
                    Address2 = "Dee Way",
                    Town = "User Town",
                    PostCode = "DK99USR",
                    NINumber = "JL372998A",
                    AccountName = "S U User",
                    AccountNumber = 4789023,
                    BankName = "User Bank",
                    BankAddress = "123 Bank Road",
                    SortCode = 931238,
                    TaxCode = "T89AL",
                    MobileNumber = 07948392731,
                    WorkMobileNumber = 4898309423,
                    HomePhoneNumber = 01493902383,
                    WorkPhoneNumber = 89402939481,
                    PayPerHour = 12M,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                };

                await context.EmployeeDetails.AddAsync(employeeDetails);
            }

            await context.SaveChangesAsync();
        }
    }
}
