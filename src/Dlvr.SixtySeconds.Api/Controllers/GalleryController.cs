using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dlvr.SixtySeconds.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        public DataContext _context { get; set; }
        public GalleryController (DataContext dataContext )
        {
            _context = dataContext;
        }

        [HttpPost]
        [Route("/api/gallery/getall")]
        public ResponseModelGetAll<GalleryVideo> GetAll(GetById objGetByid)
        {
            try
            {
                GalleryProvider objGalleryProvider = new GalleryProvider(_context);
                List<GalleryVideo> objListofGalleryVideos = objGalleryProvider.GetAllVideos(objGetByid.Id);
                ResponseModelGetAll<GalleryVideo> objResponseModel = new ResponseModelGetAll<GalleryVideo>
                {
                    StatusCode = 1,
                    Message = "Loaded",
                    ObjectList = objListofGalleryVideos
                };
                return objResponseModel;
            }
            catch(Exception ex)
            {
                ResponseModelGetAll<GalleryVideo> objResponseModel = new ResponseModelGetAll<GalleryVideo>
                {
                    StatusCode = 1,
                    Message = ex.Message
                };
                return objResponseModel;
            }


        }
    }
}