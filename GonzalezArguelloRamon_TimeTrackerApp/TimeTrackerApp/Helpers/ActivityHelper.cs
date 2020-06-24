using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackerApp.Models;

namespace TimeTrackerApp.Helpers
{
    // Activity Record Helper - Add new activity records to the database
  public class ActivityHelper
  {
    private DbHelper DbHelper { get; set; }
    private PersistenceData _persistenceData = PersistenceData.Instance;

    public ActivityHelper()
    {
      DbHelper = new DbHelper();
    }
      // Record new activity
    public int LogActivity(DescriptionOfActivity activity, CalendarDate date, TimeOfActivity time)
    {
      var user = _persistenceData.ActiveUser;

      var query = "INSERT INTO activity_log " +
                  "( user_id, calendar_day, calendar_date, day_name, " +
                  "category_description, activity_description, " +
                  "time_spent_on_activity ) " +
                  $"VALUES ({user.Id}, {date.CalendarDay()}, {date.Id}, " +
                  $"{date.Weekday()}, {activity.GetCategoryId()}, " +
                  $"{activity.Id}, {time.Id})";

        // print query to the debugger console
      System.Diagnostics.Debug.WriteLine($"query: {query}");
      return DbHelper.Insert(query);
    }
  }
}