namespace University_Management_Project
{
    internal partial class University
    {
        public readonly string Name;
        public readonly int ID;
        public readonly DateTime FoundationDate;

        private List<Faculty> faculties;
        private Budget Budget;
        private TechnicalAdministration Administration;
        private List<(Faculty faculty, StudentModel)> Students;

        public University(string name, DateTime foundationDate, Budget budget, int totalSeatsCount)
        {
            Count++;
            ID = Count;
            Universities.Add(this);

            Name = name;
            FoundationDate = foundationDate;
            Budget = budget;
            TotalSeatsCount = totalSeatsCount;

            Administration = new TechnicalAdministration();
            faculties = new List<Faculty>();
            FacultySet = new HashSet<string>();
            Faculties = new List<string>();
            Students = new List<(Faculty faculty, StudentModel)>();
            ApplyingAndNotStudents = new HashSet<string>();
        }
        public Message TryAddingFaculty(FacultyInputModel faculty)
        {
            if (faculty.TotalSeats > TotalSeatsCount)
            {
                return new Message("Invalid Seats Count.", "There is no enough space in your university to create this faculty.");
            }
            int sz = FacultySet.Count;
            FacultySet.Add(faculty.Name);
            if (sz == FacultySet.Count)
            {
                return new Message("Invalid Name.", $"There is already a faculty with the name {faculty.Name} in your university.");
            }

            Faculty f = new Faculty(this, faculties.Count, faculty.Name, faculty.CourseTime, faculty.TotalSeats, faculty.RequiredExams);
            faculties.Add(f);
            Faculties.Add(f.Name);
            return Message.Success;
        }
        public Message TryGettingApplication(ApplicationResume resume)
        {
            var fac = resume.Faculty;
            if (ID != fac.UniversityID)
                return new Message("Invalid University ID.", "You have called the function on another university. Review the id-s!");

            if (faculties.Count <= fac.FacultyID)
                return new Message("Invalid Faculty ID.", $"This university does not contain a faculty with the id {fac.FacultyID}.");

            return faculties[fac.FacultyID].AddApplication(resume);
        }
        public void Update()
        {
            foreach (var fac in faculties)
            {
                fac.StartUpdate();
            }
        }

        private Message AddStudent(int facultyID, StudentInputModel student)
        {
            Administration.SendEmailToStudent(student.PassportID, "Congrates! You have been accepted to the uiversity.");
            StudentModel studentModel = new StudentModel(student, ID, facultyID);
            Students.Add((faculties[facultyID], studentModel));
            return faculties[facultyID].AddStudent(studentModel);
        }

        private HashSet<string> ApplyingAndNotStudents;
        private HashSet<string> FacultySet;
        public int TotalSeatsCount { get; private set; }
        public int FreeSeatsCount => TotalSeatsCount - Students.Count;
        public List<string> Faculties { get; private set; }

    }
}