using System;
using System.Linq;
using TimeTrackerApp.Helpers;

namespace TimeTrackerApp.Screens
{
    public static class Activities
    {
        private static readonly DbHelper DbHelper = new DbHelper();
        private static readonly ActivityHelper ActivityHelper = new ActivityHelper();
        private static PersistenceData PersistenceData = PersistenceData.Instance;

        private static void Header()
        {
            Console.Clear();
            Shared.Header();
            Console.WriteLine("Enter Activity\n");
            Console.WriteLine("[0] Exit");
            WriterHelper.RedText("=", 119);
        }

        public static void Add()
        {
            #region --- Get Activities ---

            Header();
            var activities = DbHelper.Activities();
            activities.ForEach(c => Console.WriteLine($"[{c.Id}] {c.ActivityDescription}"));
            WriterHelper.RedText("=", 119);
            Console.Write("Select A Category: ");
            var option1 = Console.ReadLine();
            var step1 = Convert.ToInt32(option1);
            var selectedCategory = activities.FirstOrDefault(c => c.Id == step1);
            if (step1 == 0)
                Shared.MainMenu();
            var dates = DbHelper.CalendarDates();

            #endregion

            #region --- Get Date ---

            Header();
            dates.ForEach(d => Console.WriteLine($"[{d.Id}] {d.FormattedDate()}"));
            WriterHelper.RedText("=", 119);
            Console.Write("Select A Date: ");
            var option2 = Console.ReadLine();
            var step2 = Convert.ToInt32(option2);
            var selectedDate = dates.FirstOrDefault(d => d.Id == step2);
            if (step2 == 0)
                Shared.MainMenu();

            #endregion

            #region --- Get Time Spent ---

            Header();
            var timeOfActivities = DbHelper.TimeOfActivities();
            timeOfActivities.ForEach(toa => Console.WriteLine($"[{toa.Id}] {toa.TimeSpentOnActivity}"));
            WriterHelper.RedText("=", 119);
            Console.Write("How Long Did You Perform That Activity?: ");
            var option3 = Console.ReadLine();
            var step3 = Convert.ToInt32(option3);
            var selectedTime = timeOfActivities.FirstOrDefault(s => s.Id == step3);

            #endregion

            // insert row in database
            var result = ActivityHelper.LogActivity(selectedCategory, selectedDate, selectedTime);
            if (result > 0)
                PersistenceData.TempMessage = "Your activity has successfully been added!";
            else
                PersistenceData.TempMessage = "Something went wrong...";
            Shared.Welcome();
        }
    }
}