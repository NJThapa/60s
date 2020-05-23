using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Dlvr.SixtySeconds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScenarioController : ControllerBase
    {
        public IConfiguration Configuration;
        public DataContext _context;
        public ScenarioController(IConfiguration configuration, DataContext DbContext)
        {
            Configuration = configuration;
            _context = DbContext;
        }

        [HttpPost]
        [Route("/api/scenario/addScenario")]
        public ResponseModel<Scenario> AddScenario(Scenario objScenario)
        {
            try
            {
                ScenarioProvider objScenarioProvider = new ScenarioProvider(_context);
                Scenario objAddedScenario = objScenarioProvider.AddScenario(objScenario);

                ResponseModel<Scenario> objResponseModel = new ResponseModel<Scenario>
                {
                    Message = "Successfully Added",
                    StatusCode = 1,
                    ResponseObject = objAddedScenario
                };
                return objResponseModel;

            }
            catch (Exception ex)
            {
                ResponseModel<Scenario> objResponseModel = new ResponseModel<Scenario>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }

        }

        [HttpPost]
        [Route("/api/scenario/updatescenario")]
        public ResponseModel UpdateScenario(Scenario objScenario)
        {
            try
            {
                ScenarioProvider objScenarioProvider = new ScenarioProvider(_context);
                objScenarioProvider.UpdateScenario(objScenario);
                ResponseModel objResponseModel = new ResponseModel
                {
                    Message = "Successfully Updated",
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

        [HttpPost]
        [Route("/api/scenario/deletescenario")]
        public ResponseModel DeleteScenario(Scenario objScenario)
        {
            try
            {
                ScenarioProvider objScenarioProvider = new ScenarioProvider(_context);
                objScenarioProvider.DeleteScenario(objScenario);
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
                    StatusCode = 0
                };
                return objResponseModel;
            }

        }
        [HttpPost]
        [Route("/api/scenario/getscenario")]
        public ResponseModel<Scenario> GetScenario(GetById objGetById)
        {
            try
            {
                ScenarioProvider objScenarioProvider = new ScenarioProvider(_context);
                Scenario objScenario = objScenarioProvider.GetScenarioById(objGetById.Id);
                ResponseModel<Scenario> objResponseModel = new ResponseModel<Scenario>
                {
                    StatusCode = 1,
                    Message = "Successfully retrieved",
                    ResponseObject = objScenario
                };
                return objResponseModel;

            }
            catch (Exception ex)
            {
                ResponseModel<Scenario> objResponseModel = new ResponseModel<Scenario>
                {
                    StatusCode = 0,
                    Message = ex.Message
                };

                return objResponseModel;
            }
        }

    }
}