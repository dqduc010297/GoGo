﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Services.Transportation;
using Microsoft.AspNetCore.Mvc;
using Groove.AspNetCore.Mvc;
using Domains.Helpers;
using Domains.GoGo.Services;
using System.Collections;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace GoGoApi.Controllers
{
    [Route("api/request")]
    public class RequestController : BaseController
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [Route("/api/request")]
        [HttpGet]
        public IActionResult GetRequests([DataSourceRequest]DataSourceRequest request)
        {
            var userId = GetCurrentUserId<int>();
            var result = _requestService.GetCustomerRequests(request, userId);
            return Ok(result);
        }

        [Route("/api/request/{code}/{status}")]
        [HttpGet]
        public IActionResult ChangeStatus(string code, string status)
        {
            var result = _requestService.ChangeStatus(code, status);
            return  Ok(result);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody]RequestModel model)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userId = GetCurrentUserId<int>();
            var result = await this._requestService.CreateCustomerRequest(model, userId);
            return OkValueObject(result);
        }

        [Route("{requestId}")]
        [HttpGet]
        public async Task<IActionResult> GetRequestAsync(int requestId)
        {
            var userId = GetCurrentUserId<int>();
            var result = await _requestService.FindCustomerRequestAsync(requestId,userId);
            return Ok(result);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpateRequest([FromBody]RequestModel model, int userId)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var userIdentity = GetCurrentIdentity<long>();
            var result = await this._requestService.UpdateCustomerRequest(model, userId);
            return OkValueObject(result);
        }
    }
}