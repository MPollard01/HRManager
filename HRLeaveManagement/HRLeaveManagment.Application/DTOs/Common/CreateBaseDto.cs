namespace HRLeaveManagment.Application.DTOs.Common
{
    public class CreateBaseDto
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
