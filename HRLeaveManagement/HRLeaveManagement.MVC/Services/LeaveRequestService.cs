﻿using AutoMapper;
using HRLeaveManagement.Clean.Domain;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Helpers;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly IMapper _mapper;

        public LeaveRequestService(
            IClient client,
            ILocalStorageService localStorageService,
            IMapper mapper)
            : base(client, localStorageService)
        {
            _mapper = mapper;
        }

        public async Task ApproveLeaveRequest(int id, bool approved)
        {
            AddBearerToken();
            try
            {
                var request = new ChangeLeaveRequestApprovalDto { Approved = approved, Id = id };
                await _client.ChangeapprovalAsync(id, request);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveRequestDto createLeaveRequest = _mapper.Map<CreateLeaveRequestDto>(leaveRequest);
                AddBearerToken();
                var apiResponse = await _client.LeaveRequestPOSTAsync(createLeaveRequest);
                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public Task DeleteLeaveRequest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList(string userId, int? pageNumber)
        {
            AddBearerToken();
            var leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: false);

            if (!string.IsNullOrEmpty(userId))
            {
                leaveRequests = leaveRequests.Where(r => r.RequestingEmployeeId == userId).ToList();
            }

            var requestsVM = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);

            var model = new AdminLeaveRequestViewVM
            {
                TotalRequests = leaveRequests.Count,
                ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
                PendingRequests = leaveRequests.Count(q => q.Approved == null),
                RejectedRequests = leaveRequests.Count(q => q.Approved == false),
                LeaveRequests = PaginatedList<LeaveRequestVM>.Create(requestsVM, pageNumber ?? 1, 10)
            };
            return model;
        }

        public async Task<LeaveRequestVM> GetLeaveRequest(int id)
        {
            AddBearerToken();
            var leaveRequest = await _client.LeaveRequestGETAsync(id);
            return _mapper.Map<LeaveRequestVM>(leaveRequest);
        }

        public async Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests(string searchString, string sortOrder, int? pageNumber)
        {
            AddBearerToken();
            var leaveRequests = await _client.LeaveRequestAllAsync(isLoggedInUser: true);

            if (!string.IsNullOrEmpty(searchString))
            {
                leaveRequests = leaveRequests
                    .Where(e => e.LeaveType.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            switch (sortOrder)
            {
                case "Date":
                    leaveRequests = leaveRequests.OrderBy(e => e.StartDate).ToList();
                    break;
                case "date_desc":
                    leaveRequests = leaveRequests.OrderByDescending(e => e.StartDate).ToList();
                    break;
                case "Type":
                    leaveRequests = leaveRequests.OrderBy(e => e.LeaveType.Name).ToList();
                    break;
                case "type_desc":
                    leaveRequests = leaveRequests.OrderByDescending(e => e.LeaveType.Name).ToList();
                    break;
                case "Requested":
                    leaveRequests = leaveRequests.OrderBy(e => e.DateRequested).ToList();
                    break;
                case "requested_desc":
                    leaveRequests = leaveRequests.OrderByDescending(e => e.DateRequested).ToList();
                    break;
                default:
                    break;
            }

            var leaveRequestsVM = _mapper.Map<List<LeaveRequestVM>>(leaveRequests);

            var allocations = await _client.LeaveAllocationAllAsync(isLoggedInUser: true);
            var model = new EmployeeLeaveRequestViewVM
            {
                LeaveAllocations = _mapper.Map<List<LeaveAllocationVM>>(allocations),
                LeaveRequests = PaginatedList<LeaveRequestVM>.Create(leaveRequestsVM, pageNumber ?? 1, 10)
            };

            return model;
        }

    }
}
