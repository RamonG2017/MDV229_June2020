using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Helpers;
using CardGame.Models;

namespace CardGame.Screens
{
  /// <summary>
  ///   Rating System Screen
  /// </summary>
  public class RatingSystemScreen : IVisualizationOption
  {
    public RatingSystemScreen(Action goBackToMainMenu)
    {
      Options = new[]
      {
        "\t[1] List Restaurants Alphabetically",
        "\t[2] List Restaurants in Reverse Alphabetical",
        "\t[3] Sort Restaurants From Best to Worst",
        "\t[4] Sort Restaurants From Worst to Best",
        "\t[5] Show Only X and Up",
        "\r\n"
      };
      SubOptions = new[]
      {
        "\r\n",
        "\t[1] Show the best",
        "\t[2] Show 4 Stars and Up",
        "\t[3] Show 3 Stars and Up",
        "\t[4] Show the Worst",
        "\t[5] Show Unrated"
      };
      RatingHelper = new RatingHelper();
      GoBackToMainMenu = goBackToMainMenu;
    }

    /// <summary>
    ///   Menu Options
    /// </summary>
    private IEnumerable<string> Options { get; }

    /// <summary>
    ///   Sub Menu Options
    /// </summary>
    private IEnumerable<string> SubOptions { get; }

    /// <summary>
    ///   Current Selection
    /// </summary>
    private int Selection { get; set; }

    /// <summary>
    ///   User Name
    /// </summary>
    private string Name { get; set; }

    /// <summary>
    ///   Rating Query Helper
    /// </summary>
    private RatingHelper RatingHelper { get; }

    /// <summary>
    ///   Lambda for go back to main menu
    /// </summary>
    private Action GoBackToMainMenu { get; }

    /// <summary>
    ///   Main Menu Option
    /// </summary>
    /// <param name="name">User Name</param>
    public void MainMenu(string name)
    {
      Console.Clear();
      Name = name;
      Console.WriteLine($"Hello {Name}, How would you like to sort the data?");
      Console.WriteLine("\r\n");
      Console.WriteLine(string.Join(Environment.NewLine, Options));
      Console.WriteLine("\t[6] Exit");
      Console.Write("\r\n");
      Console.WriteLine("=============================================" +
                        "=========================");
      Console.Write("Make your selection: ");
      Selection = Convert.ToInt32(Console.ReadLine());
      ProcessSelection();
    }

    /// <summary>
    ///   Sub Menu Options
    /// </summary>
    private void SubMenu()
    {
      Console.Clear();
      Console.WriteLine(string.Join(Environment.NewLine, SubOptions));
      Console.Write("\r\n");
      Console.WriteLine("\t[6] Exit");
      Console.Write("\r\n");
      Console.WriteLine("=============================================" +
                        "=========================");
      Console.Write("Make your selection: ");
      // add 6 to the selection because of the 6 options already available on root menu
      Selection = Convert.ToInt32(Console.ReadLine()) + 6;
      ProcessSelection();
    }

    /// <summary>
    ///   Process Selected Option
    /// </summary>
    private void ProcessSelection()
    {
      switch (Selection)
      {
        case 1:
          PrintRating(RatingHelper.Get(SortBy.NAME_ASC));
          break;
        case 2:
          PrintRating(RatingHelper.Get(SortBy.NAME_DESC));
          break;
        case 3:
          PrintRating(RatingHelper.Get(SortBy.BEST_DESC));
          break;
        case 4:
          PrintRating(RatingHelper.Get(SortBy.WORST_DESC));
          break;
        case 5:
          SubMenu();
          break;
        case 6:
          GoBackToMainMenu();
          break;
        case 7:
          PrintRating(RatingHelper.Get(SortBy.THE_BEST));
          break;
        case 8:
          PrintRating(RatingHelper.Get(SortBy.IV_STARS_UP));
          break;
        case 9:
          PrintRating(RatingHelper.Get(SortBy.III_STARS_UP));
          break;
        case 10:
          PrintRating(RatingHelper.Get(SortBy.THE_WORST));
          break;
        case 11:
          PrintRating(RatingHelper.Get(SortBy.UNRATED));
          break;
        default:
          MainMenu(Name);
          break;
      }
    }

    /// <summary>
    ///   Generic Rating Printer
    /// </summary>
    /// <param name="data">Sorted/Filtered Data</param>
    private void PrintRating(IEnumerable<RestaurantProfiles> data)
    {
      Console.WriteLine("======================================================================");
      for (var i = 0; i < data.Count(); i++)
      {
        var item = data.ElementAt(i);
        var number = (i + 1).ToString().PadLeft(5, '0');
        var name = item.RestaurantName.PadRight(46, ' ');
        Console.Write($"{number} {name} Rating: ");
        RatingHelper.DrawStars(item.OverallRating.GetValueOrDefault());
        Console.Write(Environment.NewLine);
      }

      Console.WriteLine("======================================================================");
      Console.WriteLine("Press any key to return to the main menu ...");
      Console.ReadLine();
      // if current value is greater than 6 means is a sub menu, then go back to sub menu root.
      Selection = Selection > 6 ? 5 : -1;
      ProcessSelection();
    }
  }
}