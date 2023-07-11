namespace HRLeaveManagment.Application.DTOs.Common
{
    public class UpdateBaseDto
    {
        public int Id { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
