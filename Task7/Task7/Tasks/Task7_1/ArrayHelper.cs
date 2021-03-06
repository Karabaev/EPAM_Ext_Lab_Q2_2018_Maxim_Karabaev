﻿namespace Task7.Tasks.Task7_1
{
    using System;

    internal static class ArrayHelper
    {
        public static double EntrySum(this Array array)
        {
            double result = 0;
            try
            {
                foreach (var item in array)
                {
                    result += Convert.ToDouble(item);
                }
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Error! {0}", ex.Message);
            }

            return result;
        }
    }
}
