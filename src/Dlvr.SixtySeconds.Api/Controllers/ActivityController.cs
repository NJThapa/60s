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
    public class ActivityController : ControllerBase
    {
        public DataContext _context;
        public ActivityController(DataContext dataContext)
        {
            _context = dataContext;
        }

        [Route("/api/activity/addcomment")]
        [HttpPost]
        public ResponseModel AddComment(Comment objComment)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);
                objActivityProvider.AddComment(objComment);
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 1,
                    Message = "Successfull",
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 0,
                    Message = ex.Message,
                };
                return objResponseModel;
            }
        }


        [Route("/api/activity/getcomments")]
        [HttpPost]
        public ResponseModelGetAll<Comment> GetComments(CommentRequest objCommentRequest)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);
                List<Comment> objComment = objActivityProvider.GetComments(objCommentRequest);
                ResponseModelGetAll<Comment> objResponseModel = new ResponseModelGetAll<Comment>
                {
                    Message = "Succesfull",
                    StatusCode = 0,
                    ObjectList = objComment
                };
                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModelGetAll<Comment> objResponseModel = new ResponseModelGetAll<Comment>
                {
                    Message = ex.Message,
                    StatusCode = 0
                };
                return objResponseModel;
            }


        }

        [Route("/api/activity/addlike")]
        [HttpPost]
        public ResponseModel AddLike(Like objLike)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);
                objActivityProvider.AddLike(objLike);
                ResponseModel objResponseMOdel = new ResponseModel
                {
                    Message = "Successfull",
                    StatusCode = 1
                };
                return objResponseMOdel;

            }
            catch (Exception ex)
            {
                ResponseModel objResponseModel = new ResponseModel
                {
                    StatusCode = 0,
                    Message = ex.Message

                };
                return objResponseModel;
            }
        }

     
        [Route("/api/activity/addfeedback")]
        [HttpPost]
        public ResponseModel<Feedback> AddFeedBack(Feedback objFeedBack)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);
                Feedback objAddedfacebook = objActivityProvider.AddFeedBack(objFeedBack);
                ResponseModel<Feedback> objResponseModel = new ResponseModel<Feedback>
                {
                    StatusCode = 1,
                    Message = "Added",
                    ResponseObject = objAddedfacebook
                };

                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Feedback> objResponseModel = new ResponseModel<Feedback>
                {
                    StatusCode = 1,
                    Message = ex.Message
                };

                return objResponseModel;
            }


        }

        [Route("/api/activity/updatefeedback")]
        [HttpPost]
        public ResponseModel<Feedback> UpdateFeedback(Feedback objFeedback)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);
                Feedback objAddedfacebook = objActivityProvider.AddFeedBack(objFeedback);
                ResponseModel<Feedback> objResponseModel = new ResponseModel<Feedback>
                {
                    StatusCode = 1,
                    Message = "Added",
                    ResponseObject = objAddedfacebook
                };

                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModel<Feedback> objResponseModel = new ResponseModel<Feedback>
                {
                    StatusCode = 1,
                    Message = ex.Message
                };

                return objResponseModel;
            }
        }

        [HttpPost]
        [Route("/api/activity/getallfeedback")]
        public ResponseModelGetAll<Feedback> GetAllFeedBack(GetFeedBackList objGetFeedBackList)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);
                List<Feedback> objFeedbackList = objActivityProvider.GetAllFeedBacks(objGetFeedBackList);
                ResponseModelGetAll<Feedback> objResponseModel = new ResponseModelGetAll<Feedback>
                {
                    StatusCode = 0,
                    Message = "Successfully Retrieved",
                    ObjectList = objFeedbackList
                };

                return objResponseModel;
            }
            catch (Exception ex)
            {
                ResponseModelGetAll<Feedback> objResponseModel = new ResponseModelGetAll<Feedback>
                {
                    StatusCode = 0,
                    Message = ex.Message
                };

                return objResponseModel;
            }
        }

        [HttpDelete]
        [Route("deletefeedback/{id}")]
        public IActionResult DeleteFeedBack(int id)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);

                bool result = objActivityProvider.DeleteFeedback(id);

                ResponseModelGetAll<Feedback> objResponseModel = new ResponseModelGetAll<Feedback>
                {
                    StatusCode = 1,
                    Message = "Successfully Deleted"
                };

                return Ok(objResponseModel);
            }
            catch (Exception ex)
            {
                ResponseModelGetAll<Feedback> objResponseModel = new ResponseModelGetAll<Feedback>
                {
                    StatusCode = 0,
                    Message = ex.Message
                };

                return StatusCode(500, objResponseModel);
            }
        }

        [HttpDelete]
        [Route("deletecomments/{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                ActivityProvider objActivityProvider = new ActivityProvider(_context);

                bool result = objActivityProvider.DeleteComment(id);

                ResponseModelGetAll<Feedback> objResponseModel = new ResponseModelGetAll<Feedback>
                {
                    StatusCode = 1,
                    Message = "Successfully Deleted"
                };

                return Ok(objResponseModel);
            }
            catch (Exception ex)
            {
                ResponseModelGetAll<Feedback> objResponseModel = new ResponseModelGetAll<Feedback>
                {
                    StatusCode = 0,
                    Message = ex.Message
                };

                return StatusCode(500, objResponseModel);
            }
        }

    }
}