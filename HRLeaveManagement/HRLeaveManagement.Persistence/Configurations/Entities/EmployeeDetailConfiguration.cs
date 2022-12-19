using HRLeaveManagement.Clean.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRLeaveManagement.Persistence.Configurations.Entities
{
    public class EmployeeDetailConfiguration : IEntityTypeConfiguration<EmployeeDetail>
    {
        public void Configure(EntityTypeBuilder<EmployeeDetail> builder)
        {
            builder.HasData(
                new EmployeeDetail
                {
                    Id = 1,
                    EmployeeId = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                    FirstName = "System",
                    LastName = "User",
                    DOB = new DateTime(1998,1,1),
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
                    SortCode = 093123,
                    TaxCode = "T89AL",
                    MobileNumber = 07948392731,
                    WorkMobileNumber = 4898309423,
                    HomePhoneNumber = 01493902383,
                    WorkPhoneNumber = 89402939481,
                    PayPerHour = 12M
                }
            );
        }
    }
}
