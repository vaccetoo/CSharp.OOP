using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Tomcat : Cat
    {
        private const string TomacatGender = "Male";
        public Tomcat(string name, int age) 
            : base(name, age, TomacatGender)
        {
        }

        public override string ProduceSound() => "MEOW";
    }
}
