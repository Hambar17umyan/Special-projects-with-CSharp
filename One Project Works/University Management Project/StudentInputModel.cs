namespace University_Management_Project
{
    internal class StudentInputModel
    {
        public StudentInputModel(string passportID, string firstName, string lastName, string email)
        {
            PassportID = passportID;
            FirstName = firstName;
            LastName = lastName;
            AdmissionExamGrades = new Grade[9];
            Email = email;
        }


        public void SetExamGrade(Exam exam, Grade grade)
        {
            AdmissionExamGrades[(int)Math.Log2((int)exam)] = grade;
        }

        public void ChangeFirstName(string newFirstName)
        {
            FirstName = newFirstName;
        }

        public void ChangeLastName(string newLastName)
        {
            LastName = newLastName;
        }

        public void ChangePassportID(string newPassportID)
        {
            PassportID = newPassportID;
        }
        public string PassportID { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public Grade[] AdmissionExamGrades { get; private set; }
    }
}