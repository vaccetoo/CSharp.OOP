using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private List<IUser> users;


        public UserRepository()
        {
            users = new List<IUser>();
        }


        public void AddModel(IUser model)
        {
            users.Add(model);
        }

        public IUser FindById(string identifier)
            => users.FirstOrDefault(u => u.DrivingLicenseNumber == identifier);

        public IReadOnlyCollection<IUser> GetAll()
            => users;

        public bool RemoveById(string identifier)
            => users.Remove(users.First(u => u.DrivingLicenseNumber == identifier));
    }
}
