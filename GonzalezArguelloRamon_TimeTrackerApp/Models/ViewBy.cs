using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerApp.Models
{
    public class ViewBy
    {
        public int DayId { get; set; }
        public string DayName { get; set; }
        public int CalendarDateId { get; set; }
        public DateTime CalendarDate { get; set; }
        public int ActivityTimeId { get; set; }
        public decimal TimeSpentOnActivity { get; set; }
        public int CalendarDayId { get; set; }
        public int CalendarNumericalDay { get; set; }
        public int ActivityDescriptionId { get; set; }
        public string ActivityDescription { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public int ActivityCategoryId { get; set; }
        public string CategoryDescription { get; set; }

        public string FormattedDate()
        {
            return CalendarDate.ToString("dd MMMM yyyy");
        }
    }
}
