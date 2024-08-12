namespace University_Management_Project
{
    internal partial class University
    {
        public readonly string Name;
        public readonly int ID;
        public readonly DateTime FoundationDate;

        public string Domain { get; private set; } //Special domain for Email
        public int TotalSeatsCount { get; private set; }
        public int FreeSeatsCount => TotalSeatsCount - Students.Count;
        public List<string> Faculties { get; private set; } //Names of the faculties with their actual id-s

        private HashSet<string> ApplyingAndNotStudents; //The HashSet of passport id-s of students that have applied for education or are already in the university
        private HashSet<string> StudentsSet; //The HashSet of passport id-s of students that are in the university
        private HashSet<string> FacultySet; //The HashSet of University faculty names
        
        private List<Faculty> faculties;
        private List<(Faculty faculty, StudentModel student)> Students; //The list of all students with their faculties
        private Budget UniversityBudget;
        private TechnicalAdministration Administration;

        public University(string name, DateTime foundationDate, BudgetInputModel budget, int totalSeatsCount)
        {
            Count++;
            ID = Count;
            Universities.Add(this);

            Name = name;
            FoundationDate = foundationDate;
            UniversityBudget = new Budget(budget);
            TotalSeatsCount = totalSeatsCount;
            Domain = "";

            Administration = new TechnicalAdministration(new UniversityInputModel(this));
            faculties = new List<Faculty>();
            FacultySet = new HashSet<string>();
            Faculties = new List<string>();
            Students = new List<(Faculty faculty, StudentModel)>();
            ApplyingAndNotStudents = new HashSet<string>();
            StudentsSet = new HashSet<string>();
        }
        
        public void setDomain(string domain)
        {
            Domain = domain;
            Administration.ChangeEmail(domain);
        }

        public void AddMoneyToBudget(Money money)
        {
            UniversityBudget += money;
        }
        public void TakeMoneyFromBudget(Money money)
        {
            UniversityBudget -= money;
        }

        public Message TryAddingSeats(int seatsNumber, int facultyID)
        {
            if (facultyID >= faculties.Count)
                return new Message("Invalid FacultyID.", $"There are only {faculties.Count} faculties");

            Message res = TryAddingSeats(seatsNumber, faculties[facultyID]);
            if (res == Message.Success)
            {
                TotalSeatsCount += seatsNumber;
                faculties[facultyID].TotalSeats += seatsNumber;
            }
            return res;
        }
        public Message TryAddingSeats(int seatsNumber, string facultyName)
        {
            int facultyID = Faculties.IndexOf(facultyName);
            return TryAddingSeats(seatsNumber, facultyID);
        }
        private Message TryAddingSeats(int seatsNumber, Faculty faculty)
        {
            BudgetInputModel budgetmodel = new BudgetInputModel(); ;
            Array.Copy(UniversityBudget.Money, budgetmodel.Money, 4);
            int k = ConstructionInfrastructureAdmin.CountAffordableSeats(budgetmodel);

            if (seatsNumber > k)
                return new Message("Too much seats.", "You cannot afford that much new seats for your university.");

            Money moneyToPay = ConstructionInfrastructureAdmin.ServiceMoney + ConstructionInfrastructureAdmin.OneSeatMoney * seatsNumber;
            if (UniversityBudget.Money[(int)moneyToPay.Currency] >= moneyToPay)
            {
                UniversityBudget.Money[(int)moneyToPay.Currency] -= moneyToPay;
                moneyToPay.Amount = 0;
                return Message.Success;
            }
            else
            {
                moneyToPay.Amount -= UniversityBudget.Money[(int)moneyToPay.Currency].Amount;
                UniversityBudget.Money[(int)moneyToPay.Currency].Amount = 0;
                for (int i = 0; i < UniversityBudget.Money.Length; i++)
                {
                    if (UniversityBudget.Money[i] >= moneyToPay)
                    {
                        UniversityBudget.Money[i] -= moneyToPay;
                        moneyToPay.Amount = 0;
                        return Message.Success;
                    }
                    else
                    {
                        moneyToPay -= UniversityBudget.Money[i];
                        UniversityBudget.Money[i].Amount = 0;
                    }
                }

                if (moneyToPay.Amount > 0)
                {
                    throw new Exception("Something went wrong");
                }
                else
                {
                    return Message.Success;
                }
            }
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
        
        public void Update()// Starts calculating the applications and the process of graduation.
        {
            foreach (var fac in faculties)
            {
                fac.StartUpdate();
            }
        }

        private Message TryAddingStudent(int facultyID, StudentInputModel student)
        {
            Administration.SendEmailToStudent(student.Email, "Congrates! You have been accepted to the uiversity.");
            StudentModel studentModel = new StudentModel(student, ID, facultyID);
            Students.Add((faculties[facultyID], studentModel));
            return faculties[facultyID].AddStudent(studentModel);
        }
        public Message TryRemovingStudent(string passportID)
        {
            for (int i = 0; i < Students.Count; i++)
            {
                var student = Students[i];
                if (student.student.PassportID == passportID)
                {
                    Administration.SendEmailToStudent(student.student.Email, "You have been banned from the University.");
                    ApplyingAndNotStudents.Remove(student.student.PassportID);
                    StudentsSet.Remove(student.student.PassportID);

                    student.faculty.Students.Remove(student.student);
                    Students.RemoveAt(i);
                    return Message.Success;
                }
            }

            return new Message("Student not found!");
        }
        public List<StudentOutputModel> StudentsInformation()
        {
            var res = new List<StudentOutputModel>();

            foreach (var student in Students)
            {
                res.Add(GetStudentOutputModel(student.student));
            }
            return res;
        }
        public (Message message, List<StudentOutputModel> list) StudentsInformationByFaculty(int facultyID)
        {
            if (facultyID >= faculties.Count)
                return (new Message("Invalid Faculty ID.", "There is no a faculty with that ID"), new List<StudentOutputModel>());

            var faculty = faculties[facultyID];
            List<StudentOutputModel> res = new List<StudentOutputModel>();

            foreach (var student in faculty.Students)
            {
                res.Add(GetStudentOutputModel(student));
            }
            return (Message.Success, res);
        }

        public (Message message, List<StudentOutputModel> list) StudentsInformationByFaculty(string facultyName)
        {
            int facultyID = Faculties.IndexOf(facultyName);
            if (facultyID == -1)
                return (new Message("Invalid Faculty ID.", "There is no a faculty with that name."), new List<StudentOutputModel>());

            var faculty = faculties[facultyID];
            List<StudentOutputModel> res = new List<StudentOutputModel>();

            foreach (var student in faculty.Students)
            {
                res.Add(GetStudentOutputModel(student));
            }
            return (Message.Success, res);
        }


    }
}