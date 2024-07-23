namespace University_Management_Project
{
    internal class ApplicationResume
    {
        public readonly StudentInputModel Student;
        public readonly FacultyDescribtionTransferType Faculty;
        public readonly DateTime ApplicationDate;


        public ApplicationResume(StudentInputModel student, FacultyDescribtionTransferType faculty)
        {
            Student = student;
            Faculty = faculty;
            ApplicationDate = DateTime.Now;
        }
    }
}