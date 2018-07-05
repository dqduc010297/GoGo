using Domains.GoGo.Entities;
using Domains.GoGo.Repositories.Transportation;
using Groove.AspNetCore.UnitOfWork;
using Groove.AspNetCore.UnitOfWork.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Groove.AspNetCore.DataBinding.AutoMapperExtentions;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domains.GoGo.Models.Transportation;
using Domains.GoGo;
using Domains.Core;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Domains.GoGo.Models;

namespace Infrastructures.Repositories.GoGo.Transportation
{
    public class RequestRepository : GenericRepository<Request, int>, IRequestRepository
    {
        private readonly IMapper _mapper;

        public RequestRepository(IMapper mapper, ApplicationDbContext uoWContext) : base(uoWContext)
        {
            _mapper = mapper;
        }

		public async Task<RequestDetailModel> GetRequestDetailAsync(int? id)
        {
            return await this.dbSet.Where(p => p.Id == id).MapQueryTo<RequestDetailModel>(_mapper).FirstAsync();
        }
        public async Task<RequestModel> FindCustomerRequestAsync(int id)
        {
            //return await this.dbSet.Where(p => p.Id == id).Include(p => p.WareHouse).MapQueryTo<RequestModel>(_mapper).FirstAsync();
            return await this.dbSet
                                 .Include(p => p.WareHouse)
                                 .Where(p => p.Id == id)
                                  .Select(p => new RequestModel
                                  {
                                      WareHouse = new DataSourceValue<int>()
                                      {
                                          Value = p.WareHouseId,
                                          DisplayName = p.WareHouse.NameWarehouse
                                      },
                                      Id = p.Id,
                                      ExpectedDate = p.ExpectedDate,
                                      Address = p.Address,
                                      DeliveryLatitude = p.DeliveryLatitude,
                                      DeliveryLongitude = p.DeliveryLongitude,
                                      Code = p.Code,
                                      PackageQuantity = p.PackageQuantity,
                                      ReceiverName = p.ReceiverName,
                                      ReceiverPhoneNumber = p.ReceiverPhoneNumber,
                                      CreatedDate = p.CreatedDate,
                                      PickingDate = p.PickingDate,
                                  }).SingleOrDefaultAsync();
        }


        public Task<string> ChangeStatus(int? id, string status)
        {
            throw new NotImplementedException();
        }

		public DataSourceResult GetAllAsync([DataSourceRequest] DataSourceRequest request)
		{
			return this.dbSet.MapQueryTo<RequestsModel>(_mapper).ToDataSourceResult(request);
		}


		//V
		public async Task<IEnumerable<DataSourceValue<int>>> GetDataSource(string value, int warehouseId)
		{
            // TODO: Create ShipmentStatus class for Constant instead of hard code
			// Done
            var requestedIdList = this.context.Set<ShipmentRequest>().Where(p => p.Status == ShipmentStatus.PENDING).Select(p => p.RequestId).ToList();

            // TODO: Create RequestStatus class for Constant instead of hard code
            return await this.dbSet.Where(p => (( p.Code.Contains(value) || p.Address.Contains(value)) 
									&& !requestedIdList.Contains(p.Id) && p.Status == ShipmentStatus.PENDING && p.WareHouseId == warehouseId ) )
									.Select(p => new DataSourceValue<int>
									{
										Value = p.Id,
										DisplayName = p.Code
									}).ToListAsync();
		}

        public async Task<RequestsModel> GetRequestByIdAsync(string id)
        {
            return await this.dbSet.Where(p => p.Id.ToString() == id).MapQueryTo<RequestsModel>(_mapper).FirstAsync();
        }

		public IEnumerable<RequestsModel> GetRequestsByShipmentId(int shipmentId)
		{
            // TODO: Create ShipmentRequestStatus class for Constant instead of hard code
			// Done
            var requestIdList = this.context.Set<ShipmentRequest>().Where(p =>( p.ShipmentId == shipmentId && p.Status == ShipmentStatus.PENDING)).Select(p => p.RequestId).ToList();

			return this.dbSet.Where(p => (requestIdList.IndexOf(p.Id) != -1)).MapQueryTo<RequestsModel>(_mapper).ToList();
		}

		public IEnumerable<int> GetRequestIdList(int shipmentId)
		{
            // TODO: Create ShipmentRequestStatus class for Constant instead of hard code
			// Done
            return this.context.Set<ShipmentRequest>().Where(p => (p.ShipmentId == shipmentId && p.Status == ShipmentStatus.PENDING)).Select(p => p.RequestId).ToList();
		}
        public async Task<LocationModel> GetPositionWarehouseAsync(string code)
        {
            var query = this.dbSet
                .Include(p => p.WareHouse)
                .Where(p => p.Code == code)
                .Select(p => new LocationModel
                {
                    Address = p.Address,
                    Latitude = p.WareHouse.Latitude,
                    Longitude = p.WareHouse.Longitude
                });
            return await query.FirstAsync();
        }
        public async Task<int> GetRequestID(string code)
        {
            return await this.dbSet.Where(p => p.Code == code).Select(p => p.Id).FirstAsync();
        }
    }
}
