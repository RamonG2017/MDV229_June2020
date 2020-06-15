using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;

//Database Location
﻿﻿//string cs = @"server= 127.0.0.1;userid=root;password=root;database=SampleRestaurantDatabase;port=8889";

//Output Location
//string _directory = @"../../output/";﻿﻿

namespace ConvertedData
{
  public class JSON_Convert
  {
    public void JSON_ConvertStart()
    {
      string cs = @"server=127.0.0.1;userid=dbremoteuser;password=password;database=SampleRestaurantDatabase;port=8889";

      MySqlConnection conn = null;

      Console.Clear();

      try
      {
        //since the overall possible rating is always 5 there is no
        //need to make a list
        decimal overallPossibleRating = 5.00m;

        //create new lists to store the values
        List<string> restaurantNames = new List<string>();
        List<string> address = new List<string>();
        List<string> phoneNumbers = new List<string>();
        List<string> hoursOfOperation = new List<string>();
        List<string> priceRange = new List<string>();
        List<string> location = new List<string>();
        List<string> cuisine = new List<string>();
        List<decimal> foodRating = new List<decimal>();
        List<decimal> serviceRating = new List<decimal>();
        List<decimal> ambienceRating = new List<decimal>();
        List<decimal> valueRating = new List<decimal>();
        List<decimal> overallRating = new List<decimal>();

        conn = new MySqlConnection(cs);
        conn.Open();

        //append the returned list from the private methods into these lists
        restaurantNames.AddRange(GetRestaurantName(conn));
        address.AddRange(GetAddress(conn));
        phoneNumbers.AddRange(GetPhone(conn));
        hoursOfOperation.AddRange(GetTime(conn));
        priceRange.AddRange(GetPrice(conn));
        location.AddRange(GetLocation(conn));
        cuisine.AddRange(GetCuisine(conn));
        foodRating.AddRange(GetFoodRating(conn));
        serviceRating.AddRange(GetServiceRating(conn));
        ambienceRating.AddRange(GetAbienceRating(conn));
        valueRating.AddRange(GetValueRating(conn));
        overallRating.AddRange(GetOverallRating(conn));

        //Start of the JSON array

        string JSON = "Restaurant Reviews: ["; 

        for (int i = 0; i < 100; i++)
        {

          JSON += "\n\t{";

          JSON += "\n\t\t\""  + "\"id:\"" + (i + 1) + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Restaurant Name:\"" +
                  restaurantNames[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Address:\"" +
                  address[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Phone:\"" +
                  phoneNumbers[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Hours Of Operation:\"" +
                  hoursOfOperation[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Price Range:\"" +
                  priceRange[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Location:\"" +
                  location[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Cuisine:\"" +
                  cuisine[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Food Rating:\"" +
                  foodRating[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Service Rating:\"" +
                  serviceRating[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Ambience Rating:\"" +
                  ambienceRating[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Value Rating:\"" +
                  valueRating[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Overall Rating:\"" +
                  overallRating[i] + "\"";

          JSON += ",";

          JSON += "\n\t\t\"" + "\"Overall Possible Rating:\"" +
                  overallPossibleRating + "\"";

          JSON += "\n\t}";

          if (i >= 0 && i < 99)
          {
            JSON += ",";
          }
        }

        //End of the JSON array
        JSON += "\n]"; 

        // Writing JSON data to output folder file

        File.WriteAllText(@"../../../output/GonzalezArguelloRamon_ConvertedData.json", JSON);

        Console.WriteLine("File has been created!");

        Console.WriteLine("\r\n");

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

    private List<string> GetRestaurantName(MySqlConnection conn)
    {
      //create a new list that stores the phone of each restaurant
      List<string> restaurantNameList = new List<string>();

      int idCount = 1;

      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT RestaurantName " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";
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

    private List<string> GetAddress(MySqlConnection conn)
    {
      //create a new list that stores the addresses of each restaurant
      List<string> addressList = new List<string>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT Address " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";
        MySqlCommand cmd = new MySqlCommand(stm, conn);

        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        if (rdr1.HasRows)
        {
          rdr1.Read();
          addressList.Add(rdr1["Address"].ToString());
        }
        rdr1.Close();

        idCount++;
      }

      return addressList;
    }

    private List<string> GetPhone(MySqlConnection conn)
    {
      //create a new list that stores the phone of each restaurant
      List<string> phoneList = new List<string>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT Phone " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          phoneList.Add(rdr1["Phone"].ToString());
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return phoneList;
    }

    private List<string> GetTime(MySqlConnection conn)
    {
      //create a new list that stores the hour of operatons of each restaurant
      List<string> hoursOfOperation = new List<string>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT HoursOfOperation " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          hoursOfOperation.Add(rdr1["HoursOfOperation"].ToString());
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return hoursOfOperation;
    }

    private List<string> GetPrice(MySqlConnection conn)
    {
      //create a new list that stores the price range of each restaurant
      List<string> priceRange = new List<string>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT Price " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          priceRange.Add(rdr1["Price"].ToString());
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return priceRange;
    }

    private List<string> GetLocation(MySqlConnection conn)
    {
      //create a new list that stores the location of each restaurant
      List<string> restaurantLocation = new List<string>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT USACityLocation " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          restaurantLocation.Add(rdr1["USACityLocation"].ToString());
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return restaurantLocation;
    }

    private List<string> GetCuisine(MySqlConnection conn)
    {
      //create a new list that stores the cuisine of each restaurant
      List<string> restaurantCuisine = new List<string>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT Cuisine " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          restaurantCuisine.Add(rdr1["Cuisine"].ToString());
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return restaurantCuisine;
    }

    private List<decimal> GetFoodRating(MySqlConnection conn)
    {
      decimal foodRating = 0.00m;

      //create a new list that stores the cuisine of each restaurant
      List<decimal> foodRatingList = new List<decimal>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT FoodRating " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          decimal.TryParse(rdr1["FoodRating"].ToString(), out foodRating);
          foodRatingList.Add(foodRating);
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return foodRatingList;
    }

    private List<decimal> GetServiceRating(MySqlConnection conn)
    {
      decimal serviceRating = 0.00m;

      //create a new list that stores the cuisine of each restaurant
      List<decimal> serviceRatingList = new List<decimal>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT ServiceRating " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          decimal.TryParse(rdr1["ServiceRating"].ToString(), out serviceRating);
          serviceRatingList.Add(serviceRating);
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return serviceRatingList;
    }

    private List<decimal> GetAbienceRating(MySqlConnection conn)
    {
      decimal abienceRating = 0.00m;

      //create a new list that stores the cuisine of each restaurant
      List<decimal> abienceRatingList = new List<decimal>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT AmbienceRating " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          decimal.TryParse(rdr1["AmbienceRating"].ToString(),
                           out abienceRating);
          abienceRatingList.Add(abienceRating);
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return abienceRatingList;
    }

    private List<decimal> GetValueRating(MySqlConnection conn)
    {
      decimal valueRating = 0.00m;

      //create a new list that stores the cuisine of each restaurant
      List<decimal> valueRatingList = new List<decimal>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT ValueRating " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

        MySqlCommand cmd = new MySqlCommand(stm, conn);

        //add the id count into the profileId to filter on SQL
        cmd.Parameters.AddWithValue("@profileId", idCount);

        MySqlDataReader rdr1 = cmd.ExecuteReader();

        //only read if it has rows
        if (rdr1.HasRows)
        {
          rdr1.Read();
          decimal.TryParse(rdr1["ValueRating"].ToString(),
                           out valueRating);
          valueRatingList.Add(valueRating);
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return valueRatingList;
    }

    private List<decimal> GetOverallRating(MySqlConnection conn)
    {
      decimal overallRating = 0.00m;

      //create a new list that stores the cuisine of each restaurant
      List<decimal> overallRatingList = new List<decimal>();

      //used to add the value of the id to filter in SQL
      int idCount = 1;

      //loops and adds the phone number to the list
      for (int i = 0; i <= 100; i++)
      {
        string stm = "SELECT OverallRating " +
                   "FROM RestaurantProfiles " +
                   "WHERE id = @profileId";

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
          overallRatingList.Add(overallRating);
        }
        rdr1.Close(); //close the reader

        idCount++;
      }

      return overallRatingList;
    }
  }
}
