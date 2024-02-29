using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Models.Interfaces;

namespace Telephony.Models
{
    public class Stationaryphone : ICallable
    {
        public string Call(string phoneNumber)
        {
            if (!IsPhoneNumberVallid(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Dialing... {phoneNumber}";
        }

        private bool IsPhoneNumberVallid(string phoneNumber) 
            => phoneNumber.All(c => Char.IsDigit(c));
    }
}
