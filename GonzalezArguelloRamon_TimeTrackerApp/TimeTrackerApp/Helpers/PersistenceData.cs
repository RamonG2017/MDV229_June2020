using System;
using System.Collections.Generic;
using System.Text;
using TimeTrackerApp.Models;

namespace TimeTrackerApp.Helpers
{
    // User Persistence Data
  public sealed class PersistenceData
  {
    public User ActiveUser { get; set; }
    public string TempMessage { get; set; }
    public string TempResult { get; set; }

    private PersistenceData()
    {
      ActiveUser = new User();
    }

    private static PersistenceData _instance = null;

    public static PersistenceData Instance
    {
      get
      {
        if (_instance == null) _instance = new PersistenceData();
        return _instance;
      }
    }
  }
}