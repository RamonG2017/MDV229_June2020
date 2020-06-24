﻿using System;
using TimeTrackerApp.Helpers;
using TimeTrackerApp.Models;

namespace TimeTrackerApp.Screens
{
    // Shared Screen Sections
  public static class Shared
  {
    private static PersistenceData PersistenceData = PersistenceData.Instance;

      // Main welcome banner
    public static void Header()
    {
      WriterHelper.RedText("=", 119);
      WriterHelper.CyanText("Time Tracker App", null);
      WriterHelper.RedText("=", 119);
    }

      // Login Screen
    public static void Login()
    {
      var loginHelper = new LoginHelper();

      Console.Write("First Name: ");
      var firstName = Console.ReadLine()?.Trim();

      Console.Write("Last Name: ");
      var lastName = Console.ReadLine()?.Trim();

      Console.Write("Password: ");
      var password = Console.ReadLine()?.Trim();

        /*
         * search user in database, this will override the original value
         * and change the Id from 0 to a greater number if found.
         */
      PersistenceData.ActiveUser = loginHelper.IsValid(firstName, lastName,
          password);
      Welcome();
    }

     //Main Menu
    public static void MainMenu()
    {
      int? selection = null;
      Console.Clear();
      Header();

      Console.WriteLine("Main Menu \n");
      Console.WriteLine("[1] Enter Activity");
      Console.WriteLine("[2] View Tracked Data");
      Console.WriteLine("[3] Run Calculations \n");
      Console.WriteLine("[0] Exit");

      WriterHelper.RedText("=", 119);

        // is user logged in? yes, then show name else show temp message;
      Console.WriteLine(!string.IsNullOrWhiteSpace(PersistenceData.TempMessage)
          ? PersistenceData.TempMessage
          : $"Welcome {PersistenceData.ActiveUser.FirstName}");

      PersistenceData.TempMessage = string.Empty;
            
      Console.Write("Selection: ");
      var read = Console.ReadLine();
      selection = Convert.ToInt32(read);

        // What the user wants to do now
      switch (selection)
      {
        case 1:
          Activities.Add();
          break;
        case 2:
          TrackedData.ViewBy();
          break;
        case 3:
          Calculations.View();
          break;
        case 0:
          Console.Clear();
          Console.WriteLine("Goodbye...");
          Console.ReadLine();
          PersistenceData.ActiveUser = new User();
          Environment.Exit(0);
          break;
        default:
          Console.WriteLine("Invalid selection, try again");
          Console.ReadLine();
          MainMenu();
          break;
        }
    }

      // Welcome screen
    public static void Welcome()
    {
      Console.Clear(); // clear everything before start
      Header(); // print application header

        // is there any active user?
      if (PersistenceData.ActiveUser.Id > 0)
      {
          /*
           * The above validation works because we add a constructor in the
           * User class, when instantiated the Id will be equal to 0
           * and when we override its value from the database will always
           * be distinct from 0 as the Auto Increment value starts from 1
           */
        MainMenu();
      }
      else
      {
        Login();
      }
    }
  }
}