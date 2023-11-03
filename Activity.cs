using System;
using System.Collections.Generic;
using System.Text;

namespace SerraOgutcenLab3
{
    class Activity
    {
        // unique counter for each instance, incremented by one at each instance creation (see constructors)
        public static int counter = 1;

        private int ActivityID { get; }
        private string ActivityName { get; set; }
        private string ActivityType { get; set; }
        private DateTime From { get; set; }
        private DateTime To { get; set; }

        public int GetID() { return ActivityID; }
        public DateTime GetFrom() { return From; }
        public DateTime GetTo() { return To; }

        public void SetName(string name) { ActivityName = name; }
        public void SetType(string type) { ActivityType = type; }
        public void SetFrom(DateTime from) { From = from; }
        public void SetTo(DateTime to) { To = to; }

        public Activity()
        {
            ActivityID = counter;
            ActivityName = "";
            ActivityType = "";
            From = DateTime.Now;
            To = DateTime.Now;

            counter++;
        }

        public Activity(string name, string type, DateTime from, DateTime to)
        {
            ActivityID = counter;
            ActivityName = name;
            ActivityType = type;
            From = from;
            To = to;

            counter++;
        }

        public bool Validate()
        {
            // check if From is before To
            if (DateTime.Compare(this.From, this.To) >= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tThe start time must be before end time!");
                Console.ResetColor();
                return false;
            }
            // check if From and To are in the same day
            if (this.From.ToString("MM/dd/yyyy") != this.To.ToString("MM/dd/yyyy"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tAn activity must start and end on the same day!");
                Console.ResetColor();
                return false;
            }
            // check if the date is past
            if (DateTime.Compare(this.From, DateTime.Now) < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tActivities cannot start in the past!");
                Console.ResetColor();
                return false;
            }

            // all checks are OK
            return true;
        }

        public bool IsClashing(Activity act2)
        {
            return (DateTime.Compare(this.From, act2.To) < 0) && (DateTime.Compare(this.To, act2.From) > 0);
        }

        public override string ToString()
        {
            return "ID: " + ActivityID +
                 ", Day: " + From.ToString("MM/dd/yyyy") +
                 ", Time: " + From.ToString("hh:mm tt") + " - " + To.ToString("hh:mm tt") +
                 ", Name: " + ActivityName + ", Type: " + ActivityType;
        }

    }
}
