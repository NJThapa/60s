using Dlvr.SixtySeconds.DataAccessLayer.Library.Common;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Data;
using Dlvr.SixtySeconds.DataAccessLayer.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Provider
{
    public class ActivityProvider
    {
        DataContext _context;
        public ActivityProvider(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void AddEmailNotification(List<EmailNotification> objEmailNotifications)
        {
            _context.EmailNotifications.AddRange(objEmailNotifications);
            _context.SaveChanges();
            //MethodCollection objMethodCollection = new MethodCollection();
            //foreach (var emailNotificationDetails in objEmailNotifications)
            //{
            //    objMethodCollection.SendEmail(emailNotificationDetails);
            //}
        }

        public void AddComment(Comment objComment)
        {
            objComment.CreatedOn = DateTimeOffset.UtcNow;
            _context.Comments.Add(objComment);
            _context.SaveChanges();
        }
        public void UpdateComment(Comment objComment)
        {
            objComment.UpdatedOn = DateTimeOffset.UtcNow;
            _context.Comments.Update(objComment);
            _context.SaveChanges();
        }


        public List<Comment> GetComments (CommentRequest objCommentRequest)
        {
            List<Comment> objCommentList = _context.Comments.Where(x => x.Type == objCommentRequest.Type && x.TypeId == objCommentRequest.TypeId).ToList();
            return objCommentList;
        }

        public void AddLike(Like objLike)
        {
            _context.Likes.Add(objLike);
            _context.SaveChanges();
        }
        public void RemoveLike(Like objLike)
        {
            _context.Likes.Remove(objLike);
            _context.SaveChanges();
        }


        public Feedback AddFeedBack(Feedback objFeedBack)
        {
            _context.Feedbacks.Add(objFeedBack);
            _context.SaveChanges();
            Feedback objAddedFeedback = _context.Feedbacks.Where(x => x.FeedbackId == objFeedBack.FeedbackId).SingleOrDefault();
            return objAddedFeedback;
        }
        public Feedback UpdateFeedback(Feedback objFeedBack)
        {
            objFeedBack.UpdatedOn = DateTimeOffset.UtcNow;
            _context.Feedbacks.Update(objFeedBack);
            _context.SaveChanges();
            Feedback objAddedFeedback = _context.Feedbacks.Where(x => x.FeedbackId == objFeedBack.FeedbackId).SingleOrDefault();
            return objAddedFeedback;
        }

        public List<Feedback> GetAllFeedBacks(GetFeedBackList objGetFeedBackList)
        {
            List<Feedback> objListFeedBack = _context.Feedbacks.Where(x => x.DeletedOn == null && x.Type == objGetFeedBackList.Type && x.TypeId == objGetFeedBackList.TypeId).ToList();
            return objListFeedBack;
        }

        public bool DeleteFeedback(int id)
        {
            bool result = false;

            var feedback = _context.Feedbacks.Find(id);

            if(feedback != null)
            {
                feedback.DeletedOn = DateTime.UtcNow;

                _context.SaveChanges();
                result = true;
            }

            return result;
        }

        public bool DeleteComment(int id)
        {
            bool result = false;

            var comment = _context.Comments.Find(id);

            if (comment != null)
            {
                comment.DeletedOn = DateTime.UtcNow;

                _context.SaveChanges();
                result = true;
            }

            return result;
        }
    }
}
