using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private IRepository<ISubject> subjects;
        private IRepository<IStudent> students;
        private IRepository<IUniversity> universities;


        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }


        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName($"{firstName} {lastName}") != null)
            {
                return $"{firstName} {lastName} is already added in the repository.";
            }

            IStudent student = new Student(students.Models.Count + 1, firstName, lastName);

            students.AddModel(student);

            return $"Student {firstName} {lastName} is added to the {nameof(StudentRepository)}!";
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(EconomicalSubject) && 
                subjectType != nameof(HumanitySubject) &&
                subjectType != nameof(TechnicalSubject))
            {
                return $"Subject type {subjectType} is not available in the application!";
            }

            if (subjects.FindByName(subjectName) != null)
            {
                return $"{subjectName} is already added in the repository.";
            }

            ISubject subject = subjectType switch
            {
                "EconomicalSubject" => new EconomicalSubject(subjects.Models.Count + 1, subjectName),
                "HumanitySubject" => new HumanitySubject(subjects.Models.Count + 1, subjectName),
                "TechnicalSubject" => new TechnicalSubject(subjects.Models.Count + 1, subjectName),
            };

            subjects.AddModel(subject);

            return $"{subjectType} {subjectName} is created and added to the {nameof(SubjectRepository)}!";
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return $"{universityName} is already added in the repository.";
            }

            List<int> subjectsIds = new List<int>();

            foreach (string subject in requiredSubjects)
            {
                subjectsIds.Add(subjects.FindByName(subject).Id);
            }

            IUniversity university = new University(universities.Models.Count + 1, universityName, category, capacity, subjectsIds);

            universities.AddModel(university);

            return $"{universityName} university is created and added to the {nameof(UniversityRepository)}!";
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] studentFirstLastName = studentName.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string studentFirstName = studentFirstLastName[0];
            string studentLastName = studentFirstLastName[1];

            if (students.FindByName(studentName) == null)
            {
                return $"{studentFirstName} {studentLastName} is not registered in the application!";
            }

            if (universities.FindByName(universityName) == null)
            {
                return $"{universityName} is not registered in the application!";
            }

            IStudent currentStudent = students.FindByName(studentName);
            IUniversity currentUniversity = universities.FindByName(universityName);

            if (!currentUniversity.RequiredSubjects.All(x => currentStudent.CoveredExams.Any(e => e == x)))
            {
                return $"{studentName} has not covered all the required exams for {universityName} university!";
            }

            if (currentStudent.University != null && currentStudent.University.Name == universityName)
            {
                return $"{studentFirstName} {studentLastName} has already joined {universityName}.";
            }

            currentStudent.JoinUniversity(currentUniversity);

            return $"{studentFirstName} {studentLastName} joined {universityName} university!";
        }

        public string TakeExam(int studentId, int subjectId)
        {
            if (students.FindById(studentId) == null)
            {
                return "Invalid student ID!";
            }

            if (subjects.FindById(subjectId) == null)
            {
                return "Invalid subject ID!";
            }

            IStudent currentStudent = students.FindById(studentId);

            if (currentStudent.CoveredExams.Contains(subjectId))
            {
                return $"{currentStudent.FirstName} {currentStudent.LastName} has already covered exam of {subjects.FindById(subjectId).Name}.";
            }

            currentStudent.CoverExam(subjects.FindById(subjectId));

            return $"{currentStudent.FirstName} {currentStudent.LastName} covered {subjects.FindById(subjectId).Name} exam!"
;
        }

        public string UniversityReport(int universityId)
        {
            IUniversity currentUniversity = universities.FindById(universityId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"*** {currentUniversity.Name} ***");
            sb.AppendLine($"Profile: {currentUniversity.Category}");

            int studentsCount = 0;

            foreach (IStudent student in students.Models)
            {
                if (student.University == currentUniversity)
                {
                    studentsCount++;
                }
            }

            sb.AppendLine($"Students admitted: {studentsCount}");
            sb.AppendLine($"University vacancy: {currentUniversity.Capacity - studentsCount}");

            return sb.ToString().TrimEnd();

        }
    }
}
