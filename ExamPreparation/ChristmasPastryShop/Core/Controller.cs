using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;

        public Controller()
        {
            booths = new BoothRepository();
        }

        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;

            IBooth booth = new Booth(boothId, capacity);

            booths.AddModel(booth);

            return $"Added booth number {boothId} with capacity {capacity} in the pastry shop!";
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != nameof(Hibernation) && cocktailTypeName != nameof(MulledWine))
            {
                return $"Cocktail type {cocktailTypeName} is not supported in our application!";
            }

            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return $"{size} is not recognized as valid cocktail size!";
            }

            IBooth currentBooth = booths.Models.First(b => b.BoothId == boothId);

            if (currentBooth.CocktailMenu.Models.Any(c => c.Name == cocktailName && c.Size == size))
            {
                return $"{size} {cocktailName} is already added in the pastry shop!";
            }

            ICocktail cocktail = cocktailTypeName switch
            {
                "Hibernation" => new Hibernation(cocktailName, size),
                "MulledWine" => new MulledWine(cocktailName, size),
            };

            currentBooth.CocktailMenu.AddModel(cocktail);

            return $"{size} {cocktailName} {cocktailTypeName} added to the pastry shop!";
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != nameof(Gingerbread) && delicacyTypeName != nameof(Stolen))
            {
                return $"Delicacy type {delicacyTypeName} is not supported in our application!";
            }

            IBooth currentBooth = booths.Models.First(b => b.BoothId == boothId);

            if (currentBooth.DelicacyMenu.Models.Any(d => d.Name == delicacyName))
            {
                return $"{delicacyName} is already added in the pastry shop!";
            }

            IDelicacy delicacy = delicacyTypeName switch
            {
                "Gingerbread" => new Gingerbread(delicacyName),
                "Stolen" => new Stolen(delicacyName),
            };

            currentBooth.DelicacyMenu.AddModel(delicacy);

            return $"{delicacyTypeName} {delicacyName} added to the pastry shop!";

        }

        public string BoothReport(int boothId)
        {
            IBooth booth = booths.Models.First(b => b.BoothId == boothId);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Booth: {boothId}");
            sb.AppendLine($"Capacity: {booth.Capacity}");
            sb.AppendLine($"Turnover: {booth.Turnover:f2} lv");

            sb.AppendLine("-Cocktail menu:");
            foreach (var coctail in booth.CocktailMenu.Models)
            {
                sb.AppendLine($"--{coctail.ToString()}");
            }

            sb.AppendLine("-Delicacy menu:");
            foreach (var delicacy in booth.DelicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.First(b => b.BoothId == boothId);

            booth.Charge();

            booth.ChangeStatus();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Bill {booth.Turnover:f2} lv");
            sb.AppendLine($"Booth {boothId} is now available!");

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            IBooth booth = booths.Models
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .FirstOrDefault(b => b.IsReserved == false && b.Capacity >= countOfPeople);

            if (booth == null)
            {
                return $"No available booth for {countOfPeople} people!";
            }

            booth.ChangeStatus();

            return $"Booth {booth.BoothId} has been reserved for {countOfPeople} people!";
        }

        public string TryOrder(int boothId, string order)
        {
            // TryOrder 1 MulledWine/Redstar/2/Middle

            string[] orderInfo = order.Split('/');

            string itemTypeName = orderInfo[0];
            string itemName = orderInfo[1];
            int orderCount = int.Parse(orderInfo[2]);

            // The fourth will exist only if the item is an ICocktail. The element (if such exists) will be the size of the Cocktail

            IBooth booth = booths.Models.First(b => b.BoothId == boothId);

            if (itemTypeName != nameof(MulledWine) &&
                itemTypeName != nameof(Hibernation) &&
                itemTypeName != nameof(Gingerbread) &&
                itemTypeName != nameof(Stolen))
            {
                return $"{itemTypeName} is not recognized type!";
            }

            if (itemTypeName == nameof(MulledWine) || itemTypeName == nameof(Hibernation))
            {
                if (!booth.CocktailMenu.Models.Any(c => c.Name == itemName))
                {
                    return $"There is no {itemTypeName} {itemName} available!"
;
                }
            }
            else if (itemTypeName == nameof(Gingerbread) || itemTypeName == nameof(Stolen))
            {
                if (!booth.DelicacyMenu.Models.Any(c => c.Name == itemName))
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }
            }

            if (itemTypeName == nameof(MulledWine) || itemTypeName == nameof(Hibernation))
            {
                string size = orderInfo[3];

                if (size != "Small" && size != "Middle" && size != "Large")
                {
                    return $"There is no {size} {itemName} available!";
                }

                ICocktail cocktail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName && c.Size == size);

                if (cocktail == null)
                {
                    return $"There is no {size} {itemName} available!";
                }

                double amount = orderCount * cocktail.Price;

                booth.UpdateCurrentBill(amount);

                return $"Booth {boothId} ordered {orderCount} {itemName}!";
            }
            else
            {
                if (!booth.DelicacyMenu.Models.Any(d => d.Name == itemName))
                {
                    return $"There is no {itemTypeName} {itemName} available!";
                }

                IDelicacy delicacy = booth.DelicacyMenu.Models.First(d => d.Name == itemName);

                double amount = orderCount * delicacy.Price;

                booth.UpdateCurrentBill(amount);

                return $"Booth {boothId} ordered {orderCount} {itemName}!";
            }
        }
    }
}
