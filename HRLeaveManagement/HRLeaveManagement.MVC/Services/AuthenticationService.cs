using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HRLeaveManagement.MVC.Models;
using IAuthenticationService = HRLeaveManagement.MVC.Contracts.IAuthenticationService;

namespace HRLeaveManagement.MVC.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private JwtSecurityTokenHandler _tokenHandler;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(client, localStorage)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
            this._tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<Response<AuthResponse>> Authenticate(string email, string password)
        {
            Response<AuthResponse> response = new();

            try
            {
                AuthRequest authenticationRequest = new() { Email = email, Password = password };
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    //Get Claims from token and Build auth user object
                    var tokenContent = _tokenHandler.ReadJwtToken(authenticationResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                    var login = _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                    _localStorage.SetStorageValue("token", authenticationResponse.Token);

                    response.Success = true;
                    response.Data = authenticationResponse;
                    response.Message = "Login successful";
                }
                else
                {
                    response.Success = false;
                    response.Data = authenticationResponse;
                    response.ValidationErrors = "Login failed";
                }           
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.ValidationErrors = ex.Message.Split(":")[4].Trim('"', '}');
            }

            return response;
        }

        public async Task<Response<RegistrationResponse>> Register(RegisterVM registration)
        {
            Response<RegistrationResponse> response = new();

            try
            {
                RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registration);
                var regResponse = await _client.RegisterAsync(registrationRequest);

                if (!string.IsNullOrEmpty(regResponse.UserId))
                {
                    await Authenticate(registration.Email, registration.Password);

                    response.Data = regResponse;
                    response.Success = true;
                    response.Message = "Registration successful";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ValidationErrors = ex.Message.Split(":")[4].Trim('"','}');
            }
           
            return response;
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
