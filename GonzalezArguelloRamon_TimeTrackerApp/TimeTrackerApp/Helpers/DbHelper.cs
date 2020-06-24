using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using TimeTrackerApp.Models;

namespace TimeTrackerApp.Helpers
{
    // MySQL Data Access Layer
    //returns: DbHelper Data Context
  public class DbHelper
  {
    public DbHelper()
  {
      try
      {
        ConnectionString = ConfigurationManager.ConnectionStrings["time_tracker_app"].ConnectionString;
        Connection = new MySqlConnection(ConnectionString);
      }
      catch (MySqlException e)
      {
        throw new ConfigurationErrorsException(e.Message);
      }
    }

    private string ConnectionString { get; }
    private MySqlConnection Connection { get; }

    public string Test()
    {
        try
        {
          Connection.Open();
          return $"Connection Successful to MySQL Server version: {Connection.ServerVersion}";
        }
        catch (Exception e)
        {
          return e.Message;
        }
    }

      // Execute DML Queries against the database
      // returns: Number of modified rows and -1 if fail
    private int ExecuteNonQuery(string query)
    {
      var command = new MySqlCommand(query, Connection);
      command.Connection.Open();

      var result = command.ExecuteNonQuery();
      command.Connection.Close();
      return result;
    }

    public int Insert(string query)
    {
      return ExecuteNonQuery(query);
    }

    public int Update(string query)
    {
      return ExecuteNonQuery(query);
    }

    public int Delete(string query)
    {
      return ExecuteNonQuery(query);
    }

      // Execute DDL query against the database 
      // returns: MySQL Reader Cursor 
    private MySqlDataReader ExecuteReader(string query)
    {
      var command = new MySqlCommand(query, Connection)
                    {CommandType = CommandType.Text};
      Connection.Open();

      var result = command.ExecuteReader();
      return result;
    }

      // Get all users in table time_tracker_users
      // returns:User Model List
    public List<User> Users()
    {
      var users = new List<User>();
      var query = "SELECT user_id, user_password, user_firstname, " +
                  "user_lastname FROM time_tracker_users";
      var result = ExecuteReader(query);

      while (result.Read())
      {
        users.Add(new User
        {
          Id = Convert.ToInt32(result["user_id"]),
          FirstName = result["user_firstname"]?.ToString(),
          LastName = result["user_lastname"]?.ToString(),
          Password = result["user_password"]?.ToString()
        });
      }
      Connection.Close();
      return users;
    }

      // Get all categories of activities available in categories_of_activities
      // returns:Categories of Activities List
    public List<CategoryOfActivity> Categories()
    {
      var categories = new List<CategoryOfActivity>();
      var query = "SELECT activity_category_id, category_description " +
                  "FROM activity_categories";
      var result = ExecuteReader(query);

      while (result.Read())
      {
        categories.Add(new CategoryOfActivity
        {
          Id = Convert.ToInt32(result["activity_category_id"]),
          CategoryDescription = result["category_description"]?.ToString()
        });
      }
      Connection.Close();
      return categories;
    }

    public List<CalendarDate> CalendarDates()
    {
      var calendarDates = new List<CalendarDate>();
      var query = "SELECT calendar_date_id, calendar_date " +
                  "FROM tracked_calendar_dates";
      var result = ExecuteReader(query);

      while (result.Read())
      {
        calendarDates.Add(new CalendarDate
        {
          Id = Convert.ToInt32(result["calendar_date_id"]),
          Date = Convert.ToDateTime(result["calendar_date"])
        });
      }
      Connection.Close();
      return calendarDates;
    }

    public List<TimeOfActivity> TimeOfActivities()
    {
      var timeOfActivities = new List<TimeOfActivity>();
      var query = "SELECT activity_times_id, time_spent_on_activity " +
                  "FROM activity_times";
      var result = ExecuteReader(query);

      while (result.Read())
      {
        timeOfActivities.Add(new TimeOfActivity
        {
          Id = Convert.ToInt32(result["activity_times_id"]),
          TimeSpentOnActivity = Convert.ToDecimal(result["time_spent_on_activity"])
        });
      }
      Connection.Close();
      return timeOfActivities;
    }

    public List<DescriptionOfActivity> Activities()
    {
      var descriptionOfActivities = new List<DescriptionOfActivity>();
      var query = "SELECT activity_description_id, activity_description " +
                  "FROM activity_descriptions";
      var result = ExecuteReader(query);

      while (result.Read())
      {
        descriptionOfActivities.Add(new DescriptionOfActivity
        {
          Id = Convert.ToInt32(result["activity_description_id"]),
          ActivityDescription = result["activity_description"]?.ToString()
        });
      }
      Connection.Close();
      return descriptionOfActivities;
    }

    public List<ViewBy> ViewBy()
    {
      var viewBy = new List<ViewBy>();

        //by prepend @ at a string we tell the compiler this is a multiline string
      var query = @"SELECT dw.day_id, 
              dw.day_name, 
              cd.calendar_date_id, 
              cd.calendar_date, 
              act.activity_times_id, 
              act.time_spent_on_activity, 
              cda.calendar_day_id, 
              cda.calendar_numerical_day, 
              ad.activity_description_id, 
              ad.activity_description, 
              ttu.user_id, 
              ttu.user_firstname, 
              aac.activity_category_id, 
              aac.category_description 
      FROM   activity_log al 
              INNER JOIN days_of_week dw 
                      ON dw.day_id = al.day_name 
              INNER JOIN tracked_calendar_dates cd 
                      ON cd.calendar_date_id = al.calendar_date 
              INNER JOIN activity_times act 
                      ON act.activity_times_id = al.time_spent_on_activity 
              INNER JOIN tracked_calendar_days cda 
                      ON cda.calendar_day_id = al.calendar_day 
              INNER JOIN activity_descriptions ad 
                      ON ad.activity_description_id = al.activity_description 
              INNER JOIN time_tracker_users ttu 
                      ON ttu.user_id = al.user_id 
              INNER JOIN activity_categories aac 
                      ON aac.activity_category_id = al.category_description";
      var result = ExecuteReader(query);

      while (result.Read())
      {
        viewBy.Add(new ViewBy
        {
          DayId = Convert.ToInt32(result["day_id"]),
          DayName = result["day_name"]?.ToString(),
          CalendarDateId = Convert.ToInt32(result["calendar_date_id"]),
          CalendarDate = Convert.ToDateTime(result["calendar_date"]),
          ActivityTimeId = Convert.ToInt32(result["activity_times_id"]),
          TimeSpentOnActivity = Convert.ToDecimal(result["time_spent_on_activity"]),
          CalendarDayId = Convert.ToInt32(result["calendar_day_id"]),
          CalendarNumericalDay = Convert.ToInt32(result["calendar_numerical_day"]),
          ActivityDescriptionId = Convert.ToInt32(result["activity_description_id"]),
          ActivityDescription = result["activity_description"]?.ToString(),
          UserId = Convert.ToInt32(result["user_id"]),
          UserFirstName = result["user_firstname"]?.ToString(),
          ActivityCategoryId = Convert.ToInt32(result["activity_category_id"]),
          CategoryDescription = result["category_description"]?.ToString()
        });
      }
      Connection.Close();
      return viewBy;
    }
  }
}