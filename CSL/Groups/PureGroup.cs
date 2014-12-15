using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Groups
{
    /// <summary>
    /// Abstract base class for Queue and Set.
    /// </summary>
    public abstract class PureGroup
    {
        /// <summary>
        /// Represents current count of elements in group.
        /// </summary>
        protected int currentCount;

        /// <summary>
        /// Getter for current count.
        /// </summary>
        internal int CurrentCount
        {
            get
            {
                return this.currentCount;
            }
        }

        /// <summary>
        /// Delegate used in group methods.
        /// </summary>
        /// <param name="el">Index of current element in group.</param>
        /// <returns>True if condition is met, false otherwise.</returns>
        public delegate bool PBoolDelegate(uint el);

        /// <summary>
        /// Delegate used in group methods.
        /// </summary>
        /// <param name="el">Index of current element in group.</param>
        /// <returns>Value.</returns>
        public delegate long PValDelegate(uint el);

        /// <summary>
        /// Initialises group.
        /// </summary>
        /// <returns></returns>
        public abstract void Load();

        /// <summary>
        /// Clears current group.
        /// </summary>
        public abstract void Zero();

        /// <summary>
        /// Add uint element to group.
        /// </summary>
        /// <param name="uintToAdd"></param>
        public abstract void To(uint uintToAdd);

        /// <summary>
        /// Take and remove uint element from group.
        /// </summary>
        /// <param name="elementToRemove">Object to remove.</param>
        public abstract void From(uint uintToRemove);

        /// <summary>
        /// Checks for element in group.
        /// </summary>
        /// <param name="element">Reference for object to find.</param>
        /// <param name="parameter">FindParameters enum.</param>
        /// <returns>True if found, false otherwise.</returns>
        public abstract bool Find(ref uint element, FindParameters parameter);

        /// <summary>
        /// Checks for element in group with specified condition.
        /// </summary>
        /// <param name="element">Reference for object to find.</param>
        /// <param name="parameter">FindParameters enum.</param>
        /// <param name="function">Specified condition to be met.</param>
        /// <returns></returns>
        public abstract bool Find(ref uint element, FindParameters parameter, PBoolDelegate function);

        /// <summary>
        /// Removed all elements grom argument group from current group.
        /// </summary>
        /// <param name="group">Group with elements to remove.</param>
        /// <returns>If success returns 1, otherwise 0.</returns>
        public abstract int Loses(PureGroup group);

        /// <summary>
        /// Adds group elements to current group.
        /// </summary>
        /// <param name="group">Group to add.</param>
        /// <returns>True if succeeded.</returns>
        public abstract int Gains(PureGroup group);

        /// <summary>
        /// Adds conversed group elements in current group.
        /// </summary>
        /// <param name="group">Elements to converse.</param>
        /// <returns>True if succeeded.</returns>
        public abstract int Converse(PureGroup group);

        /// <summary>
        /// Checks if group contains following element.
        /// </summary>
        /// <param name="j">Element to check.</param>
        /// <returns>True if group contains element.</returns>
        public abstract int In(uint j);

        /// <summary>
        /// Checks if element is not in group.
        /// </summary>
        /// <param name="j">Element to check.</param>
        /// <returns>True if element is NOT in group.</returns>
        public abstract int NotIn(uint j);

        /// <summary>
        /// Checks if current group has the same elements as input group.
        /// </summary>
        /// <param name="group">Group to compare.</param>
        /// <returns>True if groups contain the same elements.</returns>
        public abstract int Equals(PureGroup group);

        /// <summary>
        /// Checks if group is within target group.
        /// </summary>
        /// <param name="group">Group to check.</param>
        /// <returns>True if target group contains input group.</returns>
        public abstract int Within(PureGroup group);

        /// <summary>
        /// Removes all elements from group.
        /// </summary>
        /// <returns></returns>
        public abstract int Empty();

        /// <summary>
        /// Adds elemtn to head of group.
        /// </summary>
        /// <param name="j">Element to add.</param>
        /// <returns>True if element added.</returns>
        public abstract int Head(uint j);

        /// <summary>
        /// Adds element to the end of group.
        /// </summary>
        /// <param name="j">Element to add.</param>
        /// <returns>True if element added.</returns>
        public abstract int Tail(uint j);

        /// <summary>
        /// Perform specified function for each element in group.
        /// </summary>
        /// <param name="pureGroupDelegate">Delegate function to perform on elements.</param>
        /// <returns>True if everything was performed.</returns>
        public abstract int For(PBoolDelegate pureGroupDelegate);

        /// <summary>
        /// Checks if all elements in group meet specified condition.
        /// </summary>
        /// <param name="pureGroupDelegate">Delegate function with condition.</param>
        /// <returns>True if every element meets condition.</returns>
        public abstract int All(PBoolDelegate pureGroupDelegate);

        /// <summary>
        /// Checks if there exist number of elements that meet condition.
        /// </summary>
        /// <param name="j">Number of elements to meet condition.</param>
        /// <param name="groupDelegate">Delegate function with condition.</param>
        /// <returns>True if yes.</returns>
        public abstract int Exists(uint j, PBoolDelegate groupDelegate);

        /// <summary>
        /// Checks, if certain elements number match delegate method condition.
        /// </summary>
        /// <param name="j">Number of elements to match the condition.</param>
        /// <param name="groupDelegate">Delegate method that checks condition.</param>
        /// <returns>1 if number of elements that match the condition is j, otherwise -1.</returns>
        public abstract int Unique(uint j, PBoolDelegate groupDelegate);

        /// <summary>
        /// Counts elements that match specified condition.
        /// </summary>
        /// <param name="groupDelegate">Delegate method that checks condition.</param>
        /// <returns>Number of elements that match condition.</returns>
        public abstract int Count(PBoolDelegate groupDelegate);

        /// <summary>
        /// Sum the value of elements that match condition.
        /// </summary>
        /// <param name="groupDelegate">Delegate method that checks condition.</param>
        /// <returns>Sum of values.</returns>
        public abstract long Sum(PValDelegate groupDelegate);

        /// <summary>
        /// Split groups by specified conditions.
        /// If element matches both conditions, it goes to group 1.
        /// If element matches only second condition, it goes to group 2.
        /// Otherwise it is omitted.
        /// </summary>
        /// <param name="f1">First condition.</param>
        /// <param name="group1">One target group.</param>
        /// <param name="group2">Second target group.</param>
        /// <param name="f2">Second condition.</param>
        /// <returns></returns>
        public abstract int Split(PBoolDelegate f1, ref PureGroup group1, ref PureGroup group2, PBoolDelegate f2);

        /// <summary>
        /// Ranks elements by specified condition.
        /// </summary>
        /// <param name="groupDelegate">Delegate function with condition.</param>
        /// <returns>True if elements were ranked.</returns>
        public abstract int Rank(PValDelegate groupDelegate);       
    }
}
