using System;

namespace CodeSignal
{
    public static class Program
    {
        private const string WORLD_EXIST_ERROR = "World does not exist.";

        private const string WORLD_IMPLEMENT_ERROR = "World not yet implemented.";

        public const string MENU_FORMAT = "\t[{0}]\t{1}";

        public class PickWorldException : Exception {
            public PickWorldException(string message) : base(message) {}
        }

        public static void Main(string[] args)
        {
            uint choice = 0;

            do {
                try {
                    Console.Clear();
                    Console.WriteLine("Hello, CodeSignal Arcade!");
                    choice = pickWorld();

                    switch (choice) {
                        case 0:
                            break;
                        case 1:
                            try {
                                choice = Tests.Intro.pickProblem();
                                Tests.Intro.runTests(choice);
                            } catch (Exception e) {
                                Console.WriteLine("\n" + e.Message);
                            }
                            break;
                        case 2:
                            throw new PickWorldException(WORLD_IMPLEMENT_ERROR);
                        case 3:
                            throw new PickWorldException(WORLD_IMPLEMENT_ERROR);
                        case 4:
                            throw new PickWorldException(WORLD_IMPLEMENT_ERROR);
                        case 5:
                            throw new PickWorldException(WORLD_IMPLEMENT_ERROR);
                        default:
                            throw new PickWorldException(WORLD_EXIST_ERROR);
                    }
                } catch (Exception e) {
                    Console.WriteLine("\n" + e.Message);
                } finally {
                    Console.WriteLine("\nPress any key to continue from the main menu.");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (choice > 0);
        }

        private static uint pickWorld() {
            Console.WriteLine("\nPick a world:\n");
            Console.WriteLine(MENU_FORMAT, 1, "Intro");
            Console.WriteLine(MENU_FORMAT, 2, "Databases");
            Console.WriteLine(MENU_FORMAT, 3, "The Core");
            Console.WriteLine(MENU_FORMAT, 4, "Python");
            Console.WriteLine(MENU_FORMAT, 5, "Graphs");
            Console.Write("\nSelection (0 to exit):\t");
            return UInt32.Parse(Console.ReadLine());
        }
    }
}