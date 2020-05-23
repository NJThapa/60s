using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
    public class ScenarioProvider
    {

        public DataContext _context;
        public ScenarioProvider(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Scenario GetScenarioById(int id)
        {
            Scenario objScenario = _context.Scenarios.Where(x => x.ScenarioId == id).SingleOrDefault();
            return objScenario;
        }

        public Scenario AddScenario(Scenario objScenario)
        {
            objScenario.State = 0;
            objScenario.CreatedOn = DateTimeOffset.UtcNow;
            objScenario.UpdatedOn = DateTimeOffset.UtcNow;
            _context.Scenarios.Add(objScenario);
            _context.SaveChanges();
            int scenarioId = objScenario.ScenarioId;
            Scenario scenario = this.GetScenarioById(scenarioId);
            return scenario;
        }
        public Scenario UpdateScenario(Scenario objScenario)
        {
            objScenario.State = 0;
            objScenario.UpdatedOn = DateTimeOffset.UtcNow;
            _context.Scenarios.Update(objScenario);
            _context.SaveChanges();
            int scenarioId = objScenario.ScenarioId;
            Scenario scenario = this.GetScenarioById(scenarioId);
            return scenario;
        }
        public List<Scenario> GetAllScenario()
        {
            List<Scenario> scenarios = _context.Scenarios.ToList();
            return scenarios;
        }

        public void DeleteScenario(Scenario objScenario)
        {
            _context.Scenarios.Remove(objScenario);
            _context.SaveChanges();
        }

        public void ApproveScenario(ApprovalRequest objApprovalRequest)
        {
            Scenario objScenario = _context.Scenarios.Find(objApprovalRequest.TypeId);
            objScenario.State = 1;
         
            _context.Scenarios.Update(objScenario);
            _context.SaveChanges();
            Script objScript = _context.Scripts.Where(x => x.ScenarioId == objApprovalRequest.Id).FirstOrDefault();

            if (objScript.State == 1)
            {

                TaskItem objTask = _context.TaskItems.Where(x => x.ScriptId == objScript.ScriptId).SingleOrDefault();
                //objTask.State = 1;
                //_context.TaskItems.Update(objTask);

                PitchProvider objPitchProvider = new PitchProvider(_context);
                _context.SaveChanges();
                Delivery ObjDelivery = _context.Deliveries.Where(x => x.ScriptId == objScript.ScriptId && x.State == 1).FirstOrDefault();

                if (ObjDelivery != null)
                {
                    Pitch objPitch = new Pitch
                    {
                        DeliveryId = ObjDelivery.DeliveryId,
                        ScriptId = objScript.ScriptId,
                        ScenarioId = objApprovalRequest.Id,
                        TaskId = objTask.TaskId
                    };
                    objPitchProvider.AddPitch(objPitch);
                }

            }
        }
    }
}
