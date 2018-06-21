﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo.Services;
using Domains.GoGo.Services.Transportation;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers.GoGo
{
    [Route("api/Driver")]
    public class DriverController : Controller
    {
        private IRequestService _serviceRequest;
        private IShipmentService _serviceShipment;
        public DriverController(IRequestService serviceRequest, IShipmentService serviceShipment)
        {
            _serviceRequest = serviceRequest;
            _serviceShipment = serviceShipment;
        }
        public class parameter
        {
            public string code { set; get; }
            public string status { set; get; }
        }
        [Route("detail")]
        [HttpGet]
        public async Task<IActionResult> GetRequestDetail(int? id)
        {
            return Ok(await _serviceRequest.GetRequestDetails(id));
        }
        [Route("changeStatus")]
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int? id, string status)
        {
            return Ok(await _serviceRequest.ChangeStatus(id, status));
        }
        [Route("shipmentAssigned")]
        [HttpGet]
        public async Task<IActionResult> GetShipmentAssigned(long? id)
        {
            return Ok(await _serviceShipment.GetShipmentAssignedModel(id));
        }
        [Route("shipmentfeedback")]
        [HttpPost]
        public async Task<IActionResult> AcceptOrReject([FromBody]parameter p)
        {
            return Ok(await _serviceShipment.ChangeStatus(p.code,p.status));
        }
    }
}