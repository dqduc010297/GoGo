﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.GoGo.Services;
using Groove.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace GoGoApi.Controllers
{
    
    [Route("api/warehouse")]
    public class WarehouseController : BaseController
    {
        private readonly IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // TODO: Remove {displayName} from route. {} synctax to use to identify an resource,
        // In this case DisplayName is a query srring only, we should not pass it to the url
        [Route("filter-list/{displayName}")]
        [HttpGet]
        public async Task<IActionResult> GetOnFilter(string displayName)
        {
            var result = await _warehouseService.GetOnFilter(displayName);
            return Ok(result);
        }
    }
}