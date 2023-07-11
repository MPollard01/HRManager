using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<Response<AuthResponse>> Authenticate(string email, string password);
        Task<Response<RegistrationResponse>> Register(RegisterVM registration);
        Task Logout();
    }
}
