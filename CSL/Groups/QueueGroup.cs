using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Groups
{
    /// <summary>
    /// Class that represents Queue.
    /// It inherits class PureGroup.
    /// </summary>
    public class QueueGroup : PureGroup
    {
        #region Members

        internal Queue<uint> m_queue;
        private uint queueCount;

        #endregion

        #region Constructors

        /// <summary>
        /// Standard constructor.
        /// </summary>
        public QueueGroup()
        {
            this.m_queue = new Queue<uint>();
        }

        /// <summary>
        /// Constructor that takes count of members as argument.
        /// </summary>
        /// <param name="count"></param>
        public QueueGroup(uint count)
        {
            // m_queue = new List<object>(count);
            this.m_queue = new Queue<uint>();
            this.queueCount = count;
        }

        #endregion

        #region Delegates

        /// <summary>
        /// Delegate used in some methods.        
        /// </summary>
        /// <param name="el">Current element index.</param>
        /// <returns>Returns long value.</returns>
        public delegate long QueueLongDelegate(uint el);

        /// <summary>
        /// Delegate used in some methods.        
        /// </summary>
        /// <param name="el">Current element index.</param>
        /// <returns>Bool value of method.</returns>
        public delegate bool QueueDelegate(uint el);

        #endregion

        /// <summary>
        /// Loads initial elements.
        /// Count of elements is taken from constructor parameter.
        /// </summary>
        public override void Load()
        {
            for (uint i = 0; i < this.queueCount; i++)
            {
                this.m_queue.Enqueue(i);
            }
        }

        /// <summary>
        /// Removes all elements from queue.
        /// </summary>
        public override void Zero()
        {
            this.m_queue.Clear();
        }

        /// <summary>
        /// Search for element by type referenced.
        /// </summary>
        /// <param name="element">Referenced element, found value is passed by.</param>
        /// <param name="parameter">Enum with search parameter.</param>
        /// <returns></returns>
        public override bool Find(ref uint element, FindParameters parameter)
        {
            bool found = false;

            if (this.m_queue.Count > 0)
            {
                // element = m_queue.Dequeue();
                element = this.m_queue.First();
                found = true;
            }

            return found;
        }


        public override bool Find(ref uint element, FindParameters parameter, PureGroup.PBoolDelegate function)
        {
            Queue<uint> tempQueue = new Queue<uint>();
            uint tempElement;
            bool found = false;

            while (this.m_queue.Count > 0)
            {
                tempElement = this.m_queue.First();
                if (function(tempElement))
                {
                    found = true;
                }
                else
                {
                    tempQueue.Enqueue(tempElement);
                    m_queue.Dequeue();
                }

                if (found)
                {
                    element = tempElement;

                    int count = m_queue.Count;
                    for (int i = 0; i < count; i++)
                    {
                        tempElement = m_queue.Dequeue();
                        tempQueue.Enqueue(tempElement);
                    }

                    count = tempQueue.Count;
                    for (int j = 0; j < count; j++)
                    {
                        tempElement = tempQueue.Dequeue();
                        m_queue.Enqueue(tempElement);
                    }

                    return found;
                }
            }

            return found;
        }

        public override void To(uint element)
        {
            this.m_queue.Enqueue(element);
            currentCount = m_queue.Count;
        }

        public override void From(uint element)
        {
            Queue<uint> tempQueue = new Queue<uint>();
            uint tempElement;
            int count = m_queue.Count;

            for (int i = 0; i < count; i++)
            {
                tempElement = m_queue.Dequeue();
                if (!(tempElement == element))
                {
                    tempQueue.Enqueue(tempElement);
                }
            }
            count = tempQueue.Count;

            for (int j = 0; j < count; j++)
            {
                tempElement = tempQueue.Dequeue();
                m_queue.Enqueue(tempElement);
            }

            currentCount = m_queue.Count;

        }

        public override int Loses(PureGroup group)
        {
            try
            {
                QueueGroup groupToRemove = new QueueGroup();
                groupToRemove = group as QueueGroup;
                List<uint> listToRemove = new List<uint>();
                listToRemove = groupToRemove.m_queue.ToList();
                List<uint> targetList = new List<uint>();
                targetList = this.m_queue.ToList();

                foreach (uint element in listToRemove)
                {
                    targetList.Remove(element);
                }

                this.m_queue.Clear();
                foreach (uint element in targetList)
                {
                    this.m_queue.Enqueue(element);
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override int Gains(PureGroup group)
        {
            try
            {
                QueueGroup groupToAdd = new QueueGroup();
                groupToAdd = group as QueueGroup;
                List<uint> listToAdd = new List<uint>();
                listToAdd = groupToAdd.m_queue.ToList();
                List<uint> targetList = new List<uint>();
                targetList = this.m_queue.ToList();

                foreach (uint element in listToAdd)
                {
                    bool exists = false;
                    IEnumerator enumerator = targetList.GetEnumerator();
                    enumerator.Reset();

                    while (enumerator.MoveNext())
                    {
                        if ((uint)enumerator.Current == element)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists)
                    {
                        targetList.Add(element);
                        this.m_queue.Enqueue(element);
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override int Converse(PureGroup group)
        {
            try
            {
                m_queue.Clear();
                QueueGroup temp = group as QueueGroup;

                for (uint i = 0; i < queueCount; i++)
                {
                    if (!temp.m_queue.Contains(i))
                    {
                        this.m_queue.Enqueue(i);
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override int In(uint j)
        {
            List<uint> targetList = new List<uint>();
            targetList = this.m_queue.ToList();
            bool isIn = false;
            uint elementToCheck = j;

            foreach (uint element in targetList)
            {
                if (element == elementToCheck)
                {
                    isIn = true;
                }
            }

            if (isIn)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override int NotIn(uint j)
        {
            if (this.In(j) == 1)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public override int Equals(PureGroup group)
        {
            QueueGroup groupToCheck = new QueueGroup();
            groupToCheck = group as QueueGroup;
            List<uint> listToCheck = new List<uint>();
            listToCheck = groupToCheck.m_queue.ToList();
            List<uint> targetList = new List<uint>();
            targetList = this.m_queue.ToList();

            bool areEqual = false;

            if (listToCheck.Count == targetList.Count)
            {
                listToCheck.Sort();
                targetList.Sort();

                for (int i = 0; i < targetList.Count; i++)
                {
                    if (listToCheck[i] == targetList[i])
                    {
                        areEqual = true;
                    }
                    else
                    {
                        areEqual = false;
                        return 0;
                    }
                }

                if (areEqual)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public override int Within(PureGroup group)
        {
            QueueGroup groupToCheck = new QueueGroup();
            groupToCheck = group as QueueGroup;
            List<uint> listToCheck = new List<uint>();
            listToCheck = groupToCheck.m_queue.ToList();
            List<uint> targetList = new List<uint>();
            targetList = this.m_queue.ToList();

            bool within = false;

            foreach (uint element in targetList)
            {
                bool exists = false;
                IEnumerator enumerator = listToCheck.GetEnumerator();
                enumerator.Reset();

                while (enumerator.MoveNext())
                {
                    if ((uint)enumerator.Current == element)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    within = false;
                    break;
                }
                else
                {
                    within = true;
                }
            }

            if (within)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override int Empty()
        {
            if (this.m_queue.Count == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override int Head(uint j)
        {
            List<uint> targetList = new List<uint>();
            targetList = this.m_queue.ToList();
            bool elementExists = false;

            foreach (uint element in targetList)
            {
                if (element == j)
                {
                    elementExists = true;
                    break;
                }
            }

            if (!elementExists)
            {
                uint[] tempArray = new uint[this.m_queue.Count];
                this.m_queue.CopyTo(tempArray, 0);

                this.m_queue.Clear();
                this.m_queue.Enqueue(j);

                foreach (uint element in tempArray)
                {
                    this.m_queue.Enqueue(element);
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override int Tail(uint j)
        {
            List<uint> targetList = new List<uint>();
            targetList = this.m_queue.ToList();
            bool elementExists = false;

            foreach (uint element in targetList)
            {
                if (element == j)
                {
                    elementExists = true;
                    break;
                }
            }

            if (!elementExists)
            {
                this.m_queue.Enqueue(j);
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// For every element in queue performs actions from delegate method.
        /// </summary>
        /// <param name="pureGroupDelegate">Delegate method.</param>
        /// <returns>1 if success, 0 otherwise.</returns>
        public override int For(PureGroup.PBoolDelegate pureGroupDelegate)
        {
            List<uint> targetList = new List<uint>();
            targetList = this.m_queue.ToList();

            try
            {
                foreach (uint element in targetList)
                {
                    pureGroupDelegate(element);
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public override int Split(PureGroup.PBoolDelegate f1, ref PureGroup group1, ref PureGroup group2, PureGroup.PBoolDelegate f2)
        {
            try
            {
                List<uint> temp = m_queue.ToList();

                foreach (uint element in temp)
                {
                    if (f2(element))
                    {
                        if (f1(element))
                        {
                            group1.To(element);
                        }
                        else
                        {
                            group2.To(element);
                        }
                    }
                }

                return 1;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Adds sum of all elements returned by groupDelegate.
        /// </summary>
        /// <param name="groupDelegate">Method that counts value for current element.</param>
        /// <returns>Sum of all elements values or -1 if something goes wrong.</returns>
        public override long Sum(PureGroup.PValDelegate groupDelegate)
        {
            return this.m_queue.Sum(x => groupDelegate(x));
        }

        /// <summary>
        /// Checks, if certain elements number match delegate method condition.
        /// </summary>
        /// <param name="j">Number of elements to match the condition.</param>
        /// <param name="groupDelegate">Delegate method that checks condition.</param>
        /// <returns>1 if number of elements that match the condition is j, otherwise -1.</returns>
        public override int Unique(uint j, PureGroup.PBoolDelegate groupDelegate)
        {
            var count = this.Count(groupDelegate);
            if (count == j)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override int Count(PureGroup.PBoolDelegate groupDelegate)
        {
            return this.m_queue.Count(x => groupDelegate(x));
        }

        /// <summary>
        /// Check if at least certain number of elements match condition.
        /// </summary>
        /// <param name="j">Minimal number of elements to satisfy condition.</param>
        /// <param name="groupDelegate">Delegate method with condition to match.</param>
        /// <returns>1 if at least j elements match condition, -1 othwerwise.</returns>
        public override int Exists(uint j, PureGroup.PBoolDelegate groupDelegate)
        {
            foreach (var element in this.m_queue)
            {
                if (groupDelegate(element))
                {
                    j--;
                }

                if (j == 0) return 1;
            }

            return 0;
        }

        /// <summary>
        /// Checks if ALL queue elements met delegate's condition.
        /// </summary>
        /// <param name="pureGroupDelegate">Delegate that checks condition.</param>
        /// <returns>Returns 1 if all elements met condition, otherwise 0.</returns>
        public override int All(PureGroup.PBoolDelegate pureGroupDelegate)
        {
            if (this.m_queue.All(x => pureGroupDelegate(x)))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override int Rank(PureGroup.PValDelegate groupDelegate)
        {
            List<uint> targetList = new List<uint>();
            targetList = this.m_queue.ToList();
            List<long> tempList = new List<long>();
            try
            {
                for (uint i = 0; i < targetList.Count; i++)
                {
                    tempList.Add(groupDelegate(i));
                }

                List<long> helpList = new List<long>();
                foreach (long tempElement in tempList)
                {
                    helpList.Add(tempElement);
                }

                tempList.Sort();
                this.m_queue.Clear();

                foreach (long element in tempList)
                {
                    foreach (uint targetElement in helpList)
                    {
                        if (element == targetElement)
                        {
                            this.m_queue.Enqueue((uint)helpList.IndexOf(targetElement));
                        }
                    }
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
