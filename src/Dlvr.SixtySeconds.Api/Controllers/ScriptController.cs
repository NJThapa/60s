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
    public class ScriptController : ControllerBase
    {
        public IConfiguration Configuration;
        public DataContext _context;
        public ScriptController(IConfiguration configuration, DataContext dataContext)
        {
            Configuration = configuration;
            _context = dataContext;
        }

        [Route("/api/script/addscript")]
        [HttpPost]
         public ResponseModel<Script> AddScript(Script objScript)
        {
            try
            {
                ScriptProvider objScriptProvider = new ScriptProvider(_context);
                Script addedScript = objScriptProvider.AddScript(objScript);
                //List<ScriptContent> objScriptContents = objScriptProvider.AddScriptContent(objAddScript.ScriptContents, addedScript.ScriptId);
              
                //ScriptResponse scriptResponse = new ScriptResponse
                //{
                //    Script = addedScript,
                //    ScriptContents = objScriptContents,
                //};
                ResponseModel<Script> objResponseModel = new ResponseModel<Script>
                {
                    Message = "Successfully Added",
                    StatusCode = 1,
                    ResponseObject = addedScript
                };
                return objResponseModel;

            }
            catch (Exception ex)
            {
                ResponseModel<Script> objResponseModel = new ResponseModel<Script>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }

        }

        [Route("/api/script/updatescript")]
        [HttpPost]
        public ResponseModel<ScriptResponse> UpdateScript(AddScriptRequest objupdateRequest)
        {
            try
            {
                ScriptProvider objScriptProvider = new ScriptProvider(_context);
                Script updatedScript = objScriptProvider.UpdateScript(objupdateRequest.ScriptDetails);
                List<ScriptContent> scriptContents = objScriptProvider.UpdateScriptContents(objupdateRequest.ScriptContents, objupdateRequest.ScriptDetails.ScriptId);
                ScriptResponse objScriptResponse = new ScriptResponse
                {
                    Script = updatedScript,
                    ScriptContents = scriptContents
                };
                
                    
                ResponseModel<ScriptResponse> objResponseModel = new ResponseModel<ScriptResponse>
                {
                    Message = "Successfully Updated",
                    StatusCode = 1,
                    ResponseObject = objScriptResponse
                };
                return objResponseModel;

            }
            catch (Exception ex)
            {
                ResponseModel<ScriptResponse> objResponseModel = new ResponseModel<ScriptResponse>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }

        }

        [Route("/api/script/deletescript")]
        [HttpPost]
        public ResponseModel DeleteScript(Script objScript)
        {
            try
            {
                ScriptProvider objScriptProvider = new ScriptProvider(_context);
                objScriptProvider.DeleteScript(objScript);
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

        [Route("/api/script/getscript")]
        [HttpPost]
        public ResponseModel<Script> GetScript(GetById objGetById)
        {
            try
            {
                ScriptProvider objScriptProvider = new ScriptProvider(_context);
                Script objscript = objScriptProvider.GetScriptById(objGetById.Id);
             
                
                ResponseModel<Script> objResponseModel = new ResponseModel<Script>
                {
                    Message = "Successfully Retrieved",
                    StatusCode = 1,
                    ResponseObject = objscript
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Script> objResponseModel = new ResponseModel<Script>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }
        }

        [HttpGet]
        [Route("/api/script/getscriptReview")]
        public ResponseModel<ScriptReview> GetScriptReview(GetById objGetById)
        {
            try
            {
                ScriptProvider objScriptProvider = new ScriptProvider(_context);
               ScriptReview objScriptReview =  objScriptProvider.GetScriptReview(objGetById.Id);
                ResponseModel<ScriptReview> objResponseModel = new ResponseModel<ScriptReview>
                {
                    StatusCode = 1,
                    Message = "Success",
                    ResponseObject = objScriptReview,
                };
                return objResponseModel;
            }
            catch(Exception ex)
            {
                ResponseModel<ScriptReview> objResponseModel = new ResponseModel<ScriptReview>
                {
                    StatusCode = 0,
                    Message = ex.Message,
                };
                return objResponseModel;
            }
        }
    }
}