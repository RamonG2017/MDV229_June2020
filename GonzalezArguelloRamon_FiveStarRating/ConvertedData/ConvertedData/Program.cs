/******************************************************************************
filename   Program.cs
author     Ramon Gonzalez Arguello
email      rgonzalezarguello@student.fullsail.edu
course     MDV229
section    01
assignment 1       
due date   06/07/2020
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConvertedData
{
  class MainClass
  {
    public static void Main(string[] args)
    {
      // Display the menu by calling the method inside the Menu class
       MainMenu.DisplayMenu();
    }
  }
}
