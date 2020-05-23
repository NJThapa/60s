using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dlvr.SixtySeconds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PitchController : ControllerBase
    {
        public IConfiguration Configuration;
        public DataContext _context;
        public PitchController(IConfiguration configuration, DataContext dataContext)
        {
            Configuration = configuration;
            _context = dataContext;
        }

 

        [Route("/api/pitch/movetocompleted")]
        [HttpPost]
        public ResponseModel MoveToCompleted (Pitch objPitch)
        {
            try
            {
                PitchProvider objPitchProvider = new PitchProvider(_context);
                objPitchProvider.AlterPitchStatus(objPitch.PitchId, objPitch.State);
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 1,
                    Message = "Moved"
                };
                return objResponseModel;

            }
            catch(Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 1,
                    Message = ex.Message
                };
                return objResponseModel;
            }

        }
    }

}