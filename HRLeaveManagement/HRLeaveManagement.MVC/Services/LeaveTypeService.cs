﻿using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IMapper _mapper;
        public LeaveTypeService(
            IClient client, 
            ILocalStorageService localStorageService, 
            IMapper mapper) 
            : base(client, localStorageService)
        {
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var response = new Response<int>();
                var createLeaveType = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                AddBearerToken();
                var apiResponse = await _client.LeaveTypesPOSTAsync(createLeaveType);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                    response.Message = apiResponse.Message;
                }
                else
                {
                    response.ValidationErrors = 
                        string.Join(Environment.NewLine, apiResponse.Errors);

                    //foreach(var error in apiResponse.Errors)
                    //{
                    //    response.ValidationErrors += error + Environment.NewLine;
                    //}
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                AddBearerToken();
                await _client.LeaveRequestDELETEAsync(id);
                return new Response<int> { Success = true };
            }
            catch (ApiException ex)
            {

                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            AddBearerToken();
            var leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            AddBearerToken();
            var leaveType = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveType);
        }

        public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);
                AddBearerToken();
                await _client.LeaveTypesPUTAsync(leaveTypeDto);
                return new Response<int> { Success = true, Message = "Update Successfull" };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
