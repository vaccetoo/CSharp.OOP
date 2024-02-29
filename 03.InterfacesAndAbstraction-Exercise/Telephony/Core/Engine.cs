using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Core.Interfaces;
using Telephony.Models;
using Telephony.Models.Interfaces;

namespace Telephony.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            string[] inputPhoneNumbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] inputUrls = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            ICallable callable = new Stationaryphone();

            foreach (var phoneNumber in inputPhoneNumbers)
            {
                try
                {
                    if (phoneNumber.Length == 7)
                    {
                        callable = new Stationaryphone();
                    }
                    else if (phoneNumber.Length == 10)
                    {
                        callable = new Smartphone();
                    }

                    Console.WriteLine(callable.Call(phoneNumber));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            IBrowsable browsable = new Smartphone();

            foreach (var url in inputUrls)
            {
                try
                {
                    Console.WriteLine(browsable.Browse(url));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
