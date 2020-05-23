using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
    public class PitchProvider
    {
        public DataContext _context;

        public PitchProvider (DataContext dataContext)
        {
            _context = dataContext;
        }
        public Pitch GetScenarioById(int id)
        {
            Pitch objPitch = _context.Pitches.Find(id);
            return objPitch;
        }

        public void AddPitch (Pitch objPitch)
        {
            _context.Pitches.Add(objPitch);
            _context.SaveChanges();
        }

        public Pitch GetPitchBId (int id)
        {
          Pitch objPitch =   _context.Pitches.Where(x => x.PitchId == id).FirstOrDefault();
          return objPitch;
        }


        public void AlterPitchStatus(int pitchId, int state)
        {
          Pitch objPitch =  _context.Pitches.Where(x => x.PitchId == pitchId).SingleOrDefault();
          objPitch.State = state;
          _context.Pitches.Update(objPitch);
        }


    }
}
