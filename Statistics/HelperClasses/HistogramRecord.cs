using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Statistics.HelperClasses
{
    /// <summary>
    /// Helper class that represent single histogram record.
    /// </summary>
    internal class HistogramRecord
    {
        long value;
        uint occurence;

        /// <summary>
        /// Histogram record constructor.
        /// </summary>
        /// <param name="_value">Value of current record.</param>
        /// <param name="_occurence">Occurence of current record.</param>
        public HistogramRecord(long _value, uint _occurence)
        {
            value = _value;
            occurence = _occurence;
        }

    }
}
