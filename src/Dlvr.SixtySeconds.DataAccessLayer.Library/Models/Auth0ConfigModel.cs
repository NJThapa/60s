using System;
using System.Collections.Generic;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library
{
    public class Auth0ConfigModel
    {
       public string Connection { get; set; }
       public string Domain { get; set; }
       public string ClientSecret { get; set; }
       public string ClientId { get; set; }
       public string ManagementApiDomain { get; set; }
    }
}
