using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCompetition.Models
{
    public class HumanitySubject : Subject
    {
        private const double InitialRate = 1.15;


        public HumanitySubject(int subjectId, string subjectName) 
            : base(subjectId, subjectName, InitialRate)
        {
        }
    }
}
