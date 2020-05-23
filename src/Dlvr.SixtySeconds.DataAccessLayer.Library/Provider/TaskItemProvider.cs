using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
   public class TaskItemProvider
    {
        public DataContext _context;
        public TaskItemProvider(DataContext dataContext)
        {
            _context = dataContext;
        }



        public TaskResponse GetTaskById(int id)
        {
            TaskItem objTask = _context.TaskItems.Where(x => x.TaskId == id).FirstOrDefault();
            Script objScript = _context.Scripts.Where(x => x.ScriptId == objTask.ScriptId).FirstOrDefault();
            List<ScriptContent> objScriptContents = _context.ScriptContents.Where(x => x.ScriptId == objScript.ScriptId).ToList();
            objScript.ScriptContents = objScriptContents;
            
            Scenario objScenario = _context.Scenarios.Where(x => x.ScenarioId == objTask.ScenarioId).FirstOrDefault();
            Delivery objDelivery =   _context.Deliveries.Where(x => x.ScriptId == objTask.ScriptId).FirstOrDefault();

            TaskResponse objTaskDetailedResponse = new TaskResponse
            {
                TaskDetails = objTask,
                Script = objScript,
                Scenario = objScenario,
                Delivery = objDelivery
            };
            return objTaskDetailedResponse;
        }

        public TaskItem AddTask (TaskItem objTaskitem)
        {
            objTaskitem.State = 0;
            _context.TaskItems.Add(objTaskitem);
            _context.SaveChanges();
            var TaskId = objTaskitem.TaskId;
            TaskResponse addedTaskResponse = this.GetTaskById(TaskId);
            return addedTaskResponse.TaskDetails ;

        }
        public TaskItem UpdateTask(TaskItem objTaskItem)
        {
            objTaskItem.State = 0;
            objTaskItem.UpdatedOn = DateTimeOffset.UtcNow;
            _context.TaskItems.Add(objTaskItem);
            _context.SaveChanges();
            var TaskItemId = objTaskItem.TaskId;
            TaskResponse addedTaskResponse = this.GetTaskById(TaskItemId);
            return addedTaskResponse.TaskDetails;
        }
        public List<TaskResponse> GetAllTask(int companyId)
        {
            List<TaskItem> objAllTask = _context.TaskItems.ToList();
            List<TaskResponse> objAllTaskRespnse = new List<TaskResponse>();
            foreach (var taskItem in objAllTask)
            {
                TaskResponse objTaskItem = this.GetTaskById(taskItem.TaskId);
                objAllTaskRespnse.Add(objTaskItem);

            }
                return objAllTaskRespnse;
        }

        public void DeleteTask(TaskItem objTaskItem)
        {
            _context.TaskItems.Remove(objTaskItem);
            Script objScript = _context.Scripts.Where(x => x.ScenarioId == objTaskItem.ScriptId).FirstOrDefault();
            Scenario objScenario = _context.Scenarios.Where(x => x.ScenarioId == objScript.ScenarioId).FirstOrDefault();
            Delivery objDelivery = _context.Deliveries.Where(x => x.TaskId == objTaskItem.TaskId).FirstOrDefault                                                               ();
            _context.Scripts.Remove(objScript);
            _context.Scenarios.Remove(objScenario);
            if (objDelivery != null) { _context.Deliveries.Remove(objDelivery); }
            _context.SaveChanges();
        }
        
        public void AssignTask(List<AssignedTask> objAssignedTask)
        {
            _context.AssignedTasks.AddRange(objAssignedTask);
            _context.SaveChanges();
        }

        public TaskWithActivity GetTaskWithActivity(int taskId)
        {
            TaskResponse objTaskResponse = this.GetTaskById(taskId);
           
            List<Feedback> objFeedbacks = _context.Feedbacks.Where(x => x.Type == 0 && x.TypeId == taskId).ToList();

            Delivery objDelivery = _context.Deliveries.Where(x => x.TaskId == taskId).FirstOrDefault();
             Rehersal objReherseal = _context.Rehersals.Where(x => x.TaskId == taskId).FirstOrDefault();

            TaskWithActivity objTaskWithActivity = new TaskWithActivity
            {
                Feedbacks = objFeedbacks,
                TaskResponse = objTaskResponse,
                RehersalDetails = objReherseal,
                Delivery = objDelivery

            };
            return objTaskWithActivity;
         
        }
    }
}
