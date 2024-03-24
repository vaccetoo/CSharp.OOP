using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity;
        private List<ILoan> loans;
        private List<IClient> clients;


        protected Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;

            loans = new List<ILoan>();
            clients = new List<IClient>();
        }


        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set
            {
                capacity = value;
            }
        }

        public IReadOnlyCollection<ILoan> Loans
            => loans;

        public IReadOnlyCollection<IClient> Clients
            => clients;


        public void AddClient(IClient Client)
        {
            if (Capacity > clients.Count)
            {
                clients.Add(Client);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }
        }

        public void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {Name}, Type: {GetType().Name}");
            sb.Append("Clients: ");
            if (clients.Count > 0)
            {
                for (int i = 0; i < clients.Count; i++)
                {
                    sb.Append(clients[i].Name);
                    if (i < clients.Count - 1)
                        sb.Append(", ");
                }
            }
            else
            {
                sb.Append("none");
            }
            sb.AppendLine();
            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");

            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient Client)
        {
            clients.Remove(Client);
        }

        public double SumRates()
        {
            int loasnSum = 0;

            foreach (ILoan loan in loans)
            {
                loasnSum += loan.InterestRate;
            }

            return loasnSum;
        }
    }
}
