using System;
using System.Collections.Generic;
using System.Linq;
using TimeTrackerApp.Models;

namespace TimeTrackerApp.Helpers
{
    // Activity Builder Object
  public class ActivityBuilder
  {
      // Activity DB Helper
    private ActivityHelper ActivityHelper { get; set; }

      // Current User Data
    private PersistenceData PersistenceData { get; set; }

      // Calendar Date Type Entity
    private CalendarDate Date { get; set; }

      // Description Of Activity Type Entity
    private DescriptionOfActivity Category { get; set; }

      // Time Of Activity Type Entity
    private TimeOfActivity TimeSpent { get; set; }

      // Calendar Dates Collection
    private List<CalendarDate> Dates { get; set; }

      // Description Of Activities Collection
    private List<DescriptionOfActivity> Categories { get; set; }

      // Time Of Activity Collection
    private List<TimeOfActivity> TimeList { get; set; }

      // Database Helper
    private DbHelper DbHelper { get; set; }

    public ActivityBuilder()
    {
      DbHelper = new DbHelper();
      Date = new CalendarDate();
      Category = new DescriptionOfActivity();
      ActivityHelper = new ActivityHelper();
      PersistenceData = PersistenceData.Instance;
      Dates = DbHelper.CalendarDates();
      Categories = DbHelper.Activities();
      TimeList = DbHelper.TimeOfActivities();
    }

      // Set selected date
      // returns: found
    public bool SetDate(int date)
    {
      Date = Dates.FirstOrDefault(d => d.Id == date);
      ;
      return Date != null;
    }

      // Set selected category
      // returns: found
    public bool SetCategory(int category)
    {
      Category = Categories.FirstOrDefault(c => c.Id == category);
      return Category != null;
    }

      // Set selected time spent on activity
      // returns: found
    public bool SetTimeSpent(int timeSpent)
    {
      TimeSpent = TimeList.FirstOrDefault(s => s.Id == timeSpent);
      return TimeSpent != null;
    }

      // Print List of Categories available in database
    public void PrintCategoryOptions()
    {
      Categories.ForEach(c => Console.WriteLine($"[{c.Id}] " + $"{c.ActivityDescription}"));
    }

      // Print list of dates available in database
    public void PrintDateOptions()
    {
      Dates.ForEach(d => Console.WriteLine($"[{d.Id}] {d.FormattedDate()}"));
    }

      // Print list of time spent options available in database
    public void PrintTimeOptions()
    {
      TimeList.ForEach(toa => Console.WriteLine($"[{toa.Id}] " + $"{toa.TimeSpentOnActivity}"));
    }

      // Build and save new activity record
    public int Build()
    {
      if (Category != null || Date != null || TimeSpent != null)
      {
        return 0;
      }
      return ActivityHelper.LogActivity(Category, Date, TimeSpent);
    }
  }
}