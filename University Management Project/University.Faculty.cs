using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Management_Project
{
    internal partial class University
    {
        private class Faculty
        {
            public readonly University University;
            public readonly int ID;
            public readonly string Name;
            public TimeSpan CourseTime;
            public int TotalSeats;
            public int FreeSeats => TotalSeats - Students.Count;
            public List<StudentModel> Students;
            public Exam RequiredExams;

            public List<ApplicationResume> Applications;

            public Faculty(University university, int id, string name, TimeSpan courseTime, int totalSeats, Exam requiredExams)
            {
                ID = id;
                Name = name;
                CourseTime = courseTime;
                TotalSeats = totalSeats;
                RequiredExams = requiredExams;
                Students = new List<StudentModel>();
                University = university;
                Applications = new List<ApplicationResume>();
            }

            public Message AddApplication(ApplicationResume resume)
            {
                int s = University.ApplyingAndNotStudents.Count;
                University.ApplyingAndNotStudents.Add(resume.Student.PassportID);

                if (s == University.ApplyingAndNotStudents.Count)
                {
                    return new Message("Invalid Passport ID.", "The student with that passport id has already applied for the university.");
                }

                Applications.Add(resume);

                return Message.Success;
            }

            private Grade CountAvarageAdmissionExamGrade(StudentInputModel student)
            {
                int c = 0;
                decimal gr = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (RequiredExams.HasFlag((Exam)(1 << i)))
                    {
                        gr += (int)student.AdmissionExamGrades[i];
                        c++;
                    }
                }

                gr /= c;
                return (Grade)(int)Math.Round(gr);
            }

            public void StartUpdate()
            {
                Graduation();
                Admit();
            }

            private void Graduation()
            {
                DateTime now = DateTime.Now;
                for (int i = 0; i < Students.Count; i++)
                {
                    var student = Students[i];
                    if (student.AdmissionDate + CourseTime >= now)
                    {
                        Graduate(ref i);
                    }
                }
            }

            private void Admit()
            {
                List<(Grade g, ApplicationResume resume)> localList;

                localList = new List<(Grade g, ApplicationResume resume)>();
                foreach (var resume in Applications)
                {
                    Grade g = CountAvarageAdmissionExamGrade(resume.Student);
                    if (g >= Grade.C)
                    {
                        localList.Add((g, resume));
                    }
                    else
                    {
                        University.Administration.SendEmailToStudent(resume.Student.PassportID, "Your application has been rejected");
                    }
                }

                localList.Sort((a, b) => a.g.CompareTo(b.g));

                for (int i = localList.Count - 1; i >= 0; i--)
                {
                    if (FreeSeats == 0)
                    {
                        University.Administration.SendEmailToStudent(localList[i].resume.Student.PassportID, "Your application has been rejected");
                    }
                    else
                    {
                        University.AddStudent(ID, localList[i].resume.Student);
                    }
                }

                Applications.Clear();
            }

            private void Graduate(ref int i)
            {
                var student = Students[i];
                University.ApplyingAndNotStudents.Remove(student.PassportID);
                University.Students.Remove((this, student));

                Students.RemoveAt(i);
                i--;
            }

            public Message AddStudent(StudentModel st)
            {
                int s = University.ApplyingAndNotStudents.Count;
                University.ApplyingAndNotStudents.Add(st.PassportID);

                if (s == University.ApplyingAndNotStudents.Count)
                {
                    return new Message("Invalid Passport ID.", "The student with that passport id has already applied for the university.");
                }
                Students.Add(st);
                return Message.Success;
            }

        }
    }
}
