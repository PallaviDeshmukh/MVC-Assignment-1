using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Birthday.Entities;

namespace Data.Birthday.RepositoryInterfaces
{
    public interface BirthdayInterface:IDisposable
    {
        IEnumerable<Domain.Birthday.Entities.Birthday> GetBirthDates(int contactID);
        Domain.Birthday.Entities.Birthday GetUserByID(int userId);
        Domain.Birthday.Entities.Birthday InsertUser(Domain.Birthday.Entities.Birthday user);
        void DeleteUser(int userID);
        void UpdateUser(Domain.Birthday.Entities.Birthday user);
        void InserContactBirthdate(Domain.Birthday.Entities.ContactBirthdate contact);
        void Save();
    }
}
