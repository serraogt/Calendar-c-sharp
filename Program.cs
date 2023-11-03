using System;

namespace SerraOgutcenLab3
{
    class Program
    {
        static void Main(string[] args)
        {

            Calendar ke = new("Kutluhan Erol");
            Calendar cg = new("Çınar Gedizlioğlu");

            cg.AddActivity(new Activity("SE 307 Lab", "Work", new DateTime(2023, 11, 28, 12, 00, 00), new DateTime(2023, 11, 28, 13, 30, 00)));
            cg.AddActivity(new Activity("Wedding", "Social", new DateTime(2023, 11, 28, 19, 00, 00), new DateTime(2023, 11, 28, 19, 30, 00)));
            cg.AddActivity(new Activity("Picnic", "Social", new DateTime(2023, 11, 28, 23, 00, 00), new DateTime(2023, 11, 28, 23, 30, 00)));

            // Activity in the past
            cg.AddActivity(new Activity("Meeting", "Work", new DateTime(2023, 08, 29, 11, 00, 00), new DateTime(2023, 08, 29, 16, 00, 00)));

           // cg.RemoveActivity(3);
            cg.UpdateActivity(1, 1, "SE 307-1 Lab");
            cg.UpdateActivity(1, 4, "23:00");

            ke.AddActivity(new Activity("Office Hour", "Work", new DateTime(2023, 11, 27, 12, 00, 00), new DateTime(2023, 11, 27, 14, 00, 00)));
            ke.AddActivity(new Activity("SE 307 Theory", "Work", new DateTime(2023, 11, 28, 14, 30, 00), new DateTime(2023, 11, 28, 16, 00, 00)));
            ke.AddActivity(new Activity("Football Match", "Social", new DateTime(2023, 11, 28, 19, 00, 00), new DateTime(2023, 11, 28, 23, 30, 00)));

            // Activity that starts and ends at different days
            ke.AddActivity(new Activity("Meeting", "Work", new DateTime(2023, 12, 04, 11, 00, 00), new DateTime(2023, 12, 05, 14, 15, 00)));

            // Clashing activity
            ke.AddActivity(new Activity("Meeting", "Work", new DateTime(2023, 11, 27, 11, 00, 00), new DateTime(2023, 11, 27, 14, 15, 00)));

            ke.RemoveActivity(5);
            ke.UpdateActivity(4, 3, "15:00");
            ke.UpdateActivity(4, 3, "13:00");

            //sucessful share
            cg.ShareActivity(1, ke);

            //invalid id
            cg.ShareActivity(15, ke);

            //clash
            cg.ShareActivity(2, ke);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(cg.ToString());
            Console.WriteLine(ke.ToString());
            Console.ResetColor();

        }
    }
}
