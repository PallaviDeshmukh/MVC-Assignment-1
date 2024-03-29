using Mvc.Mailer;

namespace MvcApplication1.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage Welcome()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "Welcome";
				x.ViewName = "Welcome";
				x.To.Add("some-email@example.com");
			});
		}
 
		public virtual MvcMailMessage GoodBye()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "GoodBye";
				x.ViewName = "GoodBye";
				x.To.Add("some-email@example.com");
			});
		}
 
		public virtual MvcMailMessage BirthdayReminder()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "Birthday Reminder";
				x.ViewName = "BirthdayReminder ";
				x.To.Add("testmvcassign@example.com");
			});
		}
 	}
}