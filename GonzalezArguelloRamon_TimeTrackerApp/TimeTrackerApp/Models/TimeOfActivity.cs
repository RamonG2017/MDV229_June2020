using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerApp.Models
{
    // Time Of Activity Entity
  public class TimeOfActivity
  {
    public int Id { get; set; }
    public decimal TimeSpentOnActivity { get; set; }
  }
}