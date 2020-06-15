/******************************************************************************
filename   SortByXMenu.cs
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
  public class SortByXMenu
  {
    public static void SortByXMenuStart()
    {
      string option = "0";

      do
      {
        Console.Clear();

        Console.WriteLine("\r\n");

        Console.WriteLine("\t[1] Show the Best");
        Console.WriteLine("\t[2] Show 4 Stars and Up");
        Console.WriteLine("\t[3] Show 3 Stars and Up");
        Console.WriteLine("\t[4] Show the Worst");
        Console.WriteLine("\t[5] Show Unrated");

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
            ShowBestRestaurants.ShowBestRestaurantsStart();
            Console.ReadKey();
            break;
          case "2":
            Console.Clear();
            ShowFourToFive.ShowFourToFiveStart();
            Console.ReadKey();
            break;
          case "3":
            Console.Clear();
            ShowThreeToFive.ShowThreeToFiveStart();
            Console.ReadKey();
            break;
          case "4":
            Console.Clear();
            ShowWorst.ShowWorstStart();
            Console.ReadKey();
            break;
          case "5":
            Console.Clear();
            ShowUnrated.ShowUnratedStart();
            Console.ReadKey();
            break;
          case "6":
            Console.Clear();
            Console.WriteLine("Press the return key to go back to " +
                              "the Rating menu...");
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
