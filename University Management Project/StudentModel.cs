namespace University_Management_Project
{
    partial class University
    {
        public class StudentModel
        {
            static int count;

            public readonly int StudentID;
            public readonly string PassportID;
            public readonly string FirstName;
            public readonly string LastName;
            public readonly int UniversityID;
            public readonly int FacultyID;
            public readonly DateTime AdmissionDate;

            static StudentModel()
            {
                count = 0;
            }
            private StudentModel(string passportID, string firstName, string lastName, int universityID, int facultyID)
            {
                AdmissionDate = DateTime.Now;
                StudentID = count;
                count++;
                FirstName = firstName;
                LastName = lastName;
                PassportID = passportID;
                UniversityID = universityID;
                FacultyID = facultyID;
                Grades = new List<Grade>();
            }

            public StudentModel(StudentInputModel student, int universityID, int facultyID): this(student.PassportID, student.FirstName, student.LastName, universityID, facultyID)
            {

            }

            public List<Grade> Grades;
            public Grade GPA => CalculateGPA();


            private Grade CalculateGPA()
            {
                decimal gr = 0;
                foreach (Grade g in Grades)
                {
                    gr += (decimal)g;
                }
                gr /= Grades.Count;

                return (Grade)(int)Math.Round(gr);
            }

        }
    }
}