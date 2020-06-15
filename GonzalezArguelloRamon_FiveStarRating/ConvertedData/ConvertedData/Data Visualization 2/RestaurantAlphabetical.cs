/******************************************************************************
filename   RestaurantAlphabetical.cs
author     Ramon Gonzalez Arguello
email      rgonzalezarguello@student.fullsail.edu
course     MDV229
section    01
assignment 2        
due date   06/14/2020
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConvertedData
{
  public class RestaurantAlphabetical
  {
    public void RestaurantAlphabeticalStart()
    {
      string cs = @"server=127.0.0.1;userid=dbremoteuser;password=password;database=SampleRestaurantDatabase;port=8889";

      MySqlConnection conn = null;

      try
      {
        //create new lists to store the values
        List<string> restaurantNames = new List<string>();
        List<decimal> overallRating = new List<decimal>();

        conn = new MySqlConnection(cs);
        conn.Open();

        //append the returned list from the private methods into these lists
        restaurantNames.AddRange(GetRestaurantNameASC(conn));
        overallRating.AddRange(GetOverallRatingASC(conn));

        for (int i = 0; i < 100; i++)
        {
          decimal rating = overallRating[i];
          string restNumber = (i + 1) + ". ";
          

          Console.WriteLine("=============================================" +
                          "=========================");

          Console.Write("{0, 5}", restNumber);
          Console.Write("{0, 5}", restaurantNames[i]);
          Console.Write("{0, 10}", "Rating: ");
          Console.WriteLine(DrawStars.DrawStarsStart(rating));
         

          if (i == 99)
          {
            Console.WriteLine("=============================================" +
                         "=========================");
          }

        }

          Console.WriteLine("Please enter return key to go back to menu...");
      }
      catch (MySqlException error)
      {
        Console.WriteLine("Error: {0}", error.ToString());
      }
      finally
      {
        if (conn != null)
        {
          conn.Close();
        }
      }
    }

    // Function for the reverse alphabetical display
    public void RestaurantReverseAlphabeticalStart()
    {
      string cs = @"server=127.0.0.1;userid=dbremoteuser;password=password;database=SampleRestaurantDatabase;port=8889";

      MySqlConnection conn = null;

      try
      {
        //create new lists to store the values
        List<string> restaurantNames = new List<string>();
        List<decimal> overallRating = new List<decimal>();

        conn = new MySqlConnection(cs);
        conn.Open();

        //append the returned list from the private methods into these lists
        restaurantNames.AddRange(GetRestaurantNameDESC(conn));
        overallRating.AddRange(GetOverallRatingDESC(conn));

        //reverse the values of the lists to accomodate reverse alphabetical
        restaurantNames.Reverse();
        overallRating.Reverse();

        for (int i = 0; i < 100; i++)
        {
          decimal rating = overallRating[i];
          string restNumber = (100 - i) + ". ";


          Console.WriteLine("=============================================" +
                          "=========================");

          Console.Write("{0, 5}", restNumber);
          Console.Write("{0, 5}", restaurantNames[i]);
          Console.Write("{0, 10}", "Rating: ");
          Console.WriteLine(DrawStars.DrawStarsStart(rating));


          if (i == 99)
          {
            Console.WriteLine("=============================================" +
                         "=========================");
          }

        }

        Console.WriteLine("Please enter return key to go back to menu...");
      }
      catch (MySqlException error)
      {
        Console.WriteLine("Error: {0}", error.ToString());
      }
      finally
      {
        if (conn != null)
        {
          conn.Close();
        }
      }
    }

    private List<string> GetRestaurantNameDESC(MySqlConnection conn)
    {
      //create a new list that stores the names of each restaurant
      List<string> restaurantNameList = new List<string>();

      int idCount = 1;

      for (int i = 0; i <= 100; i++)
      {
        //select the restaurant name based on reverse alphabetical order
        string stm = "SELECT RestaurantName " +
                      "FROM RestaurantProfiles " +
                      "WHERE id = @profileId " +
                      "ORDER BY RestaurantName DESC";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        if (rdr1.HasRows)
        {
          rdr1.Read();
          restaurantNameList.Add(rdr1["RestaurantName"].ToString());
        }
        rdr1.Close();

        idCount++;
      }

      return restaurantNameList;
    }

    private List<decimal> GetOverallRatingDESC(MySqlConnection conn)
    {
      decimal overallRating = 0.00m;

      //create a new list that stores the overall rating
      List<decimal> overallRatingList = new List<decimal>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the overall rating to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT OverallRating " +
                     "FROM RestaurantProfiles " +
                     "WHERE id = @profileId " +
                     "ORDER BY RestaurantName DESC";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          decimal.TryParse(rdr1["OverallRating"].ToString(),
                           out overallRating);

          //round the rating away from 0, e.g 2.49 = 2 and 2.5 = 3
          decimal roundedRating = Math.Round(overallRating, 0,
                                  MidpointRounding.AwayFromZero);

          overallRatingList.Add(roundedRating);
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return overallRatingList;
    }

    private List<string> GetRestaurantNameASC(MySqlConnection conn)
    {
      //create a new list that stores the names of each restaurant
      List<string> restaurantNameList = new List<string>();

      int idCount = 1;

      for (int i = 0; i <= 100; i++)
      {
        //select the restaurant name based on alphabetican order
        string stm = "SELECT RestaurantName " +
                      "FROM RestaurantProfiles " +
                      "WHERE id = @profileId " +
                      "ORDER BY RestaurantName ASC";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        if (rdr1.HasRows)
        {
          rdr1.Read();
          restaurantNameList.Add(rdr1["RestaurantName"].ToString());
        }
        rdr1.Close();

        idCount++;
      }

      return restaurantNameList;
    }

    private List<decimal> GetOverallRatingASC(MySqlConnection conn)
    {
      decimal overallRating = 0.00m;

      //create a new list that stores the overall rating of each restaurant
      List<decimal> overallRatingList = new List<decimal>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the overall rating
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT OverallRating " +
                     "FROM RestaurantProfiles " +
                     "WHERE id = @profileId " +
                     "ORDER BY RestaurantName ASC";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          decimal.TryParse(rdr1["OverallRating"].ToString(),
                           out overallRating);

          //round the rating away from 0, e.g 2.49 = 2 and 2.5 = 3
          decimal roundedRating = Math.Round(overallRating, 0,
                                  MidpointRounding.AwayFromZero);

          overallRatingList.Add(roundedRating);
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return overallRatingList;
    }
  }
}
