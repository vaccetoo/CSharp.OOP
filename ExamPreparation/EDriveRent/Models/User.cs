using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private double rating;
        private string drivingLicenseNumber;
        private bool isBlocked;

        public User(string firstName, string lastName, string drivingLicenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenseNumber;

            rating = 0;
            isBlocked = false;
        }


        public string FirstName
        {
            get => firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.FirstNameNull);
                }

                firstName = value;
            }
        }

        public string LastName
        {
            get => lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LastNameNull);
                }
                lastName = value;
            }
        }

        public double Rating
            => rating;

        public string DrivingLicenseNumber
        {
            get => drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }

                drivingLicenseNumber = value;
            }
        }

        public bool IsBlocked
            => isBlocked;


        public void DecreaseRating()
        {
            if (this.rating < 2)
            {
                this.rating = 0;
                this.isBlocked = true;
            }
            else
            {
                this.rating -= 2;
            }
        }

        public void IncreaseRating()
        {
            if (this.rating < 10)
            {
                this.rating += 0.5;
            }
        }


        public override string ToString()
            => $"{FirstName} {LastName} Driving license: {drivingLicenseNumber} Rating: {rating}";
    }
}
