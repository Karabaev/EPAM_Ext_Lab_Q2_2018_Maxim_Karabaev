using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Task6.Tasks.Model
{
    struct Time
    {
        public int Seconds;
        public int Minutes;
        public int Hours;

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", Hours, Minutes, Seconds);
        }
    }
}
