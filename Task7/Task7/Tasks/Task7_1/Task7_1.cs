namespace Task7.Tasks.Task7_1
{
    using System;
    class Task7_1
    {
        private int[] array1 = { 0, 1, 2, 4, 5, 6 };
        private float[] array2 = { 5, 5, 21, 6, 1, 6 };
        private Int64[] array3 = { 8, 1, 5, 8 };

        public Task7_1()
        {
            Console.WriteLine("Task7.1\n===================================================");
            Result();
        }

        private void Result()
        {
            Console.WriteLine("Sum entires of array1(int): {0}", array1.EntrySum());
            Console.WriteLine("Sum entires of array2(float): {0}", array2.EntrySum());
            Console.WriteLine("Sum entires of array3(int32): {0}", array3.EntrySum());
        }
    }
}
