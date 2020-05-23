using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Microsoft.Extensions.Configuration;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Provider;
using Dlvr.SixtySeconds.DataAccessLayer.Library;
using Auth0.ManagementApi.Paging;
using static Dlvr.SixtySeconds.DataAccessLayer.Library.Common.AppEnum;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;

namespace Dlvr.SixtySeconds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration Configuration;
        public Auth0ConfigModel objAuth0Config;
        public DataContext _context { get; set; }

        public UserController(IConfiguration configuration, DataContext dataContext)
        {
            _context = dataContext; 
            Configuration = configuration;
            objAuth0Config = new Auth0ConfigModel
            {
                Connection = Configuration["Auth0:Connection"],
                Domain = Configuration["Auth0:Domain"],
                ClientId = Configuration["Auth0:ClientId"],
                ClientSecret = Configuration["Auth0:ClientSecret"],
                ManagementApiDomain = Configuration["Auth0:ManagementApiDomain"]
            };
        }

        [Route("/api/user/adduser")]
        [HttpPost]
    
        public async Task <ResponseModel<UserModel>> AddUser(UserModel  objUserModel)
        {
            try
            {
                bool ifRolexist;
                ifRolexist = Enum.IsDefined(typeof(Roles), objUserModel.Role);
                if (ifRolexist == true)
                {
                    UserProvider objUserProvider = new UserProvider();
                    UserModel newUser = await objUserProvider.AddUser(objUserModel, objAuth0Config);
                    return new ResponseModel<UserModel>
                    {
                        ResponseObject = newUser,
                        Message = "Successfully Added User",
                        StatusCode = 1
                    };
                }
                else
                {
                    return new ResponseModel<UserModel>
                    {
                        Message = "Provided Role Does not exist, please verify it",
                        StatusCode = 0

                    };
                }
             
            }
            catch (Exception ex)
            {

                return new ResponseModel<UserModel>
                {
                    Message = ex.Message,
                    StatusCode = 0 
                };
            }
        }

        [Route("/api/user/updateuser")]
        [HttpPost]
        public async Task<ResponseModel<UserModel>> UpdateUser(UpdateUserRequest objUpdateUserRequest)
        {
            try
            {
                UserProvider objUserProvider = new UserProvider();
                UserModel newUser = await objUserProvider.UpdateUser(objUpdateUserRequest, objAuth0Config);
                return new ResponseModel<UserModel>
                {
                    ResponseObject = newUser,
                    Message = "Successfully Updated User",
                    StatusCode = 1
                };
            }
            catch (Exception ex)
            {

                return new ResponseModel<UserModel>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
            }
        }

        [Route("/api/user/getalluser")]
        [HttpPost]
        public async Task<ResponseModelAuth0> GetAllUser(Pagination objPagination)
        { 
            try
            {
                UserProvider objUserProvider = new UserProvider();
                PaginationInfo paginationInfo = new PaginationInfo(objPagination.PageNo, objPagination.PerPage, objPagination.IncludeTotals);
                List<UserModel> objListUser = await objUserProvider.GetUsers(objAuth0Config, paginationInfo);
               
                ResponseModelAuth0 objResponseModelAuth0 = new ResponseModelAuth0 { 
                    Message = "Successfully Retrieved" ,
                    Userlist = objListUser ,
                    StatusCode = 1
                };
                return objResponseModelAuth0;
            }
            catch (Exception ex)
            {

                ResponseModelAuth0 objResponseModelAuth0 = new ResponseModelAuth0
                {
                    StatusCode = 1,
                    Message = ex.Message
                };
                return objResponseModelAuth0;
            }
        }

        [Route("/api/user/getuser")]
        [HttpPost]
        public async Task<ResponseModel<UserModel>> GetUser(GetByIdAuth0 objGetById)
        {
            try
            {
                UserProvider objUserProvider = new UserProvider();
                UserModel objUser = await objUserProvider.GetUserById(objGetById.ID, objAuth0Config);
                ResponseModel<UserModel> objResponseModel = new ResponseModel<UserModel>
                {
                    StatusCode = 1,
                    Message = "Successfully Retrieved user",
                    ResponseObject = objUser
                };
                return objResponseModel;
                    
            }
            catch(Exception ex)
            {
                ResponseModel<UserModel> objResponseModel = new ResponseModel<UserModel>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }

        }
        [Route("/api/user/assignusertoroles")]
        [HttpPost]
        public async Task<ResponseModel> AssignUserToRoles(UserRole objUserRole)
        {
            try
            {
                UserProvider objUserProvider = new UserProvider();
               await  objUserProvider.AddUserToRoles(objUserRole, objAuth0Config);
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 1,
                    Message = "Successfully Assigned Role"
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

        [Route("/api/user/removeuserfromroles")]
        [HttpPost]
        public async Task<ResponseModel> RemoveUserFromRoles(UserRole objUserRole)
        {
            try
            {
                UserProvider objUserProvider = new UserProvider();
                await objUserProvider.RemoveUserFromRoles(objUserRole, objAuth0Config);
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 1,
                    Message = "Successfully Removed From Role"
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 0,
                    Message = ex.Message
                };
                return objResponseModel;
            }

        }
        
        
        [Route("/api/user/getrecipients")]
        [HttpPost]
        public async Task<ResponseModelGetAll<UserModel>> GetRecipients(GetRecipientRequest objGetRecipient)
        {
            try
            {
                UserProvider objuserprovider = new UserProvider();
                List<UserModel> listOfRecipients = new List<UserModel>();
                listOfRecipients = await objuserprovider.GetUserRecipients(objAuth0Config, objGetRecipient);
                ResponseModelGetAll<UserModel> objResponseModel = new ResponseModelGetAll<UserModel>
                {
                    Message = "Successfully Retrieved",
                    StatusCode = 1,
                    ObjectList = listOfRecipients
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModelGetAll<UserModel> objResponseModel = new ResponseModelGetAll<UserModel>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }
        }


        [Route("/api/user/submitRecipients")]
        [HttpPost]
        public ResponseModel SubmitRecipients(AddEmailNotificationRequest ObjEmailNotificationsRequest)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);
                objActivityProvider.AddEmailNotification(ObjEmailNotificationsRequest.ListEmailNotificationDetails);
                ResponseModel objResponseModel = new ResponseModel
                {
                    Message = "Successfully Submitted for Email",
                    StatusCode = 1
                };
                return objResponseModel;
            }
            catch(Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    Message = ex.Message ,
                    StatusCode = 0
                };
                return objResponseModel;
            }
        }

        [Route("/api/user/sendemail")]
        [HttpPost]
        public ResponseModel Sendemail (EmailNotification objEmailNotification)
        {
            try
            {
                MethodCollection objMethodCollection = new MethodCollection();
                objMethodCollection.SendEmail(objEmailNotification);
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 0,
                    Message = "Done"
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


        [Route("savedevicetoken")]
        [HttpPost]
        public ResponseModel SaveDeviceToken(UserDeviceToken request)
        {
            try
            {
                UserDeviceTokenProvider objActivityProvider = new UserDeviceTokenProvider(_context);
                objActivityProvider.SaveUserDeviceToken(request);
                ResponseModel objResponseModel = new ResponseModel
                {
                    Message = "Successfully Saved",
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
        [Route("/api/user/removeuser")]
        public async Task<ResponseModel> RemoveUser(GetByIdAuth0 objGetbyId)
        {
            try
            {
                UserProvider objUserProvider = new UserProvider();
               await objUserProvider.RemoveUser(objAuth0Config, objGetbyId.ID);
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 0,
                    Message = "SuccessFully Deleted"
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
    }
}