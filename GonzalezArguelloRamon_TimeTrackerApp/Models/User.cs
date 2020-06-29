using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public User()
        {
            Id = default(int);
            FirstName = string.Empty;
            LastName = string.Empty;
            Password = string.Empty;
        }
    }
}