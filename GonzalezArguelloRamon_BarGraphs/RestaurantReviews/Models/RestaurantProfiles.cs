using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantReviews.Constants;

namespace RestaurantReviews.Models
{
  /// <summary>
  ///   Restaurant Profile Model
  /// </summary>
  public class RestaurantProfiles
  {
    public RestaurantProfiles()
    {
      Id = default;
      Address = string.Empty;
      RestaurantName = string.Empty;
      Address = string.Empty;
      Phone = string.Empty;
      HoursOfOperation = string.Empty;
      Price = string.Empty;
      USACityLocation = string.Empty;
      Cuisine = string.Empty;
      FoodRating = default;
      ServiceRating = default;
      AmbienceRating = default;
      ValueRating = default;
      OverallRating = default;
      OverallPossibleRating = default;
      Reviews = new List<RestaurantReview>();
    }

    public int Id { get; set; }
    public string RestaurantName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string HoursOfOperation { get; set; }
    public string Price { get; set; }
    public string USACityLocation { get; set; }
    public string Cuisine { get; set; }
    public decimal? FoodRating { get; set; }
    public decimal? ServiceRating { get; set; }
    public decimal? AmbienceRating { get; set; }
    public decimal? ValueRating { get; set; }
    public decimal? OverallRating { get; set; }
    public decimal? OverallPossibleRating { get; set; }
    public List<RestaurantReview> Reviews { get; set; }

    /// <summary>
    ///   Calculate the average rating for the total amount of reviews found for the selected restaurant profile
    /// </summary>
    /// <returns>Average Rating Number</returns>
    public double AverageRating()
    {
      var summary = ApplicationConfig.IS_TESTING ? Reviews.Take(ApplicationConfig.LIMIT)?.ToList() : Reviews;
      return summary.Any() ? summary.Average(r => r.ReviewScore) : 0;
    }

    /// <summary>
    ///   Human Readable Average Rating
    /// </summary>
    /// <returns></returns>
    public string HumanAverageRating()
    {
      // Scale the response
      var rating = (int) Math.Floor(AverageRating() / ApplicationConfig.BASE_SCALE);
      // " average / base scale "
      return $"{rating}/{ApplicationConfig.BASE_SCALE}";
    }
  }
}