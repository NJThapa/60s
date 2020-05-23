using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
    public class UserDeviceTokenProvider
    {
        public DataContext _context;
        public UserDeviceTokenProvider(DataContext dataContext)
        {
            _context = dataContext;
        }

        public UserDeviceToken GetUserDeviceTokenById(string id, int devicetype)
        {
            return _context.UserDeviceTokens.Where(x => x.UserId == id && x.DeviceType == devicetype).FirstOrDefault();
        }

        public void SaveUserDeviceToken(UserDeviceToken userDevieToken)
        {
            var devicetoken = GetUserDeviceTokenById(userDevieToken.UserId, userDevieToken.DeviceType);

            if (devicetoken != null)
            {
                devicetoken.DeviceToken = userDevieToken.DeviceToken;
                devicetoken.UpdatedOn = DateTime.UtcNow;
            }
            else
            {
                userDevieToken.CreatedOn = DateTime.UtcNow;
                _context.UserDeviceTokens.Add(userDevieToken);
            }

            _context.SaveChanges();
        }
    }
}
