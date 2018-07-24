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
        public Time(Time time)
        {
            Hours = time.Hours;
            Minutes = time.Minutes;
            Seconds = time.Seconds;
        }

        public Time(int hours, int mins, int secs)
        {
            Hours = hours;
            Minutes = mins;
            Seconds = secs;
        }


        public static bool operator < (Time time1, Time time2)
        {
            if(time1.Hours < time2.Hours)
                return true;
            if (time1.Hours > time2.Hours)
                return false;
            if (time1.Hours == time2.Hours)
            {
                if (time1.Minutes < time2.Minutes)
                    return true;
                if (time1.Minutes > time2.Minutes)
                    return false;
                if (time1.Minutes == time2.Minutes)
                {
                    if (time1.Seconds < time2.Seconds)
                        return true;
                    if (time1.Seconds >= time2.Seconds)
                        return false;
                }
            }
            return true;  
        }
        public static bool operator >(Time time1, Time time2)
        {
            if (time1.Hours > time2.Hours)
                return true;
            if (time1.Hours < time2.Hours)
                return false;
            if (time1.Hours == time2.Hours)
            {
                if (time1.Minutes > time2.Minutes)
                    return true;
                if (time1.Minutes < time2.Minutes)
                    return false;
                if (time1.Minutes == time2.Minutes)
                {
                    if (time1.Seconds > time2.Seconds)
                        return true;
                    if (time1.Seconds <= time2.Seconds)
                        return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", Hours, Minutes, Seconds);
        }
    }
}
