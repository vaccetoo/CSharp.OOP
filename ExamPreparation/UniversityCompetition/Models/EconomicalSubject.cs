using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityCompetition.Models
{
    public class EconomicalSubject : Subject
    {
        private const double InitialRate = 1.0;


        public EconomicalSubject(int subjectId, string subjectName) 
            : base(subjectId, subjectName, InitialRate)
        {
        }
    }
}
