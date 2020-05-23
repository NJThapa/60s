using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Newtonsoft.Json.Linq;
using static Dlvr.SixtySeconds.DataAccessLayer.Library.Common.AppEnum;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
    public class UserProvider
    {

        public async void SignUpUser(UserModel objuserModel, Auth0ConfigModel objAuth0Config)
        {
            var authenticationApi = new AuthenticationApiClient(objAuth0Config.Domain);
            SignupUserRequest signupUserRequest = new SignupUserRequest
            {
                ClientId = objAuth0Config.ClientId,
                Connection = objAuth0Config.Connection,
                Email = objuserModel.Email,
                Name = objuserModel.FirstName,
                Password = objuserModel.Password,
            };
            await authenticationApi.SignupUserAsync(signupUserRequest);
        }

        public async Task<UserModel> AddUser(UserModel objUserModel, Auth0ConfigModel objAuth0Config)
        {

            MethodCollection methodCollection = new MethodCollection();
            string token = await methodCollection.GetAccessToken(objAuth0Config);
            var managementApiClient = new ManagementApiClient(token, objAuth0Config.Domain);

            UserMetadata userMetaData = new UserMetadata();
            userMetaData.ReportsTo = objUserModel.ReportsTo;
            userMetaData.Company = objUserModel.CompanyName;

            UserCreateRequest createRequest = new UserCreateRequest
            {
                Connection = objAuth0Config.Connection,
                FirstName = objUserModel.FirstName,
                LastName = objUserModel.LastName,
                Email = objUserModel.Email,
                Password = objUserModel.Password,
                PhoneNumber = objUserModel.PhoneNumber,
                UserMetadata = userMetaData
            };

            User addedUser = await managementApiClient.Users.CreateAsync(createRequest);
            if (objUserModel.Role.Length > 0)
            {
                List<string> roles = new List<string>
                {
                    objUserModel.Role
                };

                UserRole objUserRole = new UserRole
                {
                    UserId = addedUser.UserId,
                    RoleNames = roles
                };
                await this.AddUserToRoles(objUserRole, objAuth0Config);
            }


            UserModel newUser = new UserModel
            {
                Email = addedUser.Email,
                FirstName = addedUser.FirstName,
                LastName = addedUser.LastName,
                PhoneNumber = addedUser.PhoneNumber,
            };
            return newUser;
        }

        public async Task<UserModel> UpdateUser(UpdateUserRequest objUpdateUserRequest, Auth0ConfigModel objAuth0Config)
        {
            MethodCollection methodCollection = new MethodCollection();
            string token = await methodCollection.GetAccessToken(objAuth0Config);
            var managementApiClient = new ManagementApiClient(token, objAuth0Config.Domain);

            UserMetadata userMetaData = new UserMetadata();
            userMetaData.ReportsTo = objUpdateUserRequest.UserDetails.ReportsTo;
            userMetaData.Company = objUpdateUserRequest.UserDetails.CompanyName;

            UserUpdateRequest updateRequest = new UserUpdateRequest
            {
                Connection = objAuth0Config.Connection,
                FirstName = objUpdateUserRequest.UserDetails.FirstName,
                LastName = objUpdateUserRequest.UserDetails.LastName,
                Email = objUpdateUserRequest.UserDetails.Email,
                PhoneNumber = objUpdateUserRequest.UserDetails.PhoneNumber,
                UserMetadata = userMetaData
            };

            User updatedUser = await managementApiClient.Users.UpdateAsync(objUpdateUserRequest.UserId, updateRequest);

            UserModel newUser = new UserModel
            {
                Email = updatedUser.Email,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                PhoneNumber = updatedUser.PhoneNumber,
                ReportsTo = userMetaData.ReportsTo,
                CompanyName = userMetaData.Company
            };
            return newUser;
        }

        public async Task<List<UserModel>> GetUsers(Auth0ConfigModel objAuth0Config, PaginationInfo objPaginationInfo)
        {
            MethodCollection methodCollection = new MethodCollection();
            string token = await methodCollection.GetAccessToken(objAuth0Config);

            return await GetUsers(token, objAuth0Config, objPaginationInfo);
        }

        public async Task<List<UserModel>> GetUsers(string token, Auth0ConfigModel objAuth0Config, PaginationInfo objPaginationInfo)
        {
            var managementApiClient = new ManagementApiClient(token, objAuth0Config.Domain);

            GetUsersRequest objGetUserRequest = new GetUsersRequest
            {
            };

            IPagedList<User> userList = await managementApiClient.Users.GetAllAsync(objGetUserRequest, objPaginationInfo);
            List<UserModel> listuser = new List<UserModel>();

            foreach (var item in userList)
            {
                UserModel user = await GetUserModel(token, objAuth0Config, item);

                if (user != null)
                    listuser.Add(user);
            }

            return listuser;
        }

        public async Task<UserModel> GetUserById(string userId, Auth0ConfigModel objAuth0Config)
        {
            MethodCollection methodCollection = new MethodCollection();
            string token = await methodCollection.GetAccessToken(objAuth0Config);

            return await GetUserById(token, userId, objAuth0Config);
        }

        public async Task<UserModel> GetUserById(string token, string userId, Auth0ConfigModel objAuth0Config)
        {
            var managementApiClient = new ManagementApiClient(token, objAuth0Config.Domain);

            User objAddedUser = await managementApiClient.Users.GetAsync(userId);

            return await GetUserModel(token, objAuth0Config, objAddedUser);
        }

        public async Task<UserModel> GetUserByEmail(string token, string email, Auth0ConfigModel objAuth0Config)
        {
            var managementApiClient = new ManagementApiClient(token, objAuth0Config.Domain);

            IList<User> users = await managementApiClient.Users.GetUsersByEmailAsync(email);

            if (users.Count > 0)
            {
                User addedUser = users[0];

                return await GetUserModel(token, objAuth0Config, addedUser);
            }

            return await Task.FromResult<UserModel>(null);
        }

        public async Task<List<UserModel>> GetUserRecipients(Auth0ConfigModel objAuth0Config, GetRecipientRequest objGetRecipientRequest)
        {
            MethodCollection methodCollection = new MethodCollection();
            string token = await methodCollection.GetAccessToken(objAuth0Config);
            var user = await GetUserById(objGetRecipientRequest.UserId, objAuth0Config);

            List<UserModel> listOfRecipients = new List<UserModel>();

            switch (user.Role)
            {
                case "Admin":
                    listOfRecipients = await GetUsers(token, objAuth0Config, new PaginationInfo { });
                    break;

                case "Coach":
                    //emailOfReportingUser = await this.GetUserReportingToEmail(token, objAuth0Config, objGetRecipientRequest.UserId);

                    if (!string.IsNullOrWhiteSpace(user.ReportsTo))
                    {
                        UserModel userReportsTo = await GetUserByEmail(token, user.ReportsTo, objAuth0Config);

                        if (userReportsTo != null)
                        {
                            List<UserModel> userRecipients = await GetAssignedUsers(token, objAuth0Config, user.Email);

                            listOfRecipients.Add(userReportsTo);
                            listOfRecipients.AddRange(userRecipients);
                        }
                    }

                    break;

                case "SalesPerson":
                    //emailOfReportingUser = await this.GetUserReportingToEmail(token, objAuth0Config, objGetRecipientRequest.UserId);
                    if (!string.IsNullOrWhiteSpace(user.ReportsTo))
                    {
                        UserModel userreportsto = await GetUserByEmail(token, user.ReportsTo, objAuth0Config);
                        if (userreportsto != null)
                        {
                            listOfRecipients.Add(userreportsto);
                        }
                    }
                    break;
            }
            return listOfRecipients;

        }

        public async Task AddUserToRoles(UserRole objUserRole, Auth0ConfigModel objAuth0Config)
        {
            MethodCollection methodCollection = new MethodCollection();
            string token = await methodCollection.GetAccessToken(objAuth0Config);
            var managementApiClient = new ManagementApiClient(token, objAuth0Config.Domain);
            GetRolesRequest getRolesRequest = new GetRolesRequest { NameFilter = objUserRole.RoleNames[0] };
            IPagedList<Role> roles = await managementApiClient.Roles.GetAllAsync(getRolesRequest);

            List<string> rolesList = new List<string>
            {
                roles[0].Id
            };
            AssignRolesRequest objAssignRoleRequest = new AssignRolesRequest { Roles = rolesList.ToArray() };

            await managementApiClient.Users.AssignRolesAsync(objUserRole.UserId, objAssignRoleRequest);

        }
        public async Task RemoveUserFromRoles(UserRole objUserRole, Auth0ConfigModel objAuth0Config)
        {

            MethodCollection methodCollection = new MethodCollection();
            string token = await methodCollection.GetAccessToken(objAuth0Config);
            var managementApiClient = new ManagementApiClient(token, objAuth0Config.Domain);

            GetRolesRequest getRolesRequest = new GetRolesRequest { NameFilter = objUserRole.RoleNames[0] };
            IPagedList<Role> roles = await managementApiClient.Roles.GetAllAsync(getRolesRequest);
            List<string> rolesList = new List<string>
            {
                roles[0].Id
            };
            AssignRolesRequest objAssignRoleRequest = new AssignRolesRequest { Roles = rolesList.ToArray() };
            await managementApiClient.Users.RemoveRolesAsync(objUserRole.UserId, objAssignRoleRequest);
        }

        public async Task<List<UserModel>> GetAssignedUsers(string token, Auth0ConfigModel objAuth0Config, string userEmail)
        {
            List<UserModel> UserList = await this.GetUsers(objAuth0Config, new PaginationInfo { });
            List<UserModel> reportingToList = new List<UserModel>();

            foreach (var item in UserList)
            {
                if (item.ReportsTo == userEmail)
                {
                    reportingToList.Add(item);
                }
            }
            return reportingToList;
        }

        public async Task<UserModel> GetUserModel(string token, Auth0ConfigModel objAuth0Config, User user)
        {
            if (user != null)
            {
                var managementApiClient = new ManagementApiClient(token, objAuth0Config.Domain);
                PaginationInfo pagination = new PaginationInfo();
                IPagedList<Role> userRoles = await managementApiClient.Users.GetRolesAsync(user.UserId, pagination);

                UserMetadata meta = !string.IsNullOrEmpty(Convert.ToString(user.UserMetadata)) ? JsonConvert.DeserializeObject<UserMetadata>(user.UserMetadata.ToString()) : null;

                var userRole = userRoles.Count > 0 ? userRoles[0].Name : "n/a"; ;

                UserModel objUser = new UserModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    PictureUrl = meta?.Picture,
                    ReportsTo = meta?.ReportsTo,
                    Role = userRole
                };

                return objUser;
            }

            return null;
        }

        public async Task RemoveUser(Auth0ConfigModel objConfigModel,string userId)
        {
            MethodCollection methodCollection = new MethodCollection();
            string token = await methodCollection.GetAccessToken(objConfigModel);
            var managementApiClient = new ManagementApiClient(token, objConfigModel.Domain);
            await managementApiClient.Users.DeleteAsync(userId);
        }

         
    }

}
