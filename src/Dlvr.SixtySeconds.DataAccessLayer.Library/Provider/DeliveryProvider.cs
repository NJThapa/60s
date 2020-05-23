using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
   public class DeliveryProvider
    {
        public DataContext _context;
        public DeliveryProvider (DataContext dataContext)
        {
            _context = dataContext;
        }

        public Delivery GetDeliveryById(int id)
        {
             Delivery objDelivery  =  _context.Deliveries.Where(x => x.DeliveryId == id).Single();
            return objDelivery;
        }

        public Delivery AddDelivery(Delivery objDelivery)
        {
            objDelivery.State = 0;
            _context.Deliveries.Add(objDelivery);
            _context.SaveChanges();
            int deliveryId = objDelivery.DeliveryId;
            Delivery objAddedDelivery = this.GetDeliveryById(objDelivery.DeliveryId);
            return objAddedDelivery;
        } 

        public Delivery UpdateDelivery (Delivery objDelivery)
        {
            objDelivery.State = 0;
            objDelivery.UpdatedOn = DateTimeOffset.UtcNow;
            _context.Deliveries.Update(objDelivery);
            _context.SaveChanges();
            int deliveryId = objDelivery.DeliveryId;
            Delivery objAddedDelivery = this.GetDeliveryById(objDelivery.DeliveryId);
            return objAddedDelivery;
        }

        public void DeleteDelivery(Delivery objDelivery)
        {
            _context.Deliveries.Remove(objDelivery);
            _context.SaveChanges();
        }


        public void ApproveDelivery(ApprovalRequest objApprovalRequest)
        {
            Delivery objDelivery = _context.Deliveries.Find(objApprovalRequest.TypeId);
            objDelivery.State = 1;
            _context.Deliveries.Update(objDelivery);
            _context.SaveChanges();

            Script objScript = _context.Scripts.Where(x => x.ScriptId == objDelivery.ScriptId).SingleOrDefault();
            Scenario objScenario = _context.Scenarios.Where(x => x.ScenarioId == objScript.ScenarioId).SingleOrDefault();
            
            if(objScript.State==1 && objScenario.State ==1)
            {
                PitchProvider objPitchProvider = new PitchProvider(_context);
                Pitch objPitch = new Pitch
                {
                    ScriptId = objScript.ScriptId,
                    ScenarioId = objScenario.ScenarioId,
                    DeliveryId = objDelivery.DeliveryId,
                    State = 1,
                    CreatedBy = objDelivery.CreatedBy
                };

                objPitchProvider.AddPitch(objPitch);
            }
        
        }

    }
}
