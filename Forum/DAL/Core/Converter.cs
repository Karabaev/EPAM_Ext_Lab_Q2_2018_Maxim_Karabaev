using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public static class Converter
    {
        public static object ConvertFromDBValue(object val)
        {
            if(val == null || val == DBNull.Value)
            {
                return null;
            }
            try
            {
                if ((DateTime)val != default(DateTime))
                {
                    return (DateTime)val;
                }
            }
            catch(InvalidCastException)
            {
                return val;
            }
            return val;


        }
    }
}
