using System;
using RestaurantReviews.Constants;

namespace RestaurantReviews.Screens
{
  /// <summary>
  ///   Data Visualization App Welcome Screen
  /// </summary>
  public class Welcome : IVisualizationOption
  {
    public Welcome()
    {
      RestaurantScreen = new RestaurantScreen(() =>
      {
        Selection = -1;
        ProcessSelection();
      });
      Selection = default;
      RatingSystemScreen = new RatingSystemScreen(() =>
      {
        Selection = -1;
        ProcessSelection();
      });
    }

    /// <summary>
    ///   Restaurant Data Process Options
    /// </summary>
    private RestaurantScreen RestaurantScreen { get; }

    private RatingSystemScreen RatingSystemScreen { get; }

    /// <summary>
    ///   Selected Option
    /// </summary>
    private int Selection { get; set; }

    /// <summary>
    ///   Welcome banner
    /// </summary>
    public void Header()
    {
      Console.WriteLine(ApplicationConfig.WELCOME);
      Console.WriteLine("\r\n");

      Console.WriteLine("\t[1] Convert The Restaurant Profile Table " +
                        "From SQL To JSON");
      Console.WriteLine("\t[2] Showcase Our 5 Star Rating System");
      Console.WriteLine("\t[3] Showcase Our Animated Bar Graph Review " +
                        "System");
      Console.WriteLine("\t[4] Play A Card Game");

      Console.Write("\r\n");

      Console.WriteLine("\t[5] Exit");

      Console.Write("\r\n");

      Console.WriteLine("=============================================" +
                        "=========================");

      Console.Write("Make your selection: ");
      Selection = Convert.ToInt32(Console.ReadLine());
      ProcessSelection();
    }

    /// <summary>
    ///   Process user selection
    /// </summary>
    private void ProcessSelection()
    {
      switch (Selection)
      {
        case -1:
          Console.Clear();
          // go back to main menu
          Header();
          break;
        case 1:
          Console.Clear();
          RestaurantScreen.ToJSON();
          break;
        case 2:
          Console.Clear();
          Console.Write("Please enter your name: ");
          var name = Console.ReadLine();
          RatingSystemScreen.MainMenu(name);
          break;
        case 3:
          RestaurantScreen.MainMenu();
          break;
        case 4:
          throw new NotImplementedException("Stay tuned for further updates");
        case 5:
          Console.Clear();
          Environment.Exit(Environment.ExitCode);
          break;
        default:
          Console.WriteLine("Invalid selection, press any key...");
          Console.ReadLine();
          Console.Clear();
          // go back to main menu
          Header();
          break;
          ;
      }
    }
  }
}