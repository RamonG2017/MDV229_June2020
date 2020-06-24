using System;
using System.Linq;
using TimeTrackerApp.Helpers;

namespace TimeTrackerApp.Screens
{
    // Activity Screen
  public static class Activities
  {
    private static PersistenceData PersistenceData = PersistenceData.Instance;

      // Activity Screen Banner
    private static void Header()
    {
      Console.Clear();
      Shared.Header();
      Console.WriteLine("Enter Activity\n");
      Console.WriteLine("[0] Exit");
      WriterHelper.RedText("=", 119);
    }

      // Add new activity screen
    public static void Add()
    {
      var activityBuilder = new ActivityBuilder();

        // Print activity header
      Header();
            
        // Print Categories and ask for input
      activityBuilder.PrintCategoryOptions();
      WriterHelper.RedText("=", 119);
      Console.Write("Select A Category: ");
      var option1 = Console.ReadLine();
      var step1 = Convert.ToInt32(option1);

        // go back to main menu
      if (activityBuilder.SetCategory(step1))
      {
        Shared.MainMenu();
      }

        // Print Dates and ask for input
      Header();
      activityBuilder.PrintDateOptions();
      WriterHelper.RedText("=", 119);
      Console.Write("Select A Date: ");
      var option2 = Console.ReadLine();
      var step2 = Convert.ToInt32(option2);

        // go back to main menu
      if (activityBuilder.SetDate(step2))
      {
        Shared.MainMenu();
      }

        // Print time options and ask for input
      Header();
      activityBuilder.PrintTimeOptions();
      WriterHelper.RedText("=", 119);
      Console.Write("How Long Did You Perform That Activity?: ");
      var option3 = Console.ReadLine();
      var step3 = Convert.ToInt32(option3);
      activityBuilder.SetTimeSpent(step3);

      PersistenceData.TempMessage = activityBuilder.Build() > 0
          ? "Your activity has successfully been added!"
          : "Something went wrong...";

      Shared.Welcome();
    }
  }
}