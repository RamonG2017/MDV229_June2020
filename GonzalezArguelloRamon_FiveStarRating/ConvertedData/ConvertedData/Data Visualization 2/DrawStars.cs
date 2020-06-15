/******************************************************************************
filename   DrawStars.cs
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
using System.Drawing;

namespace ConvertedData
{
  public class DrawStars
  {
    public static string DrawStarsStart(decimal rating)
    {
      string zero = "No Rating";
      string oneStar = "*";
      string twoStars = "**";
      string threeStars = "***";
      string fourStars = "****";
      string fiveStars = "*****";

      if (rating == 0)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(zero);

        Console.ResetColor();
      }
      else if (rating == 1)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(oneStar);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(fourStars);

        Console.ResetColor();
      }
      else if (rating == 2)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Write(twoStars);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(threeStars);

        Console.ResetColor();
      }
      else if (rating == 3)
      {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(threeStars);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(twoStars);

        Console.ResetColor();
      }
      else if (rating == 4)
      {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(fourStars);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Write(oneStar);

        Console.ResetColor();
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(fiveStars);

        Console.ResetColor();
      }

      return null;
    }
  }
}
