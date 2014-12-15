using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Statistics.HelperClasses
{
    /// <summary>
    /// Helper class that represents single continuous statistic record.
    /// </summary>
    class StatCRecord
    {
        //value of record to add
        long x;
        //moment of time when current event occured
        long t;

        /// <summary>
        /// Constructor of statistic record.
        /// </summary>
        /// <param name="_x">Value of statistic.</param>
        /// <param name="_t">Time at which it occured.</param>
        internal StatCRecord(long _x, long _t)
        {
            x = _x;
            t = _t;
        }
    }
}
