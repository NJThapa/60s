using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Common
{
    public class MethodCollection
    {
        public async Task<string> GetAccessToken(Auth0ConfigModel objAuth0ConfigModel)
        {
              string auth0Domain = objAuth0ConfigModel.Domain;
            AuthenticationApiClient authAPiclient = new AuthenticationApiClient(auth0Domain);
            ClientCredentialsTokenRequest tokenRequest = new ClientCredentialsTokenRequest { 
              Audience = "https://dev-8todrv-h.au.auth0.com/api/v2/",
              ClientId = objAuth0ConfigModel.ClientId,
              ClientSecret = objAuth0ConfigModel.ClientSecret
            };
                var tokenResponse =
                    await authAPiclient.GetTokenAsync(tokenRequest);
                return tokenResponse.AccessToken;
        }

        public string GetAccessToken()
        {
            var client = new RestClient("https://dev-8todrv-h.au.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"G7Uwe9nwF41Ja1AMVUBUzAx5N3jPxJ1H\",\"client_secret\":\"_Gkv6FpTvm_Lr_KiXvCqJ3gF5w0A3Hm7iSojlvSZM3g1-BRrXplOHl1p55ATxRrI\",\"audience\":\"https://sixsdev-api.azurewebsites.net\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var responseObject = JsonConvert.DeserializeObject<WebTokenModel>(response.Content);
            return responseObject.access_token;
        }

        public  void SendEmail (EmailNotification objEmailNotification)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress fromAddress = new MailboxAddress(objEmailNotification.NotificationFrom, "myriaddlvr@outlook.com");
            message.From.Add(fromAddress);

            MailboxAddress toAddres = new MailboxAddress("User",  objEmailNotification.NotificationToEmail);
            message.To.Add(toAddres);
            message.Subject = "Approval Waiting";

            BodyBuilder objBodyBuilder = new BodyBuilder();
           // objBodyBuilder.HtmlBody = "<h1>waiting for approval </td>";
            objBodyBuilder.TextBody = "On delivery or task or step";

            message.Body = objBodyBuilder.ToMessageBody();

            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Connect("smtp-mail.outlook.com", 587, false);
            objSmtpClient.Authenticate("myriaddlvr@outlook.com", "Myriadx1234@@");
            objSmtpClient.Send(message);
            objSmtpClient.Disconnect(true);
            objSmtpClient.Dispose();
        }

    }
}
