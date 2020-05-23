using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
    public class ScriptProvider
    {
        public DataContext _context;
        public ScriptProvider(DataContext dataContext)
        {
            _context = dataContext;
        }
        public Script GetScriptById(int id)
        {
            Script objScript = _context.Scripts.Where(x => x.ScriptId == id).Single();
            List<ScriptContent> objScriptContents = _context.ScriptContents.Where(x => x.ScriptId == id).ToList();
            objScript.ScriptContents = objScriptContents;
            return objScript;
        }
        public List<ScriptContent> GetScriptContents(int scriptId)
        {
            List<ScriptContent> scriptContents = _context.ScriptContents.Where(x => x.ScriptId == scriptId).ToList();
            return scriptContents;
        }
        public Script AddScript(Script objScript)
        {
            objScript.State = 0;
            objScript.CreatedOn = DateTimeOffset.UtcNow;
            _context.Scripts.Add(objScript);
            _context.SaveChanges();
            int scriptId = objScript.ScriptId;
            Script addedScript = this.GetScriptById(scriptId);
            return addedScript;
        }

        public List<ScriptContent> AddScriptContent(List<ScriptContent> objScriptContents, int scriptId)
        {
            foreach (ScriptContent scriptContent in objScriptContents)
            {
                scriptContent.ScriptId = scriptId;
                _context.ScriptContents.Add(scriptContent);
                _context.SaveChanges();
            }
            List<ScriptContent> scriptContents = _context.ScriptContents.Where(x => x.ScriptId == scriptId).ToList();
            return scriptContents;
        }

        public List<ScriptContent> UpdateScriptContents(List<ScriptContent> objScriptContents,int scriptId)
        {
            foreach (ScriptContent scriptContent in objScriptContents)
            {
                _context.ScriptContents.Update(scriptContent);
                _context.SaveChanges();
            }
            List<ScriptContent> scriptContents = _context.ScriptContents.Where(x => x.ScriptId == scriptId).ToList();
            return scriptContents;
        }

        public Script UpdateScript(Script objScript)
        {
            objScript.State = 0;
            objScript.UpdatedOn = DateTimeOffset.UtcNow;
            _context.Scripts.Update(objScript);
            _context.SaveChanges();
            int ScriptId = objScript.ScriptId;
            Script updatedScript = this.GetScriptById(ScriptId);
            return updatedScript;
        }


        public List<Script> GetAllScripts()
        {
            List<Script> scripts = _context.Scripts.ToList();
            return scripts;
        }

        public void DeleteScriptContent(int ScriptId)
        {
            List<ScriptContent> scriptContents = _context.ScriptContents.Where(x => x.ContentId == ScriptId).ToList();
            _context.ScriptContents.RemoveRange(scriptContents);
            _context.SaveChanges();
        }

        public void DeleteScript(Script objScript)
        {
            _context.Scripts.Remove(objScript);
            _context.SaveChanges();
        }

        public void ApproveScript(ApprovalRequest objApprovalRequest)
        {
            Script objScript = _context.Scripts.Find(objApprovalRequest.TypeId);
            objScript.State = 1;
            objScript.ApprovedOn = DateTimeOffset.UtcNow;
            _context.Scripts.Update(objScript);
            _context.SaveChanges();
            Scenario objScenario = _context.Scenarios.Where(x => x.ScenarioId == objScript.ScenarioId).SingleOrDefault();
            if(objScenario.State == 1)
            {
                TaskItem objTask = _context.TaskItems.Where(x => x.ScriptId == objScript.ScriptId).SingleOrDefault();
                PitchProvider objPitchProvider = new PitchProvider(_context);
                Delivery ObjDelivery = _context.Deliveries.Where(x => x.ScriptId == objScript.ScriptId && x.State==1).FirstOrDefault();
                if (ObjDelivery != null)
                {
                    Pitch objPitch = new Pitch
                    {
                        DeliveryId = ObjDelivery.DeliveryId,
                        ScriptId = objScript.ScriptId,
                        ScenarioId = objApprovalRequest.Id,
                        TaskId = objTask.TaskId,
                        State = 0
                    };
                    objPitchProvider.AddPitch(objPitch);
                }
                _context.SaveChanges();
            }
        }


        public ScriptReview GetScriptReview (int scriptId)
        {
            Script objScript = _context.Scripts.Where(x => x.ScriptId == scriptId).FirstOrDefault();
            List<ScriptContent> objListScriptContent = _context.ScriptContents.Where(x => x.ScriptId == scriptId).ToList();
            objScript.ScriptContents = objListScriptContent;            
            List <Feedback> objListFeedBacks = _context.Feedbacks.Where(x => x.Type == 0 && x.TypeId == scriptId).ToList();
            ScriptReview objScriptReview = new ScriptReview
            {
                Feedbacks = objListFeedBacks,
                Script = objScript,
                AddedOn = objScript.CreatedOn,
                ApprovedOn  = objScript.ApprovedOn
            };
            return objScriptReview;
        }
    }
}
