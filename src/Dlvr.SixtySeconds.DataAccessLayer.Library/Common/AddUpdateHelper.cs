using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Common
{
    public class AddUpdateHelper
    {
        public DataContext _context;
        public AddUpdateHelper(DataContext dbcontext)
        {
            _context = dbcontext;
        }
        public void Add<T>(ref T obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public void Update<T>(ref T obj)
        {
            _context.Update(obj);
            _context.SaveChanges();
        }
        public void Delete<T>(ref T obj)
        {
            _context.Remove(obj);
            _context.SaveChanges();
        }

    }
}
