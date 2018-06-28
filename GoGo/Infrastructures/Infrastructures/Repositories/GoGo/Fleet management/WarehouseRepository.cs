﻿using AutoMapper;
using Domains.Core;
using Domains.GoGo;
using Domains.GoGo.Models;
using Domains.GoGo.Repositories;
using Groove.AspNetCore.UnitOfWork;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using System.Threading.Tasks;

namespace Infrastructures.Repositories.GoGo
{
    public class WarehouseRepository : GenericRepository<WareHouse, int>, IWarehouseRepository
	{
		private readonly IMapper _mapper;

		public WarehouseRepository(IMapper mapper, IUnitOfWorkContext uoWContext) : base(uoWContext)
		{
			_mapper = mapper;
		}

		public async Task<WarehouseModel> GetWarehouseDetailAsync(int id)
		{
			return await this.dbSet.Include(p => p.Owner)
									.Where(p => p.Owner.Id == p.OwnerId && p.Id == id)
									.MapQueryTo<WarehouseModel>(_mapper).FirstAsync();
		}

		public async Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string value)
		{

			return await this.dbSet.Where(p => ((p.Address.Contains(value)) || (p.NameWarehouse.Contains(value))))										
													.Select(p => new DataSourceValue<int>
													{
														DisplayName = $"{p.NameWarehouse}",
														Value = p.Id
													}).ToListAsync();
		}
	}
}

