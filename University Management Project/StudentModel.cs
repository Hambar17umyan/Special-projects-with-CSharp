namespace University_Management_Project
{
    partial class University
    {
        private class StudentModel
        {
            static int count;

            public readonly int StudentID;
            public readonly string PassportID;
            public readonly string FirstName;
            public readonly string LastName;
            public readonly int UniversityID;
            public readonly int FacultyID;
            public readonly DateTime AdmissionDate;
            public readonly string Email;

            static StudentModel()
            {
                count = 0;
            }
            private StudentModel(string passportID, string firstName, string lastName, string email, int universityID, int facultyID)
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
                Email = email;
            }

            public StudentModel(StudentInputModel student, int universityID, int facultyID) : this(student.PassportID, student.FirstName, student.LastName, student.Email, universityID, facultyID)
            {

            }

            public List<Grade> Grades { get; private set; }
            public Grade GPA => CalculateGPA();


            private Grade CalculateGPA()
            {
                decimal gr = 0;
                foreach (Grade g in Grades)
                {
                    gr += (decimal)g;
                }
                if (Grades.Count > 0)
                    gr /= Grades.Count;
                else return Grade.None;

                return (Grade)(int)Math.Round(gr);
            }

            public void AddGrade(Grade grade)
            {
                Grades.Add(grade);
            }

        }
    }
}