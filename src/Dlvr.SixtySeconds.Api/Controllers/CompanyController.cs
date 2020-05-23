using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dlvr.SixtySeconds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public DataContext _context;
        public CompanyController(DataContext dataContext)
        {
            _context = dataContext;
        }


        [HttpPost]
        public IActionResult Post(Company model)
        {
            try
            {
                return Ok(Save(0, model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Put(int id, Company model)
        {
            try
            {
                return Ok(Save(id, model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public ResponseModel Delete(int id)
        {
            try
            {
                CompanyProvider companyProvider = new CompanyProvider(_context);
                companyProvider.Delete(id);

                ResponseModel objResponseModel = new ResponseModel
                {
                    Message = "Successfully Deleted Company",
                    StatusCode = 1
                };
                return objResponseModel;

            }
            catch (Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }
        }

        [HttpGet]
        public ResponseModel<List<Company>> GetAll([FromQuery]Pagination request)
        {
            try
            {
                CompanyProvider companyProvider = new CompanyProvider(_context);
                var lst = companyProvider.GetAll(request);
                ResponseModel<List<Company>> objResponseModel = new ResponseModel<List<Company>>
                {
                    Message = "Successfully Retrieved",
                    StatusCode = 1,
                    ResponseObject = lst
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<List<Company>> objResponseModel = new ResponseModel<List<Company>>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }
        }

        [Route("{id}")]
        [HttpGet]
        public ResponseModel<Company> Get(int id)
        {
            try
            {
                CompanyProvider companyProvider = new CompanyProvider(_context);
                var company = companyProvider.Get(id);
                ResponseModel<Company> objResponseModel = new ResponseModel<Company>
                {
                    Message = "Successfully Retrieved",
                    StatusCode = 1,
                    ResponseObject = company
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Company> objResponseModel = new ResponseModel<Company>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }
        }

        private ResponseModel<Company> Save(int id, Company model)
        {
            //check duplicate for subdomain
            if (model.ScriptFieldCollection?.Count > 0)
            {
                bool hasDuplicate = _context.Companies.Any(t => t.DeletedOn == null && t.SubDomain == model.SubDomain && t.CompanyId != model.CompanyId);

                if (!hasDuplicate)
                {
                    CompanyProvider companyProvider = new CompanyProvider(_context);
                    companyProvider.Save(model);

                    return new ResponseModel<Company>
                    {
                        Message = id > 0 ? "Successfully updated Company" : "Successfully added Company",
                        StatusCode = 1,
                        ResponseObject = model
                    };
                }
                else
                {
                    return new ResponseModel<Company>
                    {
                        Message = "Subdomain already exists",
                        StatusCode = 2
                    };
                }
            }
            else
            {
                return new ResponseModel<Company>
                {
                    Message = "Minimum 1 script field is required",
                    StatusCode = 2
                };
            }
        }
    }
}