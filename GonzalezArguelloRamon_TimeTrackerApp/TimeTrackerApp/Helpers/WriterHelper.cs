using System;

namespace TimeTrackerApp.Helpers
{
  public static class WriterHelper
  {
    public static void RedText(string text, int? times)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      if (times != null)
      {
        for (var i = 0; i <= times; i++) Console.Write(text);
        Console.WriteLine("");
      }
      else
      {
        Console.WriteLine(text);
      }

      Console.ResetColor();
    }

    public static void CyanText(string text, int? times)
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      if (times != null)
      {
        for (var i = 0; i <= times; i++) Console.Write(text);
        Console.WriteLine("");
      }
      else
      {
        Console.WriteLine(text);
      }

      Console.ResetColor();
    }
    public static void WhiteText(string text, int? times)
    {
      Console.ForegroundColor = ConsoleColor.White;
      if (times != null)
      {
        for (var i = 0; i <= times; i++) Console.Write(text);
        Console.WriteLine("");
      }
      else
      {
        Console.WriteLine(text);
      }

      Console.ResetColor();
    }
  }
}