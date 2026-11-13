using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CollegeClassModel history = new CollegeClassModel("History 101", 3);
            CollegeClassModel math = new CollegeClassModel("Calculus 2", 2);

            history.EnrollmentFull += CollegeClass_EnrollmentFull; // add the event listener

            history.SignUpStudent("Neven Grguric").PrintToConsole();
            history.SignUpStudent("Yuki Fox").PrintToConsole();
            history.SignUpStudent("John Bark").PrintToConsole();
            history.SignUpStudent("Barky Dork").PrintToConsole();
            Console.WriteLine();

            math.EnrollmentFull += CollegeClass_EnrollmentFull; // add the event listener

            math.SignUpStudent("Neven Grguric").PrintToConsole();
            math.SignUpStudent("Yuki Fox").PrintToConsole();
            math.SignUpStudent("John Bark").PrintToConsole();

            // Homework
            Console.WriteLine();
            GuestListModel birthday = new GuestListModel("Neven's birthday", 5);

            birthday.PartyFull += Birthday_PartyFull;

            birthday.AddToGuestList("Kyler Miller").PrintToConsole();
            birthday.AddToGuestList("Malcolm Gilbert").PrintToConsole();
            birthday.AddToGuestList("George Little").PrintToConsole();
            birthday.AddToGuestList("Connor Sims").PrintToConsole();
            birthday.AddToGuestList("Jared Jackson").PrintToConsole();
            birthday.AddToGuestList("Oliver Wood").PrintToConsole();
            birthday.AddToGuestList("Myles Silva").PrintToConsole();

            Console.ReadLine();
        }

        private static void Birthday_PartyFull(object sender, string e)
        {
            GuestListModel model = (GuestListModel)sender;

            Console.WriteLine();
            Console.WriteLine($"{model.PartyName} is now full!");
            Console.WriteLine();
        }

        // One event handler for all classes
        private static void CollegeClass_EnrollmentFull(object sender, string e)
        {
            CollegeClassModel model = (CollegeClassModel)sender; // we cast sender to the correct type (CollegeClassModel), we can extract data from the CollegeClassModel to give a different text than the one in invoke

            Console.WriteLine();
            //Console.WriteLine(e); // this would show the message we wrote when invoking the event (EnrollmentFull?.Invoke(this, $"{CourseTitle} enrollment is now full."); // invoke the event)
            Console.WriteLine($"{model.CourseTitle}: Full");
            Console.WriteLine();
        }

        // Different event handlers for each course
        /*
        private static void Math_EnrollmentFull(object sender, string e)
        {
            Console.WriteLine();
            Console.WriteLine("The enrollment is full for math.");
            Console.WriteLine();
        }

        private static void History_EnrollmentFull(object sender, string e)
        {
            Console.WriteLine();
            Console.WriteLine("The enrollment is full for history.");
            Console.WriteLine();
        }
        */
    }

    public static class ConsoleHelpers
    {
        public static void PrintToConsole(this string message)
        {
            Console.WriteLine(message);
        }
    }

    public class CollegeClassModel
    {
        public event EventHandler<string> EnrollmentFull; // create an event handler

        private List<string> enrolledStudents = new List<string>();
        private List<string> waitingList = new List<string>();

        public string CourseTitle { get; private set; }
        public int MaximumStudents { get; private set; }

        public CollegeClassModel(string title, int maximumStudents)
        {
            CourseTitle = title;
            MaximumStudents = maximumStudents;
        }

        public string SignUpStudent(string studentName)
        {
            string output = "";

            if (enrolledStudents.Count < MaximumStudents)
            {
                enrolledStudents.Add(studentName);
                output = $"{studentName} was enrolled in {CourseTitle}";
                // Check to see if me are maxed out
                if (enrolledStudents.Count == MaximumStudents)
                {
                    EnrollmentFull?.Invoke(this, $"{CourseTitle} enrollment is now full."); // invoke the event
                }
            }
            else
            {
                output = $"{studentName} was added to the waitlist in {CourseTitle}";
                waitingList.Add(studentName);
            }

            return output;
        }
        
    }

    // Homework: Create an event in a class an then consume that class. Attach a listener to the event and havit it write to the console.
    public class GuestListModel
    {
        public event EventHandler<string> PartyFull;

        private List<string> guests = new List<string>();
        private List<string> waitList = new List<string>();

        public string PartyName { get; set; }
        public int MaxGuests { get; set; }

        public GuestListModel(string partyName, int maxGuests)
        {
            PartyName = partyName;
            MaxGuests = maxGuests;
        }

        public string AddToGuestList(string guestName)
        {
            string output = "";

            if (guests.Count < MaxGuests)
            {
                guests.Add(guestName);
                output = $"{guestName} was added to the {PartyName}";
                // Check if we are full
                if (guests.Count == MaxGuests)
                {
                    PartyFull?.Invoke(this, $"{PartyName} is now full.");
                }
            }
            else
            {
                output = $"{guestName} was added to the waitlist for the {PartyName}.";
                waitList.Add(guestName);
            }

            return output;
        }
    }
}
