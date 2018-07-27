﻿namespace Task7.Tasks.Task7_2
{
    public static class StringHelper
    {
        public static bool IsPositiveNumber(this string str)
        {
            foreach (char item in str)
            {
                if (!char.IsDigit(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
