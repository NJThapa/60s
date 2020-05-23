using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
   public class EmailNotificationProvider
    {
        DataContext _context { get; set; }

        public EmailNotificationProvider(DataContext dataContext)
        {
            _context = dataContext;
        }

        public void AddEmailNotification ()
        {

        }

    }
}
