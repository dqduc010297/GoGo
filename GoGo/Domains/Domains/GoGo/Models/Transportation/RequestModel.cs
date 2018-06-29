﻿using AutoMapper;
using Domains.Core;
using Domains.GoGo.Entities;
using Domains.Identity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.GoGo.Models.Transportation
{
    public class WaitingRequestModel
    {
        public int Id { get; set; }

        //public DateTime CreatedDate { get; set; }
        public DateTime PickingDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude { get; set; }

        public int PackageQuantity { get; set; }

        public string Code { set; get; }
        public string Status { get; set; }
    }

    public class WaitingRequestModelMaper : Profile
    {
        public WaitingRequestModelMaper()
        {
            CreateMap<WaitingRequestModel, Request>();
        }
    }
    public class RequestModel
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        //Customer add 
        public DateTime PickingDate { get; set; }
        public DateTime ExpectedDate { set; get; }
        //public string WarehouseName { set; get; }


        public double DeliveryLatitude { get; set; }
        public double DeliveryLongitude { get; set; }
        public string Address { set; get; }

        public int PackageQuantity { get; set; }

        public string Code { set; get; }
        public string Status { get; set; }

        public string ReceiverName { set; get; }
        public string ReceiverPhoneNumber { set; get; }

        public long IssuerId { get; set; }
        //public int WareHouseId { get; set; }
        public DataSourceValue<int> WareHouse { get; set; }
        public long CustomerId { set; get; }

    }

    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<RequestModel, Request>()
                .ForPath(destination => destination.WareHouseId, option => option.MapFrom(source => source.WareHouse.Value));
            CreateMap<Request, RequestModel>()
                .ForPath(destination => destination.WareHouse.Value, option => option.MapFrom(source => source.WareHouseId));
        }
    }

    public class RequestModelValidator : AbstractValidator<RequestModel>
    {
        public RequestModelValidator()
        {
            RuleFor(p => p.ExpectedDate).NotEmpty();
            RuleFor(p => p.PickingDate).NotEmpty();
            RuleFor(p => p.DeliveryLatitude).NotEmpty();
            RuleFor(p => p.DeliveryLongitude).NotEmpty();
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.PackageQuantity).NotEmpty().IsInEnum();
            RuleFor(p => p.Code).NotEmpty();
            RuleFor(p => p.Status).NotEmpty();
            RuleFor(p => p.ReceiverName).NotEmpty();
            RuleFor(p => p.ReceiverPhoneNumber).NotEmpty();
            RuleFor(p => p.IssuerId).NotEmpty();

        }
    }

    public class SummaryRequestModel
    {
        public string Code { set; get; }
        public string Status { get; set; }
        public DateTime PickingDate { get; set; }
        public DateTime ExpectedDate { set; get; }
        public string WareHouse { get; set; }
        public string Address { set; get; }
    }
}
