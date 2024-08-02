using Pizza_Ordering_Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Application_Models
{
    internal class FeedbackOutputModel
    {
        public string Text;
        public FeedbackGrade Grade;

        public FeedbackOutputModel(Feedback fb)
        {
            Text = fb.Text;
            Grade = fb.Grade;
        }
    }
}
