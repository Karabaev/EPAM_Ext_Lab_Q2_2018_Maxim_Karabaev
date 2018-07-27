namespace Task7
{
    using System;
    using Task7.Tasks.Task7_1;
    using Task7.Tasks.Task7_2;
    using Task7.Tasks.Task7_3;
    class Program
    {
        private const string Yes = "y";
        static void Main(string[] args)
        {
            Task7_1 task7_1 = new Task7_1();
            Task7_2 task7_2 = new Task7_2();
            Task7_3 task7_3;
            Console.WriteLine("Start tests of task 7.3? This may take a long time. {0} / n", Yes);
            if (Console.ReadLine().ToLower() == Yes)
                task7_3 = new Task7_3();
        }
    }
}
