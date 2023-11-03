using System;
using System.Collections.Generic;
using System.Text;

namespace SerraOgutcenLab3
{
    class Calendar
    {
        private Dictionary<DateTime, List<Activity>> Activities { get; set; }

        private string CalendarOwner { get; set; }

        public Calendar()
        {
            Activities = new Dictionary<DateTime, List<Activity>>();
            CalendarOwner = "";
        }
        public Calendar(string name)
        {
            Activities = new Dictionary<DateTime, List<Activity>>();
            CalendarOwner = name;
        }

        public Activity SearchActivity(int actId)
        {
            foreach (DateTime key in Activities.Keys)
            {
                foreach (Activity item in Activities[key])
                {
                    if (item.GetID() == actId)
                        return item;
                }
            }
            return null;
        }

        public void AddActivity(Activity act)
        {
            Console.WriteLine("Attempting to add the following activity to " + CalendarOwner + "'s calendar:");
            Console.WriteLine("\t" + act.ToString());

            if (!act.Validate())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tFailed to add activity!\n");
                Console.ResetColor();
                return;
            }

            DateTime dateKey = new(act.GetFrom().Year, act.GetFrom().Month, act.GetFrom().Day);

            if (!Activities.ContainsKey(dateKey))
                Activities.Add(dateKey, new List<Activity>());
            else
            {
                // check if there are any clashes
                foreach (Activity item in Activities[dateKey])
                {
                    if (act.IsClashing(item))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\tThis activity clashes with activity {0} ", item.GetID());
                        Console.WriteLine("\tFailed to add activity! \n");
                        Console.ResetColor();
                        return;
                    }
                }
            }

            Activities[dateKey].Add(act);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tActivity added successfully!\n");
            Console.ResetColor();
        }

        public void RemoveActivity(int actIdToRemove)
        {
            Console.WriteLine("Attempting to remove the activity with ID " + actIdToRemove + " from " + CalendarOwner + "'s calendar.");
            Activity actToRemove = SearchActivity(actIdToRemove);
            if (actToRemove is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tAn activity with ID " + actIdToRemove + " was not found in " + CalendarOwner + "'s calendar.\n");
                Console.ResetColor();
                return;
            }
            foreach (DateTime key in Activities.Keys)
            {
                if (Activities[key].Contains(actToRemove))
                {
                    Activities[key].Remove(actToRemove);

                    if (Activities[key].Count == 0)
                        Activities.Remove(key);  // Delete key if empty

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\tThe following activity was successfully removed from " + CalendarOwner + "'s calendar:");
                    Console.WriteLine("\t" + actToRemove.ToString() + "\n");
                    Console.ResetColor();

                    return;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\tAn activity with ID " + actIdToRemove + " was not found in " + CalendarOwner + "'s calendar.\n");
            Console.ResetColor();
        }

        public void UpdateActivity(int actIdToUpdate, int param, string updateValue)
        {
            Console.WriteLine("Attempting to update the activity with ID " + actIdToUpdate + " within " + CalendarOwner + "'s calendar");
            Activity actToUpdate = SearchActivity(actIdToUpdate);
            if (actToUpdate is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tAn activity with ID " + actIdToUpdate + " was not found in " + CalendarOwner + "'s calendar.\n");
                Console.ResetColor();
                return;
            }
            else
            {
                Console.WriteLine("Activity found:\n\t" + actToUpdate.ToString());
                Console.WriteLine("\tAttempted change: " + (param == 1 ? "Name" : (param == 2 ? "Type" : (param == 3 ? "Start time" : "End time"))) + ", value: " + updateValue);
            }
            if (param == 1)    // Name
                actToUpdate.SetName(updateValue);
            else if (param == 2) // Type
                actToUpdate.SetType(updateValue);
            else if (param == 3 || param == 4) // Time
            {
                DateTime tempStart = actToUpdate.GetFrom();
                DateTime tempEnd = actToUpdate.GetTo();

                string[] parsed = updateValue.Split(":");
                int hour = Convert.ToInt32(parsed[0]);
                int minute = Convert.ToInt32(parsed[1]);

                DateTime newDate = new(actToUpdate.GetFrom().Year, actToUpdate.GetFrom().Month, actToUpdate.GetFrom().Day, hour, minute, 0);
                DateTime dateKey = new(actToUpdate.GetFrom().Year, actToUpdate.GetFrom().Month, actToUpdate.GetFrom().Day);

                if (param == 3)
                    actToUpdate.SetFrom(newDate);
                else
                    actToUpdate.SetTo(newDate);

                // Do validation
                if (!actToUpdate.Validate())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tFailed to add activity!\n");
                    Console.ResetColor();

                    // Revert changes
                    if (param == 3)
                        actToUpdate.SetFrom(tempStart);
                    else
                        actToUpdate.SetTo(tempEnd);
                    return;
                }

                // Check for clashes
                foreach (Activity item in Activities[dateKey])
                {
                    if (item.GetID() != actToUpdate.GetID() && actToUpdate.IsClashing(item))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\tThis activity clashes with another!");
                        Console.WriteLine("\tFailed to update activity!\n");
                        Console.ResetColor();

                        // Revert changes
                        if (param == 3)
                            actToUpdate.SetFrom(tempStart);
                        else
                            actToUpdate.SetTo(tempEnd);
                        return;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tActivity updated successfully!\n");
            Console.ResetColor();
        }

        public void ShareActivity(int actIdToShare, Calendar newCal)
        {
            if (null!=SearchActivity(actIdToShare)) { 
            Activity a = SearchActivity(actIdToShare);
            newCal.AddActivity(a);
        }
            else
            {
                Console.WriteLine("There is no activity with that id in this calendar!");
            }
        }

        public void SortByTime(DateTime Day) {

            for (int i= 1; i < Activities[Day].Count ; i++) {
                Console.WriteLine("this runs for {0}", CalendarOwner);

                Activity later = Activities[Day][i];
                DateTime laterfrom  = later.GetFrom();
                TimeSpan a = laterfrom.TimeOfDay;

                Activity earlier = Activities[Day][i - 1];
                DateTime earlierfrom = earlier.GetFrom();
                TimeSpan b = earlierfrom.TimeOfDay;

                if (a <b)

                {
                    Console.WriteLine("oldu");
                    Activity temp = Activities[Day][i];
                    Activities[Day][i] = Activities[Day][i - 1];
                    Activities[Day][i - 1] = temp;
                }
                
            }
        }

        public override string ToString()
        {
            string finalstring = new('-', 100);
            finalstring += "\n" + CalendarOwner + "'s Calendar:\n\n";
           
            foreach (DateTime key in Activities.Keys)
            {
                SortByTime(key);
                finalstring += key.ToString("MM/dd/yyyy") + ":\n";
                foreach (Activity item in Activities[key])
                {
                    finalstring += "\t" + item.ToString() + "\n";
                }
            }
            return finalstring + new string('-', 100) + "\n\n";
        }

    }
}
