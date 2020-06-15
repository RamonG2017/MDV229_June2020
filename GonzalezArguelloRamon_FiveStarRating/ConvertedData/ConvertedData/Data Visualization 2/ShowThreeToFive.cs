/******************************************************************************
filename   ShowThreeToFive.cs
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
  public class ShowThreeToFive
  {
    public static void ShowThreeToFiveStart()
    {
      string cs = @"server=127.0.0.1;userid=dbremoteuser;password=password;database=SampleRestaurantDatabase;port=8889";

      MySqlConnection conn = null;

      try
      {
        conn = new MySqlConnection(cs);
        conn.Open();

        decimal overallRating = 0.00m;

        //create a new list that stores the overall rating
        List<decimal> overallRatingList = new List<decimal>();

        //loops and adds the overall rating to the list
        for (int i = 0; i < 48; i++)
        {
          string stm = "SELECT OverallRating " +
                       "FROM RestaurantProfiles " +
                       "WHERE OverallRating BETWEEN 2.5 AND 5 " +
                       "ORDER BY OverallRating ASC";

          MySqlCommand cmd = new MySqlCommand(stm, conn);

          MySqlDataReader rdr1 = cmd.ExecuteReader();

          while (rdr1.Read())
          {
            decimal.TryParse(rdr1["OverallRating"].ToString(),
                             out overallRating);

            decimal roundedRating = Math.Round(overallRating, 0,
                                   MidpointRounding.AwayFromZero);

            overallRatingList.Add(roundedRating);
          }
          rdr1.Close(); //close the reader
        }

        //create a new list that stores the names of each restaurant
        List<string> restaurantNameList = new List<string>();

        for (int j = 0; j < 48; j++)
        {
          //select the restaurant name based on overall rating ASC
          string stm2 = "SELECT RestaurantName " +
                        "FROM RestaurantProfiles " +
                        "WHERE OverallRating BETWEEN 2.5 AND 5 " +
                        "ORDER BY OverallRating ASC";

          MySqlCommand cmd2 = new MySqlCommand(stm2, conn);


          MySqlDataReader rdr2 = cmd2.ExecuteReader();

          while (rdr2.Read())
          {
            restaurantNameList.Add(rdr2["RestaurantName"].ToString());
          }
          rdr2.Close(); //close the reader
        }

        for (int i = 0; i < 48; i++)
        {
          decimal rating = overallRatingList[i];
          string restNumber = (i + 1) + ". ";


          Console.WriteLine("=============================================" +
                          "=========================");

          Console.Write("{0, 5}", restNumber);
          Console.Write("{0, 5}", restaurantNameList[i]);
          Console.Write("{0, 10}", "Rating: ");
          Console.WriteLine(DrawStars.DrawStarsStart(rating));


          if (i == 47)
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
  }
}
