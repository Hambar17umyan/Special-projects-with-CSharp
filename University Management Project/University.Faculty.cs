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

            private decimal CountAvarageAdmissionExamGrade(StudentInputModel student)
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
                return gr;
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
                    if (now - student.AdmissionDate >= CourseTime)
                    {
                        Graduate(ref i);
                    }
                }
            }

            private void Admit()
            {
                List<(decimal g, ApplicationResume resume)> localList;

                localList = new List<(decimal, ApplicationResume)>();
                foreach (var resume in Applications)
                {
                    decimal g = CountAvarageAdmissionExamGrade(resume.Student);
                    if (g >= (int)Grade.C)
                    {
                        localList.Add((g, resume));
                    }
                    else
                    {
                        University.Administration.SendEmailToStudent(resume.Student.Email, "Your application has been rejected");
                    }
                }

                localList.Sort((a, b) =>
                {
                    int result = a.g.CompareTo(b.g);
                    if (result == 0)
                    {
                        result = b.resume.ApplicationDate.CompareTo(a.resume.ApplicationDate);
                    }
                    return result;
                });

                for (int i = localList.Count - 1; i >= 0; i--)
                {
                    if (FreeSeats == 0)
                    {
                        University.Administration.SendEmailToStudent(localList[i].resume.Student.Email, "Your application has been rejected");
                    }
                    else
                    {
                        University.TryAddingStudent(ID, localList[i].resume.Student);
                    }
                }

                Applications.Clear();
            }

            private void Graduate(ref int i)
            {
                var student = Students[i];
                University.Administration.SendEmailToStudent(student.Email, $"Congrates! You have graduated from {University.Name}.");
                University.ApplyingAndNotStudents.Remove(student.PassportID);
                University.Students.Remove((this, student));
                University.StudentsSet.Remove(student.PassportID);

                Students.RemoveAt(i);
                i--;
            }

            public Message AddStudent(StudentModel st)
            {
                int s = University.StudentsSet.Count;
                University.StudentsSet.Add(st.PassportID);

                if (s == University.StudentsSet.Count)
                {
                    return new Message("Invalid Passport ID.", "The student with that passport id has already applied for the university.");
                }
                Students.Add(st);
                return Message.Success;
            }

        }
    }
}
