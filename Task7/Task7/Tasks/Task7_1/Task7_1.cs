namespace Task7.Tasks.Task7_1
{
    using System;

    internal class Task7_1
    {
        private int[] array1 = { 0, 1, 2, 4, 5, 6 };
        private float[] array2 = { 5, 5, 21, 6, 1, 6 };
        private long[] array3 = { 8, 1, 5, 8 };

        public Task7_1()
        {
            Console.WriteLine("Task7.1\n===================================================");
            this.Result();
        }

        private void Result()
        {
            Console.WriteLine("Sum entires of array1(int): {0}", this.array1.EntrySum());
            Console.WriteLine("Sum entires of array2(float): {0}", this.array2.EntrySum());
            Console.WriteLine("Sum entires of array3(int32): {0}", this.array3.EntrySum());
        }
    }
}
