﻿using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;

namespace HRLeaveManagement.MVC.Services
{
    public class TemplateTimeService : BaseHttpService, ITemplateTimeService
    {
        private readonly IMapper _mapper;

        public TemplateTimeService(IClient client, 
            ILocalStorageService localStorageService,
            IMapper mapper) 
            : base(client, localStorageService)
        {
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateTemplate(CreateTemplateTimeVM template)
        {
            try
            {
                var response = new Response<int>();
                var createTemplate = _mapper.Map<CreateTemplateTimeDto>(template);
                AddBearerToken();
                var apiResponse = await _client.TemplateTimePOSTAsync(createTemplate);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    response.ValidationErrors =
                        string.Join(Environment.NewLine, apiResponse.Errors);
                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<TemplateTimeVM> GetTemplateTime()
        {
            AddBearerToken();
            var template = await _client.TemplateTimeGETAsync();
            return _mapper.Map<TemplateTimeVM>(template);
        }

        public async Task<Response<int>> UpdateTemplate(TemplateTimeVM template)
        {
            try
            {
                var templateTime = _mapper.Map<TemplateTimeDto>(template);
                AddBearerToken();
                await _client.TemplateTimePUTAsync(templateTime);
                return new Response<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
