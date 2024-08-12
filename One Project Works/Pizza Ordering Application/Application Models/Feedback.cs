using Pizza_Ordering_Application.Enums;

namespace Pizza_Ordering_Application
{
    internal class Feedback
    {
        public readonly string Text;
        public readonly FeedbackGrade Grade;

        public Feedback(string text, FeedbackGrade grade)
        {
            Text = text;
            Grade = grade;
        }
    }
}