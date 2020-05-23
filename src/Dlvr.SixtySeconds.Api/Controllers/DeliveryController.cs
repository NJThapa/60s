using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dlvr.SixtySeconds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryController : ControllerBase
    {
        public DataContext _context { get; set; }
        public DeliveryController(DataContext datacontext)
        {
            _context = datacontext;
        }


        [Route("/api/delivery/adddelivery")]
        [HttpPost]
        public ResponseModel<Delivery> AddDelivery(Delivery objDeliveryDetails)
        {
            try
            {
                DeliveryProvider objDeliveryProvider = new DeliveryProvider(_context);
                Delivery objAddedDelivery =  objDeliveryProvider.AddDelivery(objDeliveryDetails);

                ResponseModel<Delivery> objResponseModel = new ResponseModel<Delivery>
                {
                    StatusCode = 1,
                    Message = "Successfully Added",
                    ResponseObject = objAddedDelivery
                };
                return objResponseModel;
            }
            catch(Exception ex)
            {
                ResponseModel<Delivery> objResponseModel = new ResponseModel<Delivery>
                {
                    StatusCode = 0,
                    Message = ex.Message,
                };
                return objResponseModel;
            }
        }


        [Route("/api/delivery/update")]
        [HttpPost]
        public ResponseModel<Delivery> UpdateDelivery(Delivery objDeliveryDetails)
        {
            try
            {
                DeliveryProvider objDeliveryProvider = new DeliveryProvider(_context);
                Delivery updatedDelivery = objDeliveryProvider.UpdateDelivery(objDeliveryDetails);

                ResponseModel<Delivery> objResponseModel = new ResponseModel<Delivery>
                {
                    StatusCode = 1,
                    Message = "Successfully Updated",
                    ResponseObject = updatedDelivery
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Delivery> objResponseModel = new ResponseModel<Delivery>
                {
                    StatusCode = 0,
                    Message = ex.Message,
                };
                return objResponseModel;
            }
        }
       
        
        [Route("/api/delivery/deletedelivery")]
        [HttpPost]
        public ResponseModel DeleteDelivery(Delivery objDeliveryDetails)
        {
            try
            {
                DeliveryProvider objDeliveryProvider = new DeliveryProvider(_context);
                 objDeliveryProvider.DeleteDelivery(objDeliveryDetails);

                ResponseModel objResponseModel = new ResponseModel
                {
                    Message = "Successfully Deleted",
                    StatusCode = 1
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    Message = ex.Message,
                    StatusCode = 1
                };
                return objResponseModel;
            }
        }


        [Route("/api/delivery/getdelivery")]
        [HttpPost]
        public ResponseModel<Delivery> GetDelivery(GetById objGetById)
        {
            try
            {
                DeliveryProvider objDeliveryProvider = new DeliveryProvider(_context);
               Delivery objDelivery =  objDeliveryProvider.GetDeliveryById(objGetById.Id);
                ResponseModel<Delivery> objResponseModel = new ResponseModel<Delivery>
                {
                    StatusCode = 0,
                    Message = "Successful",
                    ResponseObject = objDelivery
                };
                return objResponseModel;
            }
            catch(Exception ex)
            {
                ResponseModel<Delivery> objResponseModel = new ResponseModel<Delivery>
                {
                    StatusCode = 0,
                    Message = ex.Message
                };
                return objResponseModel;
            }
        }
    }
}