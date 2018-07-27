namespace Task7.Tasks.Task7_2
{
    using static System.Console;

    internal class Task7_2
    {
        private string[] strs =
        {
             "1,35",
             "-1,35",
             "1",
             "fref",
             "1,3efsffs",
             "5436",
             "-5436",
             "546.2445"
        };

        public Task7_2()
        {
            WriteLine("Task7.2\n===================================================");
            this.Result();
        }

        private void Result()
        {
            foreach (var item in this.strs)
            {
                WriteLine("\"{0}\", is positive integer: {1}", item, item.IsPositiveNumber());
            }
        }
    }
}
