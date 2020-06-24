using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeTrackerApp.Helpers;

namespace TimeTrackerApp.Screens
{
    // Calculations Screen
  public static class Calculations
  {
    private static readonly DbHelper DbHelper = new DbHelper();
    private static PersistenceData PersistenceData = PersistenceData.Instance;
    private static readonly int totalHoursIn26Days = 624;

      // Calculations Scree banner
    public static void Header()
    {
      Console.Clear();
      Shared.Header();

        // available options
      Console.WriteLine("Calculations \n");
      Console.WriteLine("[1] Time Spent Working Out              [5] Time Spent Sleep");
      Console.WriteLine("[2] Percentage of time Working Out      [6] Percentage of time Sleeping");
      Console.WriteLine("[3] Time Spent Class                    [7] Time Spent Relaxation");
      Console.WriteLine("[4] Percentage of time Class            [8] Percentage of time Relaxation\n");
      Console.WriteLine("[0] Main Menu");

      WriterHelper.RedText("=", 119);
    }

      // Main View for Calculations Screen
    public static void View()
    {
      var selection = default(int);

        // print calculations screen
      Header();

        // check for temp message if present print it to the screen
      if (!string.IsNullOrEmpty(PersistenceData.TempResult))
      {
          Console.WriteLine($"{PersistenceData.TempResult}");
          WriterHelper.RedText("=", 119);
      }

        // clear temporary message from persistence data
      PersistenceData.TempResult = String.Empty;

        // ask for new input
      Console.Write("Selection: ");
      selection = Convert.ToInt32(Console.ReadLine());
      CalculationMenu(selection);
    }

      // Calculations Screen Data Processing
    private static void CalculationMenu(int selection)
    {
        // active user
      var user = PersistenceData.ActiveUser;

        // get all process data from database
      var viewBy = DbHelper.ViewBy();

      switch (selection)
      {
        case 1:
          var summary = viewBy
              .Where(s => s.ActivityCategoryId == 1) // category 1 = Working Out
              .Where(k => k.UserId == user.Id) // filter by current user id
              .GroupBy(s => s.ActivityCategoryId)
              .Select(a => new {TimeSpent = a.Sum(b => b.TimeSpentOnActivity)});
          PersistenceData.TempResult =
              $"[{selection}] Total time spent working out in 26 days: {(summary.Any() ? summary.First().TimeSpent : 0)} hours";
          break;
        case 2:
          var summary2 = viewBy
              .Where(s => s.ActivityCategoryId == 1) // category 1 = Working Out
              .Where(k => k.UserId == user.Id) // filter by current user id
              .GroupBy(s => s.ActivityCategoryId)
              .Select(a => new {TimeSpent = a.Sum(b => b.TimeSpentOnActivity)});

          PersistenceData.TempResult =
              $"[{selection}] The percentage of time spent working out in 26 days: {(summary2.Any() ? (summary2.First().TimeSpent / totalHoursIn26Days) : 0):P}";
          break;
        case 3:
          var summary3 = viewBy
              .Where(s => s.ActivityCategoryId == 3 || s.ActivityCategoryId == 4) // category class 1 and 2
              .Where(k => k.UserId == user.Id) // filter by user id
              .GroupBy(s => s.ActivityCategoryId)
              .Select(a => new {TimeSpent = a.Sum(b => b.TimeSpentOnActivity)});
          PersistenceData.TempResult =
              $"[{selection}] Total time spent on classes in 26 days: {(summary3.Any() ? summary3.First().TimeSpent : 0)} hours";
          break;
        case 4:
          var summary4 = viewBy
              .Where(s => s.ActivityCategoryId == 3 || s.ActivityCategoryId == 4) // category class 1 and 2
              .Where(k => k.UserId == user.Id) // filter by user id
              .GroupBy(s => s.ActivityCategoryId)
              .Select(a => new {TimeSpent = a.Sum(b => b.TimeSpentOnActivity)});
          PersistenceData.TempResult =
              $"[{selection}] The percentage of time spent on classes in 26 days: {(summary4.Any() ? summary4.First().TimeSpent / totalHoursIn26Days : 0):P}";
          break;
        case 5:
          var summary5 = viewBy
              .Where(s => s.ActivityCategoryId == 5) // category sleep
              .Where(k => k.UserId == user.Id)
              .GroupBy(s => s.ActivityCategoryId)
              .Select(a => new {TimeSpent = a.Sum(b => b.TimeSpentOnActivity)});
          PersistenceData.TempResult =
              $"[{selection}] Total time spent sleeping in 26 days: {(summary5.Any() ? summary5.First().TimeSpent : 0)} hours";
          break;
        case 6:
          var summary6 = viewBy
              .Where(s => s.ActivityCategoryId == 5) // category sleep
              .Where(k => k.UserId == user.Id)
              .GroupBy(s => s.ActivityCategoryId)
              .Select(a => new {TimeSpent = a.Sum(b => b.TimeSpentOnActivity)});
          PersistenceData.TempResult =
              $"[{selection}] The percentage of time spent sleeping in 26 days: {(summary6.Any() ? summary6.First().TimeSpent / totalHoursIn26Days : 0):P}";
          break;
        case 7:
          var summary7 = viewBy
              .Where(s => s.ActivityCategoryId == 6) // category relaxation
              .Where(k => k.UserId == user.Id)
              .GroupBy(s => s.ActivityCategoryId)
              .Select(a => new {TimeSpent = a.Sum(b => b.TimeSpentOnActivity)});
          PersistenceData.TempResult =
              $"[{selection}] Total time spent relaxing in 26 days: {(summary7.Any() ? summary7.First().TimeSpent : 0)} hours";
          break;
        case 8:
          var summary8 = viewBy
              .Where(s => s.ActivityCategoryId == 5) // category sleep
              .Where(k => k.UserId == user.Id)
              .GroupBy(s => s.ActivityCategoryId)
              .Select(a => new {TimeSpent = a.Sum(b => b.TimeSpentOnActivity)});
          PersistenceData.TempResult =
              $"[{selection}] The percentage of time spent relaxing in 26 days: {(summary8.Any() ? summary8.First().TimeSpent / totalHoursIn26Days : 0):P}";
          break;
        default:
          Shared.Welcome();
          break;
      }

      View();
  }
  }
}