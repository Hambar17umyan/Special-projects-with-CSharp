using Pizza_Ordering_Application.Application_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Ordering_Application.Interfaces
{
    internal interface IFeedbackGetter
    {
        void GetFeedback(Feedback feedback);
        List<FeedbackOutputModel> ShowFeedbacks();
    }
}
