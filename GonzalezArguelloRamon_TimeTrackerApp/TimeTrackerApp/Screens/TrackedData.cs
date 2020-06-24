using System;
using System.Linq;
using TimeTrackerApp.Constants;
using TimeTrackerApp.Helpers;

namespace TimeTrackerApp.Screens
{
   // Tracked Data Screen
  public static class TrackedData
  {
    private static readonly DbHelper DbHelper = new DbHelper();
    private static PersistenceData PersistenceData = PersistenceData.Instance;

      // Tracked Data Banner
    private static void Header()
    {
      Console.Clear();
      Shared.Header();
      Console.WriteLine("View Tracked Data\n");
      Console.WriteLine("[1] View By Date");
      Console.WriteLine("[2] View By Category");
      Console.WriteLine("[3] View By Description\n");
      Console.WriteLine("[0] Main Menu");
      WriterHelper.RedText("=", 119);
    }

      // View Tracked Data By 
    public static void ViewBy()
    {
      Header();
      Console.Write("Selection: ");
      var selection = Convert.ToInt32(Console.ReadLine());
      ViewByMenu(selection);
    }

     // Select view for tracked data screen
    public static void ViewByMenu(int selection)
    {
      switch (selection)
      {
        case 1:
          ViewByDate();
          break;
        case 2:
          ViewByCategory();
          break;
        case 3:
          ViewByDescription();
          break;
        default:
          Shared.Welcome();
          break;
      }
    }

    // View Tracked Data by Activity Description
    public static void ViewByDescription()
    {
      var user = PersistenceData.ActiveUser;
      var selected = default(int);
      Console.Clear();

        // Print Application Banner
      Shared.Header();
      Console.WriteLine("View By Description: \n");
      Console.WriteLine("[0] Exit");
      WriterHelper.RedText("=", 119);

        // Get all category activities in database
      var descriptions = DbHelper.Activities();

        // Print all category options
      descriptions.ForEach(c => Console.WriteLine($"[{c.Id}] " + $"{c.ActivityDescription}"));
      WriterHelper.RedText("=", 119);
      Console.Write("Select Description: ");
      selected = Convert.ToInt32(Console.ReadLine());
            
      if (selected == 0)
      {
        ViewByMenu(selected);
      }

        // Get all activities filtered by category 
      var selectedActivity = descriptions.First(d => d.Id == selected);

       // View By Description
      Console.Clear();

        //Print Tracked Data Banner
      Header();

        // Print selected category and activity
      Console.WriteLine($"Description: " + $"{selectedActivity.ActivityDescription.ToUpper()}:\n");
      Console.WriteLine(WindowTitle.VIEW_BY_DESCRIPTION);
      WriterHelper.WhiteText("-", 119);

        // Get summary data from database
      var viewby = DbHelper.ViewBy();
      var row = 1;
      var total = default(decimal);
      var summary = viewby
          .Where(c => c.UserId == user.Id) // filter by user id
          .Where(c1 => c1.ActivityDescriptionId == selectedActivity.Id) // filter by selected category
          .Select(a => new //new anonymous type object
          {
            Date = a.CalendarDate.ToString("dd MMMM yyyy"),
            Activity = a.ActivityDescription,
            TimeSpent = a.TimeSpentOnActivity
          });

      if (summary.Any())
      {
          // Printing table of activities filtered by activity id
          // [index][22?]Activity[43?]timeSpent
        summary.ToList().ForEach(r =>
        {
          var index = row++.ToString().PadLeft(2, ' ');
          total += r.TimeSpent;
          Console.WriteLine(
              $"[{index}] {r.Date.PadRight(22, ' ')} " +
              $"{r.Activity.PadRight(43, ' ')} " +
              $"{r.TimeSpent}");
        });
      }
      else
      {
        Console.WriteLine("No data found for the selected description");
      }

      WriterHelper.WhiteText("-", 119);
      Console.WriteLine($"Activity {selectedActivity.ActivityDescription} " +  $"Total: {total} hours");
      WriterHelper.RedText("=", 119);
      Console.Write("Selection: ");
      selected = Convert.ToInt32(Console.ReadLine());
      ViewByMenu(selected);
    }

      // View Tracked Data by Category Description
    public static void ViewByCategory()
    {
      var user = PersistenceData.ActiveUser;
      var selected = default(int);
      Console.Clear();

        // Print application banner
      Shared.Header();
      Console.WriteLine("View By Category: \n");
      Console.WriteLine("[0] Exit");
      WriterHelper.RedText("=", 119);

        //Get Categories available in database
      var categories = DbHelper.Categories();
      categories.ForEach(c => Console.WriteLine($"[{c.Id}] " + $"{c.CategoryDescription}"));
      WriterHelper.RedText("=", 119);
      Console.Write("Select Category: ");
      selected = Convert.ToInt32(Console.ReadLine());

      if (selected == 0)
      {
        ViewByMenu(selected);
      }

      var selectedCategory = categories.First(d => d.Id == selected);

        //View By Category
      Console.Clear();

        // Print Tracked Data Banner
      Header();
      Console.WriteLine($"CATEGORY: {selectedCategory.CategoryDescription.ToUpper()}:\n");
      Console.WriteLine(WindowTitle.VIEW_BY_CATEGORY);
      WriterHelper.WhiteText("-", 119);

      var viewby = DbHelper.ViewBy();
      var row = 1;
      var total = default(decimal);
      var summary = viewby
          .Where(d => d.UserId == user.Id)
          .Where(d1 => d1.ActivityCategoryId == selectedCategory.Id)
          .Select(a => new
          {
            Date = a.CalendarDate.ToString("dd MMMM yyyy"),
            Category = a.CategoryDescription,
            Activity = a.ActivityDescription,
            TimeSpent = a.TimeSpentOnActivity
          });

      if (summary.Any())
      {
          // Print table for activities filtered by category
          // [index][22?]Category[31?]Activity[23?]timeSpent
        summary.ToList().ForEach(r =>
        {
          var index = row++.ToString().PadLeft(2, ' ');
          total += r.TimeSpent;
          Console.WriteLine(
              $"[{index}] {r.Date.PadRight(22, ' ')} " +
              $"{r.Category.PadRight(31, ' ')} " +
              $"{r.Activity.PadRight(23, ' ')} {r.TimeSpent}");
        });
      }
      else
      {
        Console.WriteLine("No data found for the selected category");
      }

      WriterHelper.WhiteText("-", 119);
      Console.WriteLine($"Category {selectedCategory.CategoryDescription} Total: {total} hours");
      WriterHelper.RedText("=", 119);
      Console.Write("Selection: ");
      selected = Convert.ToInt32(Console.ReadLine());
      ViewByMenu(selected);
    }

      // View Tracked Data by Date
    public static void ViewByDate()
    {
      var user = PersistenceData.ActiveUser;

      Console.Clear();
      Shared.Header();
      Console.WriteLine("View By Date: \n");
      Console.WriteLine("[0] Exit");
      WriterHelper.RedText("=", 119);

        // Get Date Id
      var dates = DbHelper.CalendarDates();
      dates.ForEach(d => Console.WriteLine($"[{d.Id}] {d.FormattedDate()}"));
      WriterHelper.RedText("=", 119);
      Console.Write("Select Date: ");

      var selection = Convert.ToInt32(Console.ReadLine());
      var selectedDate = dates.First(d => d.Id == selection);

      if (selection == 0)
      {
          ViewByMenu(selection);
      }

        // Display Data by Date
      Console.Clear();
      Header();

      Console.WriteLine($"{selectedDate.Date.ToString("dddd dd MMMM yyyy").ToUpper()}: \n");
      var totalTimeTracked = default(decimal);
      var viewSummary = DbHelper.ViewBy();
      var data = viewSummary.Where(a => a.UserId == user.Id) // filter by user id
          .Where(a1 => a1.CalendarDateId == selectedDate.Id) // filter activity by date 
          .GroupBy(c => c.CategoryDescription).Select(c1 => new
          {
            Category = c1.First().CategoryDescription,
            Activity = c1.First().ActivityDescription,
            TimeSpent = c1.Sum(d => d.TimeSpentOnActivity)
          });

      Console.WriteLine(WindowTitle.VIEW_BY_DATE);
      WriterHelper.WhiteText("-", 119);

      if (data.Any())
      {
        var rowNum = 1;
        data?.ToList().ForEach(d =>
        {
          var row = rowNum++.ToString().PadRight(2, ' ');
          var category = d.Category.PadRight(26, ' ');
          var activity = d.Activity.PadRight(31, ' ');
          var total = d.TimeSpent;
          totalTimeTracked += total;

            // Print activities filtered by date
          Console.WriteLine($"[{row}] {category} {activity} {total}");
        });
      }
      else
      {
        Console.WriteLine("No tracked data found for the selected date");
      }

      WriterHelper.WhiteText("-", 119);
      Console.WriteLine($"Activity Time Tracked: {totalTimeTracked} hours");
      Console.WriteLine("Activity Time Untracked: 0 hours");
      WriterHelper.RedText("=", 119);
      Console.Write("Selection: ");
      selection = Convert.ToInt32(Console.ReadLine());
      ViewByMenu(selection);
    }
  }
}