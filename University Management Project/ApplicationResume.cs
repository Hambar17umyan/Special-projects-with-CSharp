namespace University_Management_Project
{
    internal class ApplicationResume
    {
        public StudentInputModel Student;
        public FacultyDescribtionTransferType Faculty;


        public ApplicationResume(StudentInputModel student, FacultyDescribtionTransferType faculty)
        {
            Student = student;
            Faculty = faculty;
        }
    }
}