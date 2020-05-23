using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class RequestModels
    {

    }

    public class GetById
    {
        public int Id { get; set; }
    }
    public class GetByIdAuth0
    {
        public string ID { get; set; }
    }

    public class UpdateUserRequest
    {
        public UserModel UserDetails { get; set; }
        public string UserId { get; set; }
    }
    public class AddScriptRequest
    {
        public List<ScriptContent> ScriptContents { get; set; }
        public Script ScriptDetails { get; set; }

    }

    public class GetRecipientRequest
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
    public class AddEmailNotificationRequest
    {
        public List<EmailNotification> ListEmailNotificationDetails { get; set; }

    }

    public class ListRequestModel<T> where T : class
    {
        public List<T> ObjectList { get; set; }
    }

    public class GetFeedBackList
    {
        public  int Type { get; set; }
        public int TypeId { get; set; }
        public int CompanyId { get; set; }
    }

    public class CommentRequest
    {
        public int Type { get; set; }
        public int TypeId { get; set; }
        public int CompanyId { get; set; }
    }

    
}
