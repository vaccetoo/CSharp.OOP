using ChristmasPastryShop.Models.Delicacies.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy : IDelicacy
    {
        private string name;


        protected Delicacy(string delicacyName, double price)
        {
            Name = delicacyName;
            Price = price;
        }


        public string Name
        {
            get => name; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                name = value;
            }
        }

        public double Price { get; private set; }


        public override string ToString()
            => $"{Name} - {Price:f2} lv";
    }
}
