namespace University_Management_Project
{
    internal class FacultyInputModel
    {
        public string Name { get; set; }
        public TimeSpan CourseTime { get; set; }
        public Exam RequiredExams { get; set; }
        public int TotalSeats { get; set; }

        public FacultyInputModel(string name, int totalSeats, TimeSpan courseTime, Exam exams)
        {
            Name = name;
            TotalSeats = totalSeats;
            CourseTime = courseTime;
            RequiredExams = exams;
        }
    }
}