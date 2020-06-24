using System.Linq;
using TimeTrackerApp.Models;

namespace TimeTrackerApp.Helpers
{
  public class LoginHelper
  {
    public LoginHelper()
    {
      Db = new DbHelper();
    }

    public DbHelper Db { get; set; }

    public User IsValid(string firstName, string lastName, string password)
    {
      var users = Db.Users()
          .Where(x => x.FirstName == firstName && x.LastName == lastName && x.Password == password);
      return users.ToList().FirstOrDefault();
    }
  }
}