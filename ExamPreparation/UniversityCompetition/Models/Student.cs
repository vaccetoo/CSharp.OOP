﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class Student : IStudent
    {
        private string firstName;
        private string lastName;
        private List<int> coveredExams;
        private IUniversity studentUni;


        public Student(int studentId, string firstName, string lastName)
        {
            Id = studentId;
            FirstName = firstName;
            LastName = lastName;

            coveredExams = new List<int>();
        }


        public int Id { get; private set; }

        public string FirstName
        {
            get => firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.NameNullOrWhitespace);
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                lastName = value;
            }
        }

        public IReadOnlyCollection<int> CoveredExams
            => coveredExams;

        public IUniversity University { get => studentUni; private set => studentUni = value; }

        public void CoverExam(ISubject subject)
            => coveredExams.Add(subject.Id);

        public void JoinUniversity(IUniversity university)
            => University = university;
    }
}
