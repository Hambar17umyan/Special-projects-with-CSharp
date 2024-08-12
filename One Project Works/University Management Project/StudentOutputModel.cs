using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_Project
{
    internal class StudentOutputModel
    {
        public int FacultyID { get; private set; }
        public DateTime AdmissionDate { get; private set; }
        public string FirstName { get; private set; }
        public Grade GPA { get; private set; }
        public List<Grade> Grades { get; private set; }
        public string LastName { get; private set; }
        public string PassportID { get; private set; }
        public int StudentID { get; private set; }
        public int UniversityID { get; private set; }

        public StudentOutputModel(int FacultyID, DateTime AdmissionDate, string FirstName, string LastName, Grade GPA, List<Grade>Grades, string PassportID, int StudentID, int UniversityID)
        {
            this.FacultyID = FacultyID;
            this.AdmissionDate = AdmissionDate;
            this.FirstName = FirstName; 
            this.LastName = LastName;
            this.PassportID = PassportID;
            this.StudentID = StudentID;
            this.UniversityID = UniversityID;
            this.Grades = Grades;
            this.GPA = GPA;
        }

    }
}
