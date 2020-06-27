using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.AppData;
using CardGame.Models;

namespace CardGame.Helpers
{
  /// <summary>
  ///   Rating Query Helper
  /// </summary>
  public class RatingHelper
  {
    public RatingHelper()
    {
      DbContext = new DbContext();
      Restaurants = DbContext.Restaurants();
    }

    /// <summary>
    ///   Data Access Layer
    /// </summary>
    private DbContext DbContext { get; }

    /// <summary>
    ///   Data Container
    /// </summary>
    private IEnumerable<RestaurantProfiles> Restaurants { get; }

    /// <summary>
    ///   Filter and Sort Restaurant Profiles By Specific Selection
    /// </summary>
    /// <param name="sortBy"></param>
    /// <returns></returns>
    public IEnumerable<RestaurantProfiles> Get(SortBy sortBy)
    {
      var result = Restaurants;
      switch (sortBy)
      {
        case SortBy.NAME_ASC:
          result = Restaurants.OrderBy(a => a.RestaurantName);
          break;
        case SortBy.NAME_DESC:
          result = Restaurants.OrderByDescending(a => a.RestaurantName);
          break;
        case SortBy.BEST_DESC:
          result = Restaurants.OrderByDescending(a => a.OverallRating.GetValueOrDefault());
          break;
        case SortBy.WORST_DESC:
          result = Restaurants.OrderBy(a => a.OverallRating.GetValueOrDefault());
          break;
        case SortBy.THE_BEST:
          result = Restaurants.Where(b => b.OverallRating == Restaurants.Max(a => a.OverallRating));
          break;
        case SortBy.IV_STARS_UP:
          result = Restaurants.Where(a => a.OverallRating >= 4);
          break;
        case SortBy.III_STARS_UP:
          result = Restaurants.Where(a => a.OverallRating >= 3);
          break;
        case SortBy.THE_WORST:
          result = Restaurants.Where(a =>
            a.OverallRating == Restaurants.Where(z => z.OverallRating.GetValueOrDefault() != 0)
              .Min(b => b.OverallRating));
          break;
        case SortBy.UNRATED:
          result = Restaurants.Where(a => a.OverallRating.GetValueOrDefault() == 0);
          break;
        default:
          result = Restaurants.OrderBy(a => a.Id);
          break;
      }

      return result;
    }

    public void DrawStars(decimal rating)
    {
      var zero = "No Rating";
      var oneStar = "*";
      var twoStars = "**";
      var threeStars = "***";
      var fourStars = "****";
      var fiveStars = "*****";

      if (rating <= 0)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(zero);

        Console.ResetColor();
      }

      if (rating > 0 && rating <= 1)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(oneStar);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(fourStars);

        Console.ResetColor();
      }

      if (rating > 1 && rating <= 2)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(twoStars);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(threeStars);

        Console.ResetColor();
      }

      if (rating > 2 && rating <= 3)
      {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(threeStars);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(twoStars);

        Console.ResetColor();
      }

      if (rating > 3 && rating <= 4)
      {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(fourStars);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(oneStar);

        Console.ResetColor();
      }

      if (rating > 4)
      {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(fiveStars);

        Console.ResetColor();
      }
    }
  }
}