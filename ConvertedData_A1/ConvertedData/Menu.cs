using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConvertedData
{
  public class Menu
  {
    public static void DisplayMenu()
    {
      string option = "0";

      //clear the console so that the menu is the only thing displayed
      Console.Clear();

      do
      {
        Console.Clear();

        Console.WriteLine("Hello Admin, What Would You Like To Do Today ?");

        Console.WriteLine("\r\n");

        Console.WriteLine("\t[1] Convert The Restaurant Profile Table " +
                          "From SQL To JSON");
        Console.WriteLine("\t[2] Showcase Our 5 Star Rating System");
        Console.WriteLine("\t[3] Showcase Our Animated Bar Graph Review " +
                          "System");
        Console.WriteLine("\t[4] Play A Card Game");

        Console.Write("\r\n");

        Console.WriteLine("\t[5] Exit");

        Console.Write("\r\n");

        Console.WriteLine("=============================================" +
                          "=========================");

        Console.Write("Make your selection: ");

        //read whatever the user inputs and uses it to access the options given
        option = Console.ReadLine();

        switch (option)
        {
          case "1":
            JSON_Convert jSON_Convert = new JSON_Convert();
            jSON_Convert.JSON_ConvertStart();
            Console.ReadKey();
            break;
          case "2":
            Console.Clear();
            Console.WriteLine("This option has yet to be implemented, " +
              "sorry about that.");
            Console.ReadKey();
            break;
          case "3":
            Console.Clear();
            Console.WriteLine("This option has yet to be implemented, " +
              "sorry about that.");
            Console.ReadKey();
            break;
          case "4":
            Console.Clear();
            Console.WriteLine("This option has yet to be implemented, " +
              "sorry about that.");
            Console.ReadKey();
            break;
          case "5":
            Console.Clear();
            Console.WriteLine("Goodbye...");
            Console.ReadKey();
            break;
          default:
            Console.Clear();
            Console.WriteLine("Please enter a valid option!");
            Console.ReadKey();
            break;
        }
      } while (option != "5");
    }
  }
}
