using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dlvr.SixtySeconds.DataAccessLayer.Library.Common
{
    public class AppEnum
    {
        public enum Roles
        {
            [Description("Full ROle ID")]
            Admin,
            [Description("Full Role ID")]
            User,
            [Description("Full Role Id")]
            Coach
        }

        public enum States
        {
            ToReview = 0,
            Approved = 1,
            Completed = 2,
        }

        public enum EmailNotificationType
        {
            Script = 0,
            Scenario = 1,
            Delivery = 2
        }

        public enum DeviceType
        {
            Web = 1,
            Mobile = 2
        }
    }
}
