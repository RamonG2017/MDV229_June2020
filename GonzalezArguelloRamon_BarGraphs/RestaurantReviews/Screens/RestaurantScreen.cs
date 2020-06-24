using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using RestaurantReviews.AppData;
using RestaurantReviews.Constants;
using RestaurantReviews.Extensions;
using RestaurantReviews.Helpers;
using RestaurantReviews.Models;

namespace RestaurantReviews.Screens
{
  /// <summary>
  ///   Restaurant Screen Helper
  /// </summary>
  public class RestaurantScreen : IVisualizationOption
  {
    /// <summary>
    ///   Go back to main menu delegate action
    /// </summary>
    private readonly Action GoBackToWelcomeMenu;

    public RestaurantScreen(Action goBackToWelcomeMenu)
    {
      // database access
      DbContext = new DbContext();
      Selection = default;
      // 
      Restaurants = DbContext.Restaurants()?.ToList();
      ChartHelper = new ChartHelper();
      Options = new[]
      {
        "\r\n",
        "\t[1] Show Average of Reviews for Restaurants",
        "\t[2] Dinner Spinner",
        "\t[3] Top 10 Restaurants",
        "\r\n",
        "\t[4] Back to Main Menu"
      };
      Random = new Random();
      GoBackToWelcomeMenu = goBackToWelcomeMenu;
      JsonHelper = new JSONHelper();
    }

    /// <summary>
    ///   Data Base Access Layer
    /// </summary>
    private DbContext DbContext { get; }

    /// <summary>
    ///   Current Selection
    /// </summary>
    private int Selection { get; set; }

    /// <summary>
    ///   Restaurant Entities from database
    /// </summary>
    private List<RestaurantProfiles> Restaurants { get; }

    /// <summary>
    ///   Data Visualization Helper
    /// </summary>
    private ChartHelper ChartHelper { get; }

    /// <summary>
    ///   Menu Options
    /// </summary>
    private string[] Options { get; }

    /// <summary>
    ///   Random Generator Helper
    /// </summary>
    private Random Random { get; }

    /// <summary>
    ///   JSON Converter Helper
    /// </summary>
    private JSONHelper JsonHelper { get; }

    public void ToJSON()
    {
      var skip = new[]
      {
        "Reviews"
      };
      var output = JsonHelper.ToJSON(DbContext.Restaurants(), skip);
      File.WriteAllText(ApplicationConfig.OUTPUT_PATH, output);
      Console.WriteLine("File has been created!");
      Console.WriteLine("\r\n");
      Console.WriteLine("Press any key to return to the menu ...");
      Console.ReadLine();
      GoBackToWelcomeMenu();
    }

    /// <summary>
    ///   Welcome banner for restaurant screen
    /// </summary>
    public void MainMenu()
    {
      Console.Clear();
      // take the array of options, join it by line breaks creating 
      // a paragraph and print it directly to the screen
      Console.WriteLine(string.Join("\n", Options));
      Console.Write("\r\n");
      Console.WriteLine("=============================================" +
                        "=========================");
      Console.Write("Make your selection: ");
      Selection = Convert.ToInt32(Console.ReadLine());
      ProcessSelection();
    }

    /// <summary>
    ///   Process User Selection
    /// </summary>
    private void ProcessSelection()
    {
      // user selection
      switch (Selection)
      {
        case -1:
          Console.Clear();
          MainMenu();
          break;
        case 1:
          ShowAverageReviews();
          break;
        case 2:
          DinnerSpinner();
          break;
        case 3:
          TopTen();
          break;
        case 4:
          GoBackToWelcomeMenu();
          break;
        default:
          Console.WriteLine("Invalid Selection\n Press any key...");
          Console.ReadLine();
          Console.Clear();
          MainMenu();
          break;
      }
    }

    /// <summary>
    ///   Get Top Ten Reviewed Restaurants
    ///   Be mindful is you are in testing mode the result may vary due to the limitation
    ///   impose in your fetch query.
    /// </summary>
    private void TopTen()
    {
      Console.Clear();
      var topTenRestaurants = TopTenRestaurants();
      var sortRestaurants = topTenRestaurants.OrderByDescending(s => s.AverageRating()).Take(10);
      // print options in a fancy way
      Console.WriteLine(string.Join("\n", Options));
      Console.WriteLine($"Enter your choice: {Selection}");
      foreach (var restaurant in sortRestaurants)
      {
        Console.WriteLine($"{restaurant.RestaurantName} - Rating: {restaurant.HumanAverageRating()}");
        // animate the bar chart
        AnimateChart(restaurant.AverageRating());
        // print real bar chart with restaurant rating
        ChartHelper.Generate(restaurant.AverageRating());
      }

      Console.WriteLine("Press any key to return to the menu ...");
      Console.ReadLine();
      Selection = -1;
      ProcessSelection();
    }

    /// <summary>
    ///   Animate Chart
    /// </summary>
    /// <param name="rating"></param>
    private void AnimateChart(double rating)
    {
      for (var iteration = 0; iteration < ApplicationConfig.ANIMATION_LOOP; iteration++)
      {
        // print random chart
        ChartHelper.Generate(rating);
        Thread.Sleep(50); // allowing human eye to see the content update
        rating = RandomRating(); // set new random rating
        // set cursor position at the line break and clear
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        // set cursor position at the char line and clear
        Console.SetCursorPosition(0, Console.CursorTop - 1);
      }
    }

    /// <summary>
    ///   Get Top 10 Restaurants by Overall Rating
    /// </summary>
    /// <returns></returns>
    private IEnumerable<RestaurantProfiles> TopTenRestaurants()
    {
      return DbContext.Restaurants().OrderByDescending(d => d.OverallRating);
    }

    /// <summary>
    ///   Find a random restaurant
    /// </summary>
    private void DinnerSpinner()
    {
      Console.Clear();
      // get random values
      var rating = RandomRating();
      var restaurant = RandomRestaurant();
      // print options in a fancy way
      Console.WriteLine(string.Join("\n", Options));
      Console.WriteLine($"Enter your choice: {Selection}");
      // print random restaurant information
      Console.WriteLine($"{restaurant.RestaurantName} - Rating: {restaurant.HumanAverageRating()}");
      // animate the bar chart
      AnimateChart(rating);
      // print real bar chart with restaurant rating
      ChartHelper.Generate(restaurant.AverageRating());
      Console.WriteLine("Press any key to return to the menu ...");
      Console.ReadLine();
      Selection = -1;
      ProcessSelection();
    }

    /// <summary>
    ///   Get a random restaurant information
    /// </summary>
    /// <returns></returns>
    private RestaurantProfiles RandomRestaurant()
    {
      var restaurantProfiles = DbContext.Restaurants()?.ToList();
      var restaurantId = Random.NextRandom(1, restaurantProfiles.Max(r => r.Id));
      return restaurantProfiles.First(r => r.Id == restaurantId);
    }

    /// <summary>
    ///   Get a random restaurant rating
    /// </summary>
    /// <returns></returns>
    private double RandomRating()
    {
      // new random rating value
      return Random.NextRandom(
        (double)ApplicationConfig.RANDOM_LIMITS["min"],
        (double)ApplicationConfig.RANDOM_LIMITS["max"]);
    }

    /// <summary>
    ///   Print average rating for registered restaurant profiles.
    /// </summary>
    private void ShowAverageReviews()
    {
      // Restaurant entities from database
      Restaurants.ForEach(r =>
      {
        Console.Write($"{r.RestaurantName} - Rating: {r.HumanAverageRating()} Bar Graph: ");
        ChartHelper.Generate(r.AverageRating());
        Thread.Sleep(ApplicationConfig.CHART_DELAY); // pause execution for a brief moment
      });
      Console.WriteLine("Press any key to return to the menu ...");
      Console.ReadLine();
      Selection = -1;
      ProcessSelection();
    }
  }
}