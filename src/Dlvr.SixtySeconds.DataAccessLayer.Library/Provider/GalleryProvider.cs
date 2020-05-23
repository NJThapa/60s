using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
    public class GalleryProvider
    {
        public DataContext _context;
        public GalleryProvider(DataContext dataContext)
        {
            _context = dataContext;
        }

        public List<GalleryVideo> GetAllVideos(int companyId)
        {
            List<GalleryVideo> objListOfVideos = new List<GalleryVideo>();
            List<Pitch> pitches = _context.Pitches.ToList();
            foreach(var objPitch in pitches)
            {
                Delivery objDelivery = _context.Deliveries.Where(x => x.DeliveryId == objPitch.DeliveryId).SingleOrDefault();
                ScriptContent objScriptContent = _context.ScriptContents.Where(x => x.ScriptId == objPitch.ScriptId).SingleOrDefault();
                Script objScript = _context.Scripts.Where(x => x.ScriptId == objPitch.ScriptId).SingleOrDefault();

                GalleryVideo objGalleryVideo = new GalleryVideo
                {
                    ScriptContent = objScriptContent,
                    Delivery = objDelivery,
                    PitchId = objPitch.PitchId,
                    Script = objScript
                };
                objListOfVideos.Add(objGalleryVideo);
            }
            return objListOfVideos;
        }
    }
}
