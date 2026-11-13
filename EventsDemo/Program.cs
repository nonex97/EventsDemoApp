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

            Console.ReadLine();
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
}
