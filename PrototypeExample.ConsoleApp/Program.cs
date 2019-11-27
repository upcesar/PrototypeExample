using PrototypeExample.ConsoleApp.People;
using PrototypeExample.ConsoleApp.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Rectangle = PrototypeExample.ConsoleApp.Shapes.Rectangle;

namespace PrototypeExample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadSafeSingleton();
            NaiveSingleton();
            PrototypePerson();
            PrototypeShapes();
        }

        private static void PrototypeShapes()
        {
            var circle = new Circle(50, 10, 30);
            circle.FillColor(Color.Blue);
            circle.Tag = new Tag($"This is the original {circle.Name}");
            circle.Notes = "Note 1";

            var rectangle = new Rectangle(10, 20, 50, 50);
            rectangle.FillColor(Color.Red);
            rectangle.Tag = new Tag($"This is the original {rectangle.Name}");
            rectangle.Notes = "Note 2";

            Console.WriteLine(circle);
            Console.WriteLine(rectangle);

            var shapeList = new List<Shape> { circle, rectangle };

            Console.WriteLine(new string('*', 50));
            shapeList.ForEach(original =>
            {
                var shallow = original.ShallowCopy();
                var deep = original.DeepCopy();

                Console.WriteLine("{0} position before moving: ({1},{2})", original.Name, original.X, deep.Y);
                Console.WriteLine("---Notes before changing---");
                Console.WriteLine("Original: {0}\tShallowCopy: {1}", original.Tag, shallow.Tag);
                Console.WriteLine("Original: {0}\tDeepCopy: {1}", original.Tag, deep.Tag);

                original.Move(original.X + 5, original.Y + 5);
                original.FillColor(Color.Black);
                original.Tag.Notes = $"A Shallow Copy of {original.Name} was created";
                original.Notes = original.Tag.Notes;

                Console.WriteLine("{0} position after moving: ({1},{2})", original.Name, original.X, deep.Y);

                Console.WriteLine("{0} Color", original.Name);
                Console.WriteLine("Original: {0}\nShallowCopy: {1}", original.Color, shallow.Color);
                Console.WriteLine("Original: {0}\nDeepCopy: {1}", original.Color, deep.Color);

                Console.WriteLine("---Notes after changing---");
                Console.WriteLine("Original: {0}\nShallowCopy: {1}", original.Tag, shallow.Tag);
                Console.WriteLine("Original: {0}\nDeepCopy: {1}", original.Tag, deep.Tag);


                Console.WriteLine(new string('*', 50));
                Console.WriteLine("Original: {0}\nShallowCopy: {1}\nDeepCopy: {2}", original.Notes, shallow.Notes, deep.Notes);
                Console.WriteLine(new string('*', 50));

            });
        }

        private static void NaiveSingleton()
        {
            Person p1 = Person.GetInstance();
            SetupValues(p1);

            Person p2 = Person.GetInstance();

            Console.WriteLine("\nNahive Singleton - Comparing instances");
            Console.WriteLine("   p1 comparing instance with p5 (Singleton)");
            DisplaySingleton(p1, p2);
        }

        private static void SetupValues(Person p1)
        {
            p1.Age = 42;
            p1.BirthDate = Convert.ToDateTime("1977-01-01");
            p1.Name = "Jack Daniels";
            p1.document = new Document(666);
        }

        private static void ThreadSafeSingleton()
        {
            Console.WriteLine("\nSingleton with Thread Safe");

            Thread process1 = new Thread(() =>
            {
                var tp1 = Person.GetInstanceThreadSafe();
                tp1.BirthDate = Convert.ToDateTime("1940-03-10");
                tp1.Age = new DateTime(DateTime.Now.Date.Subtract(tp1.BirthDate).Ticks).Year - 1;
                tp1.Name = "Chuck Norris";
                tp1.document = new Document(6969);

                DisplayPersonValues(tp1);

            });

            Thread process2 = new Thread(() =>
            {
                var tp2 = Person.GetInstanceThreadSafe();
                tp2.BirthDate = Convert.ToDateTime("1940-11-27");
                tp2.Age = new DateTime(DateTime.Now.Date.Subtract(tp2.BirthDate).Ticks).Year - 1;
                tp2.Name = "Bruce Lee";
                tp2.document = new Document(74156);

                DisplayPersonValues(tp2);
            });

            process1.Start();
            process2.Start();

            process1.Join();
            process2.Join();
        }

        private static void PrototypePerson()
        {
            Person p1 = Person.GetInstance();

            // Perform a shallow copy of p1 and assign it to p2.
            Person p2 = p1.ShallowCopy();

            // Make a deep copy of p1 and assign it to p3.
            Person p3 = p1.DeepCopy();

            // Display values of p1, p2 and p3.
            Console.WriteLine("\nOriginal values of p1, p2, p3:");
            Console.WriteLine("   p1 instance values: ");
            DisplayPersonValues(p1);
            Console.WriteLine("   p2 instance values:");
            DisplayPersonValues(p2);
            Console.WriteLine("   p3 instance values:");
            DisplayPersonValues(p3);

            // Change the value of p1 properties and display the values of p1,
            // p2 and p3.
            p1.Age = 32;
            p1.BirthDate = Convert.ToDateTime("1900-01-01");
            p1.Name = "Frank";
            p1.document.Value = 7878;
            Console.WriteLine("\nValues of p1, p2 and p3 after changes to p1:");
            Console.WriteLine("   p1 instance values: ");
            DisplayPersonValues(p1);
            Console.WriteLine("   p2 instance values (reference values have changed):");
            DisplayPersonValues(p2);
            Console.WriteLine("   p3 instance values (everything was kept the same):");
            DisplayPersonValues(p3);
        }

        private static void DisplayPersonValues(Person p)
        {
            Console.WriteLine(
                "      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}",
                p.Name, p.Age, p.BirthDate);

            Console.WriteLine(
                "      ID#: {0:d}",
                p.document.Value);
        }

        private static void DisplaySingleton<T>(T obj1, T obj2)
            => Console.WriteLine("      The objects {0}",
                                 ReferenceEquals(obj1, obj2) ?
                                        "use a shared instance." :
                                        "are objects with differentes instances"
                );
    }
}
