using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int capacity;
        private IRepository<IDelicacy> delicacies;
        private IRepository<ICocktail> cocktails;
        private double currentBill;
        private double turnOver;
        private bool isReserved;


        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;

            delicacies = new DelicacyRepository();
            cocktails = new CocktailRepository();

            currentBill = 0;
            turnOver = 0;
            isReserved = false;
        }


        public int BoothId { get; private set; }

        public int Capacity
        {
            get => capacity; 
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Capacity has to be greater than 0!");
                }
                capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu 
            => delicacies;

        public IRepository<ICocktail> CocktailMenu 
            => cocktails;

        public double CurrentBill 
            => currentBill;

        public double Turnover
            => turnOver;

        public bool IsReserved
        {
            get => isReserved;
            private set => isReserved = value;
        }

        public void ChangeStatus()
            => IsReserved = !IsReserved;

        public void Charge()
        {
            turnOver += currentBill;
            currentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
            => currentBill += amount;


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");

            foreach (var coctail in cocktails.Models)
            {
                sb.AppendLine(coctail.ToString());
            }

            sb.AppendLine($"-Delicacy menu:");

            foreach (var delicacy in delicacies.Models)
            {
                sb.AppendLine(delicacy.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
