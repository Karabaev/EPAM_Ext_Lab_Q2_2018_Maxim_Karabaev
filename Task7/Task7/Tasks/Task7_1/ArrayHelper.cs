using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7.Tasks.Task7_1
{
    static class ArrayHelper
    {
        static public double EntrySum(this Array array)
        {
            double result = 0;
            try
            {
                foreach (var item in array)
                {
                    result += Convert.ToDouble(item);
                }
            }
            catch(InvalidCastException ex)
            {
                Console.WriteLine("Error! {0}", ex.Message);
            }
            
            return result;
        }
    }
}
