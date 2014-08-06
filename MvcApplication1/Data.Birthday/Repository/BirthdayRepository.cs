using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Birthday.RepositoryInterfaces;
using System.Data.Entity;
using System.Net.Mail;


namespace Data.Birthday.Repository
{
    public class BirthdayRepository : BirthdayInterface
    {
        private Domain.Birthday.Entities.BirthdateEntities birthdateContext;
       
        Domain.Birthday.Entities.Birthday birthDay = null;
        Domain.Birthday.Entities.ContactBirthdate  contactBirthDay = null;
        
        public BirthdayRepository()
        {
            birthdateContext = new Domain.Birthday.Entities.BirthdateEntities();
            birthDay = new Domain.Birthday.Entities.Birthday();
            contactBirthDay = new Domain.Birthday.Entities.ContactBirthdate();
        }

        public IEnumerable<Domain.Birthday.Entities.Birthday> GetBirthDates(int contactID)
        {
            IEnumerable<Domain.Birthday.Entities.Birthday> birthDates = (from p in birthdateContext.Birthdays
                                                                         join u in birthdateContext .ContactBirthdates 
                                                                         on p.UserID equals u.UserID
                                                                         where u.ContactID == contactID && p.IsActive==true
                                                                         select p).ToList();
           
            return birthDates;
        }

        public Domain.Birthday.Entities.Birthday GetUserByID(int userId)
        {
            Domain.Birthday.Entities.Birthday birthDay = new Domain.Birthday.Entities.Birthday();
            birthDay = (from p in birthdateContext.Birthdays where p.UserID ==userId select p).FirstOrDefault();
            return birthDay ;          
        }

        public Domain.Birthday.Entities.Birthday InsertUser(Domain.Birthday.Entities.Birthday user)
        {
            birthDay.IsActive = user.IsActive;
            birthDay.LatActivityDate = user.LatActivityDate;
            birthDay.Birthdate = user.Birthdate;
            birthDay.UserName = user.UserName;
            birthDay.Password = user.Password;
            birthdateContext.Birthdays.Add(birthDay);
            birthdateContext.SaveChanges();

            return birthDay;
        }

        public void InserContactBirthdate(Domain.Birthday.Entities.ContactBirthdate contact)
        {
            contactBirthDay.IsActive = contact.IsActive;
            contactBirthDay.LatActivityDate = contact.LatActivityDate;
            contactBirthDay.ContactID = contact.ContactID;
            contactBirthDay.UserID = contact.UserID;
            birthdateContext.ContactBirthdates.Add(contactBirthDay);
            birthdateContext.SaveChanges();

        }

        public void DeleteUser(int userID)
        {

            birthDay = (from p in birthdateContext.Birthdays where p.UserID == userID select p).FirstOrDefault();
            if (birthDay != null)
            {
                birthDay.IsActive = false;
               // birthdateContext.Birthdays.Remove(birthDay);
                birthdateContext.SaveChanges();
            }
        }

        public void UpdateUser(Domain.Birthday.Entities.Birthday user)
        {
            birthDay  = (from p in birthdateContext.Birthdays where p.UserID == user.UserID select p).FirstOrDefault();
            if (birthDay != null)
            {
                birthDay.Birthdate = user.Birthdate;
                birthDay.UserName = user.UserName;
                birthDay.Password = user.Password;
                birthDay.IsActive = true;
                birthdateContext.SaveChanges();
            }
        }

        public void Save()
        {
           
        }

        public void Dispose()
        {
            
        }
    }
}
