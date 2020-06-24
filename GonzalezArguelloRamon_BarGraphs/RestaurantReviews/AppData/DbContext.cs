using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RestaurantReviews.Constants;
using RestaurantReviews.Models;

namespace RestaurantReviews.AppData
{
  /// <summary>
  ///   Database Access Layer
  /// </summary>
  public class DbContext
  {
    public DbContext()
    {
      ConfigurationRoot = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(ApplicationConfig.APP_CONFIG, false)
        .Build();
      try
      {
        ConnectionString = ConfigurationRoot.GetConnectionString("MySQLRestaurant");
        Connection = new MySqlConnection(ConnectionString);
      }
      catch (MySqlException e)
      {
        throw new ConfigurationErrorsException(e.Message);
      }
    }

    private IConfigurationRoot ConfigurationRoot { get; }
    private string ConnectionString { get; }
    private MySqlConnection Connection { get; }

    /// <summary>
    ///   Database Access Test
    /// </summary>
    /// <returns>Assert</returns>
    public string Test()
    {
      try
      {
        Connection.Open();
        return $"Connection Successful to MySQL Server version: {Connection.ServerVersion}";
      }
      catch (Exception e)
      {
        return e.Message;
      }
    }

    /// <summary>
    ///   Generic Select Method
    /// </summary>
    /// <param name="query">SQL String query</param>
    /// <param name="callback">Lambda || Delegate function to process data</param>
    /// <typeparam name="T">Generic Type</typeparam>
    /// <returns>New collection of type T</returns>
    private List<T> Select<T>(string query, Func<MySqlDataReader, List<T>> callback)
    {
      // create new Mysql Command 
      var cmd = new MySqlCommand(query, Connection);
      Connection.Open(); // open connection with database
      var reader = cmd.ExecuteReader(); // create an new reader
      var result = callback(reader); // execute callback
      Connection.Close(); // close connection with database
      return result;
    }

    /// <summary>
    ///   Get list of restaurants
    /// </summary>
    /// <returns>IEnumerable Collection of Restaurants</returns>
    public IEnumerable<RestaurantProfiles> Restaurants()
    {
      // get all reviews from database
      var reviews = Reviews()?.ToList();
      // if application testing mode is on limit the query to the base up to 10 records only
      var query = ApplicationConfig.IS_TESTING
        ? AppQueries.QUERY_RESTAURANTS_PROFILES + " LIMIT 10"
        : AppQueries.QUERY_RESTAURANTS_PROFILES;
      return Select(query,
        reader =>
        {
          var result = new List<RestaurantProfiles>();
          while (reader.Read())
            result.Add(new RestaurantProfiles
            {
              Id = Convert.ToInt32(reader["id"].ToString()),
              RestaurantName = reader["RestaurantName"]?.ToString(),
              Address = reader["Address"]?.ToString(),
              Phone = reader["Phone"]?.ToString(),
              HoursOfOperation = reader["HoursOfOperation"]?.ToString(),
              Price = reader["Price"]?.ToString(),
              USACityLocation = reader["USACityLocation"]?.ToString(),
              Cuisine = reader["Cuisine"]?.ToString(),
              FoodRating =
                Convert.ToDecimal(reader["FoodRating"] != DBNull.Value ? reader["FoodRating"] : 0),
              ServiceRating = Convert.ToDecimal(reader["ServiceRating"] != DBNull.Value
                ? reader["ServiceRating"]
                : 0),
              AmbienceRating = Convert.ToDecimal(reader["AmbienceRating"] != DBNull.Value
                ? reader["AmbienceRating"]
                : 0),
              ValueRating =
                Convert.ToDecimal(reader["ValueRating"] != DBNull.Value ? reader["ValueRating"] : 0),
              OverallRating = Convert.ToDecimal(reader["OverallRating"] != DBNull.Value
                ? reader["OverallRating"]
                : 0),
              OverallPossibleRating = Convert.ToDecimal(reader["OverallPossibleRating"] != DBNull.Value
                ? reader["OverallPossibleRating"]
                : 0),
              Reviews = reviews.Where(r => r.RestaurantId == Convert.ToInt32(reader["id"]))
                ?.ToList()
            });

          return result;
        });
    }

    /// <summary>
    ///   Restaurant Reviews All records for restaurant reviews
    /// </summary>
    /// <returns></returns>
    public IEnumerable<RestaurantReview> Reviews()
    {
      return Select(AppQueries.QUERY_RESTAURANTS_REVIEWS,
        reader =>
        {
          var result = new List<RestaurantReview>();
          while (reader.Read())
            result.Add(new RestaurantReview
            {
              Id = Convert.ToInt32(reader["id"].ToString()),
              RestaurantId = Convert.ToInt32(reader["RestaurantId"]),
              ReviewScore =
                Convert.ToInt32(reader["ReviewScore"] != DBNull.Value ? reader["ReviewScore"] : 0),
              PossibleReviewScore = Convert.ToInt32(reader["PossibleReviewScore"] != DBNull.Value
                ? reader["PossibleReviewScore"]
                : 0),
              ReviewText = reader["ReviewText"]?.ToString(),
              ReviewColor = reader["ReviewColor"]?.ToString()
            });

          return result;
        });
    }
  }
}