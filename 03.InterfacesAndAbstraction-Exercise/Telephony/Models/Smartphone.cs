using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Models.Interfaces;

namespace Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Browse(string url)
        {
            if (!IsUrlVallid(url))
            {
                throw new ArgumentException("Invalid URL!");
            }

            return $"Browsing: {url}!";
        }

        public string Call(string phoneNumber)
        {
            if (!IsPhoneNumberVallid(phoneNumber))
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Calling... {phoneNumber}";
        }

        private bool IsPhoneNumberVallid(string phoneNumber)
            => phoneNumber.All(c => Char.IsDigit(c));

        private bool IsUrlVallid(string url)
            => url.All(c => !Char.IsDigit(c));
    }
}
