using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerApp.Constants
{
  public static class WindowSizes
  {
    public static readonly int REGULAR_HEIGHT = Console.LargestWindowHeight - 5;

    /* 
     * Finding a way to run the following code with visual studio mad:
     * Console.WindowHeight = WindowSizes.REGULAR_HEIGHT;
     */
  }
}