using Auth0.ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Models
{
    public class ResponseModel<T> where T : class
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T ResponseObject { get; set; }

    }

    public class ResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ResponseModelAuth0
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<UserModel> Userlist { get; set; }
    }

    public class ResponseModelGetAll<T> where T : class
    {
        public List<T> ObjectList { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class ScriptResponse
    {
        public List<ScriptContent> ScriptContents { get; set; }
        public Script Script { get; set; }
    }

    public class TaskResponse
    {
        public TaskItem TaskDetails { get; set; }
        public Script Script { get; set; }
        public Scenario Scenario { get; set; }
        public Delivery Delivery { get; set; }
             
    }
    public class ScriptReview 
    {
        public Script Script { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public DateTimeOffset AddedOn { get; set; }
        public DateTimeOffset ApprovedOn { get; set; }
        
    }

    public class TaskWithActivity
    {
        public TaskResponse TaskResponse { get; set; }
        public List<Feedback> Feedbacks { get; set; }
        public Rehersal RehersalDetails { get; set; }
        public Delivery Delivery { get; set; }
    }
}