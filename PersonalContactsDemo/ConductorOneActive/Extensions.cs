using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConductorOneActive
{
    public static class Extensions
    {

        public static IEnumerable<Object> AsEnumerable(this IList list)
        {
            IList<Object> retVal = new List<Object>();

            foreach (Object obj in list)
            {
                retVal.Add(obj);
            }

            return retVal.AsEnumerable<Object>();
        }


    }
}
