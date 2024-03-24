using BankLoan.Core.Contracts;
using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Repositories.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Core
{
    public class Controller : IController
    {
        private IRepository<ILoan> loanRepository;
        private IRepository<IBank> bankRepository;


        public Controller()
        {
            loanRepository = new LoanRepository();
            bankRepository = new BankRepository();
        }


        public string AddBank(string bankTypeName, string name)
        {
            Type type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == bankTypeName);

            if (type == null)
            {
                throw new ArgumentException(ExceptionMessages.BankTypeInvalid);
            }

            IBank bank = Activator.CreateInstance(type, name) as IBank;
            bankRepository.AddModel(bank);

            return $"{bankTypeName} is successfully added.";
        }

        public string AddClient(string bankName, string clientTypeName, string clientName, string id, double income)
        {
            // Student => BranchBank
            // Adult => CentralBank

            Type clientType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == clientTypeName);

            if (clientType == null)
            {
                throw new ArgumentException(ExceptionMessages.ClientTypeInvalid);
            }

            IClient client = Activator.CreateInstance(clientType, clientName, id, income) as IClient;
            IBank bank = bankRepository.FirstModel(bankName);

            if (client.GetType().Name == "Student" && bank.GetType().Name != "BranchBank" ||
                client.GetType().Name == "Adult" && bank.GetType().Name != "CentralBank")
            {
                return "Unsuitable bank.";
            }

            bank.AddClient(client);

            return $"{clientTypeName} successfully added to {bankName}.";

        }

        public string AddLoan(string loanTypeName)
        {
            Type type = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == loanTypeName);

            if (type == null)
            {
                throw new ArgumentException(ExceptionMessages.LoanTypeInvalid);
            }

            ILoan loan = Activator.CreateInstance(type) as ILoan;

            loanRepository.AddModel(loan);

            return $"{loanTypeName} is successfully added.";
        }

        public string FinalCalculation(string bankName)
        {
            IBank bank = bankRepository.FirstModel(bankName);

            double clientsIncome = bank.Clients.Sum(c => c.Income);
            double loanIncomes = bank.Loans.Sum(l => l.Amount);

            double finalIncome = clientsIncome + loanIncomes;

            return $"The funds of bank {bankName} are {finalIncome:f2}.";
        }

        public string ReturnLoan(string bankName, string loanTypeName)
        {
            IBank bank = bankRepository.FirstModel(bankName);

            if (!loanRepository.Models.Any(l => l.GetType().Name == loanTypeName))
            {
                throw new ArgumentException($"Loan of type {loanTypeName} is missing.");
            }

            ILoan loan = loanRepository.FirstModel(loanTypeName);

            bank.AddLoan(loan);

            loanRepository.RemoveModel(loan);

            return $"{loanTypeName} successfully added to {bankName}.";
        }

        public string Statistics()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var bank in bankRepository.Models)
            {
                sb.AppendLine(bank.GetStatistics());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
