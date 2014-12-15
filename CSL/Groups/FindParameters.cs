using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Groups
{
    /// <summary>
    /// Enumeration with find parameters.
    /// </summary>
    public enum FindParameters
    {
        /// <summary>
        /// First value found.
        /// </summary>
        FIRST = 0,

        /// <summary>
        /// Last value found.
        /// </summary>
        LAST,

        /// <summary>
        /// Any value found.
        /// </summary>
        ANY,

        /// <summary>
        /// Lowest value found.
        /// </summary>
        MIN,

        /// <summary>
        /// Highest value found.
        /// </summary>
        MAX
    }
}
