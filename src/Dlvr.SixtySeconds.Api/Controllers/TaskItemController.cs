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
    public class TaskItemController : ControllerBase
    {
        public IConfiguration Configuration;
        public DataContext _context;
        public TaskItemController(IConfiguration configuration,DataContext dataContext )
        {
            Configuration = configuration;
            _context = dataContext;
        }
        
        [HttpPost]
        [Route("/api/task/addtask")]
        public ResponseModel<TaskItem> AddTask(TaskItem objTask)
        {
            try
            {
                TaskItemProvider objTaskItemProvider = new TaskItemProvider(_context);
                TaskItem addedTask = objTaskItemProvider.AddTask(objTask);
                ResponseModel<TaskItem> objResponseModel = new ResponseModel<TaskItem>
                {
                    Message = "Successfully Added",
                    StatusCode = 1,
                    ResponseObject = addedTask
                };
                return objResponseModel;

            }
            catch (Exception ex)
            {
                ResponseModel<TaskItem> objResponseModel = new ResponseModel<TaskItem>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }

        }

        [HttpPost]
        [Route("/api/task/addassignees")]
        public ResponseModel AddAssignees (ListRequestModel<AssignedTask> objAssignTaskRequest)
        {
            try
            {

                TaskItemProvider objTaskItemProvider = new TaskItemProvider(_context);
                objTaskItemProvider.AssignTask(objAssignTaskRequest.ObjectList);
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 0,
                    Message = "Assigned Successfully"
                };
                return objResponseModel;
            }
            catch(Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 0,
                    Message = ex.Message
                };
                return objResponseModel;
            }
        }

        [HttpPost]
        [Route("/api/task/updatetask")]
        public ResponseModel UpdateTask(TaskItem objTaskItem)
        {
            try
            {
                TaskItemProvider objTaskItemProvider = new TaskItemProvider(_context);
                objTaskItemProvider.UpdateTask(objTaskItem);

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
        [Route("/api/task/deletetask")]
        public ResponseModel DeleteTask(TaskItem objTask)
        {
            try
            {
                TaskItemProvider objTaskItemProvider = new TaskItemProvider(_context);
                objTaskItemProvider.DeleteTask(objTask);
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
        [Route("/api/task/getalltasks")]
        public ResponseModelGetAll<TaskResponse> GetAllTasks(GetById  getById)
        {
            try
            {
                TaskItemProvider objtaskItemProvider = new TaskItemProvider(_context);
                List<TaskResponse> allTasks = objtaskItemProvider.GetAllTask(getById.Id);
                ResponseModelGetAll<TaskResponse> objResponseModelGetAll = new ResponseModelGetAll<TaskResponse>
                {
                    ObjectList = allTasks,
                    Message = "Successfully Retrieved",
                    StatusCode = 1
                };

                return objResponseModelGetAll;

            }
            catch (Exception ex)
            {
                ResponseModelGetAll<TaskResponse> objResponseModelGetAll = new ResponseModelGetAll<TaskResponse>
                {
                   
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModelGetAll;
            }
        }

        [HttpPost]
        [Route("/api/task/gettask")]
        public ResponseModel<TaskWithActivity> GetTaskById(GetById objGetById)
        {
            try
            {
                TaskItemProvider objTaskItemProvider = new TaskItemProvider(_context);
                TaskWithActivity objTask = objTaskItemProvider.GetTaskWithActivity(objGetById.Id);
                ResponseModel<TaskWithActivity> objresponseModel = new ResponseModel<TaskWithActivity>
                {
                  Message = "Successfully retrieved",
                  StatusCode = 1,
                  ResponseObject = objTask
                };
                return objresponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<TaskWithActivity> objresponseModel = new ResponseModel<TaskWithActivity>
                {
                    Message = ex.Message,
                    StatusCode = 0,
                };
                return objresponseModel;
            }
        }

     

        [HttpPost]
        [Route("/api/task/approvalrequest")]
        public ResponseModel ApprovalRequest (ApprovalRequest objApprovalRequest)
        {
           try
            {
                switch (objApprovalRequest.Type)
                {
                    case 0:
                        ScenarioProvider objScenarioProvider = new ScenarioProvider(_context);
                        objScenarioProvider.ApproveScenario(objApprovalRequest);
                        break;
                    case 1:
                        ScriptProvider objScriptProvider = new ScriptProvider(_context);
                        objScriptProvider.ApproveScript(objApprovalRequest);
                        break;
                    case 2:
                        DeliveryProvider objDeliveryProvider = new DeliveryProvider(_context);
                        objDeliveryProvider.ApproveDelivery(objApprovalRequest);
                        break;
                }
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 1,
                    Message = "Action Performed Successfully"
                };
            return objResponseModel;
            }
            catch  (Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 0,
                    Message = ex.Message
                };
                return objResponseModel;
            }
        }
     
    }
}