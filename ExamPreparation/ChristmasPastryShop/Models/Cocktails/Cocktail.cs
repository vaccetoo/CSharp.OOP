using ChristmasPastryShop.Models.Cocktails.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private double price;


        protected Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
        }


        public string Name
        {
            get => name; 
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be null or whitespace!");
                }
                name = value;
            }
        }

        public string Size { get; private set; }

        public double Price
        {
            get => price; 
            private set
            {
                if (this.Size == "Small")
                {
                    value /= 3;
                }
                else if (this.Size == "Middle")
                {
                    value = (value / 3) * 2;
                }

                price = value;
            }
        }


        public override string ToString()
            => $"{Name} ({Size}) - {Price:f2} lv"
;
    }
}
