using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Dlvr.SixtySeconds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public IConfiguration Configuration;
        public DataContext _context;
        public NotificationController(IConfiguration configuration, DataContext dataContext)
        {
            Configuration = configuration;
            _context = dataContext;
        }
    }
}