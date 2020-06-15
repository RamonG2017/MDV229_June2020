/******************************************************************************
filename   FiveStarMenu.cs
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
  public class FiveStarRatingMenu
  {
    public static void FiveStarRatingMenuStart(string name)
    {
      string option = "0";

      do
      {
        Console.Clear();

        Console.WriteLine("Hello " + name + ", How would you like to " +
        "sort the data?");

        Console.WriteLine("\r\n");

        Console.WriteLine("\t[1] List Restaurants Alphabetically");
        Console.WriteLine("\t[2] List Restaurants in Reverse Alphabetical");
        Console.WriteLine("\t[3] Sort Restaurants From Best to Worst");
        Console.WriteLine("\t[4] Sort Restaurants From Worst to Best");
        Console.WriteLine("\t[5] Show Only X and Up");

        Console.Write("\r\n");

        Console.WriteLine("\t[6] Exit");

        Console.Write("\r\n");

        Console.WriteLine("=============================================" +
                          "=========================");

        Console.Write("Make your selection: ");

        //read whatever the user inputs and uses it to access the options given
        option = Console.ReadLine();

        switch (option)
        {
          case "1":
            Console.Clear();
            RestaurantAlphabetical restaurant = new RestaurantAlphabetical();
            restaurant.RestaurantAlphabeticalStart();
            Console.ReadKey();
            break;
          case "2":
            Console.Clear();
            RestaurantAlphabetical restaurant1 =
                                          new RestaurantAlphabetical();
            restaurant1.RestaurantReverseAlphabeticalStart();
            Console.ReadKey();
            break;
          case "3":
            Console.Clear();
            SortRestaurants sortRestaurants = new SortRestaurants();
            sortRestaurants.SortBestToWorstStart();
            Console.ReadKey();
            break;
          case "4":
            Console.Clear();
            SortRestaurants sortRestaurants1 = new SortRestaurants();
            sortRestaurants1.SortWorstToBestStart();
            Console.ReadKey();
            break;
          case "5":
            Console.Clear();
            SortByXMenu.SortByXMenuStart();
            Console.ReadKey();
            break;
          case "6":
            Console.Clear();
            Console.WriteLine("Press the return key to go back to " +
                              "the main menu...");
            break;
          default:
            Console.Clear();
            Console.WriteLine("Please enter a valid option!");
            Console.ReadKey();
            break;
        }
      } while (option != "6");
    }
  }
}
