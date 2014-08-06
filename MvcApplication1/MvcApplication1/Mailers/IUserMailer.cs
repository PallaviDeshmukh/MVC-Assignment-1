using Mvc.Mailer;

namespace MvcApplication1.Mailers
{ 
    public interface IUserMailer
    {
			MvcMailMessage Welcome();
			MvcMailMessage GoodBye();
			MvcMailMessage BirthdayReminder();
	}
}