using System.Data;

namespace University_Management_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            University YSU = new University
                ("Yerevan State University", 
                new DateTime(month: 5, day: 16, year: 1919), 
                new BudgetInputModel(new Money(30000, Currency.USD)), 10);
            YSU.setDomain("YSU");

            FacultyInputModel Faculty_0 = new FacultyInputModel
                ("Applied Mathematics", 
                3, 
                TimeSpan.FromSeconds(5), 
                Exam.Math | Exam.ComputerScience | Exam.Engineering);

            FacultyInputModel Faculty_1 = new FacultyInputModel
                ("Faculty of Mathematics",
                1,
                TimeSpan.FromSeconds(5),
                Exam.Math | Exam.Science);

            FacultyInputModel Faculty_2 = new FacultyInputModel
                ("Mechanics and Mathematics",
                1,
                TimeSpan.FromSeconds(5),
                Exam.Math | Exam.Engineering | Exam.Science
                );

            FacultyInputModel Faculty_3 = new FacultyInputModel
                ("Faculty of law",
                2,
                TimeSpan.FromSeconds(5),
                Exam.History | Exam.Politics | Exam.Linguistics);


            var f0 = YSU.TryAddingFaculty(Faculty_0);
            var f1 = YSU.TryAddingFaculty(Faculty_1);
            var f2 = YSU.TryAddingFaculty(Faculty_2);
            var f3 = YSU.TryAddingFaculty(Faculty_3);

            /******************************************************************************/

            StudentInputModel Student_1 = new StudentInputModel
                ("AP12345",
                "Adolf",
                "Hitler",
                "adolfhitler@nazi.com"
                );
            Student_1.SetExamGrade(Exam.Art, Grade.A);
            Student_1.SetExamGrade(Exam.Math, Grade.A);
            Student_1.SetExamGrade(Exam.Science, Grade.A);
            Student_1.SetExamGrade(Exam.Politics, Grade.A);

            StudentInputModel Student_2 = new StudentInputModel
                ("AP98765",
                "Winston",
                "Churchill",
                "winstonchurchill@gov.uk"
                );

            Student_2.SetExamGrade(Exam.Politics, Grade.A);
            Student_2.SetExamGrade(Exam.History, Grade.A);
            Student_2.SetExamGrade(Exam.Linguistics, Grade.C);

            StudentInputModel Student_3 = new StudentInputModel
                ("AR12345",
                "Donald",
                "Trump",
                "donaldtrump@yahoo.com"
                );
            Student_3.SetExamGrade(Exam.ComputerScience, Grade.B);
            Student_3.SetExamGrade(Exam.Literature, Grade.C);
            Student_3.SetExamGrade(Exam.Engineering, Grade.C);
            Student_3.SetExamGrade(Exam.Math, Grade.F);
            Student_3.SetExamGrade(Exam.Linguistics, Grade.F);

            StudentInputModel Student_4 = new StudentInputModel
                ("K1K2K3",
                "Kim",
                "Jong Un",
                "kimjongun@nuclear.com"
                );

            StudentInputModel Student_5 = new StudentInputModel
                ("RS11111",
                "Joseph",
                "Stalin",
                "josephstalin@ussr.ru"
                );
            Student_5.SetExamGrade(Exam.Math, Grade.B);
            Student_5.SetExamGrade(Exam.Science, Grade.B);

            StudentInputModel Student_6 = new StudentInputModel
                ("MHAYQ",
                "Tigranes II",
                "Artashesyan",
                "tigranesthegreat@hayq.am");
            Student_6.SetExamGrade(Exam.Math, Grade.A);
            Student_6.SetExamGrade(Exam.Science, Grade.A);
            Student_6.SetExamGrade(Exam.History, Grade.A);
            Student_6.SetExamGrade(Exam.Politics, Grade.A);
            Student_6.SetExamGrade(Exam.Art, Grade.A);
            Student_6.SetExamGrade(Exam.Linguistics, Grade.A);


            ApplicationResume Application_1 = new ApplicationResume
                (Student_1,
                new FacultyDescribtionTransferType
                    (1,
                    1)
                );

            ApplicationResume Application_2 = new ApplicationResume
                (Student_2,
                new FacultyDescribtionTransferType
                    (1,
                    3)
                );

            ApplicationResume Application_3 = new ApplicationResume
                (Student_3,
                new FacultyDescribtionTransferType
                    (1,
                    0)
                );

            ApplicationResume Application_4 = new ApplicationResume
                (Student_4,
                new FacultyDescribtionTransferType
                    (1,
                    6)
                );

            ApplicationResume Application_5 = new ApplicationResume
                (Student_5,
                new FacultyDescribtionTransferType
                    (1,
                    1)
                );

            ApplicationResume Application_6 = new ApplicationResume
                (Student_6,
                new FacultyDescribtionTransferType
                    (1,
                    3)
                );
            var res1 = YSU.TryGettingApplication(Application_1);
            var res2 = YSU.TryGettingApplication(Application_2);
            var res3 = YSU.TryGettingApplication(Application_3);
            var res4 = YSU.TryGettingApplication(Application_4);
            var res5 = YSU.TryGettingApplication(Application_5);
            var res6 = YSU.TryGettingApplication(Application_6);

            YSU.Update();//Start calculating the applications
            //look at the console
            var info = YSU.StudentsInformation();
            var info0 = YSU.StudentsInformationByFaculty("Applied Mathematics");
            var info1 = YSU.StudentsInformationByFaculty(1);
            var info2 = YSU.StudentsInformationByFaculty(2);
            var info3 = YSU.StudentsInformationByFaculty(3);
            var infoUndefined1 = YSU.StudentsInformationByFaculty(5);
            var infoUndefined2 = YSU.StudentsInformationByFaculty("Physics faculty");

            var Remove1 = YSU.TryRemovingStudent("K1K2K3");
            var Remove2 = YSU.TryRemovingStudent("AP98765");

            Thread.Sleep(5000);



            YSU.Update();//Starts the graduation


        }
    }
}
