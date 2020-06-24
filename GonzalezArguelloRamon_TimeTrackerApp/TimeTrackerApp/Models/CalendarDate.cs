using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerApp.Models
{
    // Calendar Date Entity
  public class CalendarDate
  {
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public CalendarDate()
    {
      Id = default(int);
      Date = default(DateTime);
    }

    public string FormattedDate()
    {
      return Date.ToString("dd MMMM yyyy");
    }

    public int CalendarDay()
    {
      return Date.Day;
    }

    public int Weekday()
    {
      var weekday = 0;

      switch (Date.DayOfWeek)
      {
        case DayOfWeek.Monday:
          weekday = 1;
          break;
        case DayOfWeek.Tuesday:
          weekday = 2;
          break;
        case DayOfWeek.Wednesday:
          weekday = 3;
          break;
        case DayOfWeek.Thursday:
          weekday = 4;
          break;
        case DayOfWeek.Friday:
          weekday = 5;
          break;
        case DayOfWeek.Saturday:
          weekday = 6;
          break;
        case DayOfWeek.Sunday:
          weekday = 7;
          break;
        default:
          weekday = 0;
          break;
      }

      return weekday;
    }
  }
}